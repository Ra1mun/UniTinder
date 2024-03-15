using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using Unity.VisualScripting;

public class Client : MonoBehaviour
{
    public static Client Instance { get; private set; }
    public static int dataBufferSize = 4096;

    [SerializeField] private string ip = "127.0.0.1";
    [SerializeField] private int port = 26950;
    public int myId = 0;

    public TCP tcp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> packetHandlers;

    private void Awake()
    { if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToServer()
    {
        InitializeClientData();

        isConnected = true;
        tcp.Connect();
    }

    public void SendMessageToServer()
    {

    }

    public class TCP
    {
        public TcpClient socket;

        private NetworkStream _stream;
        private Packet _receivedData;
        private byte[] _receiveBuffer;

        public void Connect()
        {
            socket = new TcpClient
            {
                ReceiveBufferSize = dataBufferSize,
                SendBufferSize = dataBufferSize,
            };

            _receiveBuffer = new byte[dataBufferSize];
            socket.BeginConnect(Instance.ip, Instance.port, ConnectCallback, socket);
        }

        private void ConnectCallback(IAsyncResult result)
        {
            socket.EndConnect(result);

            if (!socket.Connected)
            {
                return;
            }

            _stream = socket.GetStream();

            _receivedData = new Packet();

            _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
        }

        public async void SendData(Packet packet)
        {
            try
            {
                if (socket != null)
                {
                    //_stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null); // legacy code

                    await _stream.WriteAsync(packet.ToArray(), 0 ,packet.Length());

                }
            }
            catch (Exception ex)
            {
                Debug.Log($"Error sending data to server via TCP: {ex}");
            }
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = _stream.EndRead(result);
                if (byteLength <= 0)
                {
                    Disconnect();
                    return;
                }

                byte[] data = new byte[byteLength];
                Array.Copy(_receiveBuffer, data, byteLength);

                _receivedData.Reset(HandleData(data));
                _stream.BeginRead(_receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error receiving TCP data: {e}");
                Disconnect();

            }
        }

        private bool HandleData(byte[] data)
        {
            int packetLength = 0;

            _receivedData.SetBytes(data);

            if (_receivedData.UnreadLength() >= 4)
            {
                packetLength = _receivedData.ReadInt();
                if (packetLength <= 0)
                {
                    return true;
                }
            }
            while (packetLength > 0 && packetLength <= _receivedData.UnreadLength())
            {
                byte[] packetBytes = _receivedData.ReadBytes(packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet packet = new Packet(packetBytes))
                    {
                        int packetId = packet.ReadInt();
                        packetHandlers[packetId](packet);
                    }
                });

                packetLength = 0;
                if (_receivedData.UnreadLength() >= 4)
                {
                    packetLength = _receivedData.ReadInt();
                    if (packetLength <= 0)
                    {
                        return true;
                    }
                }
            }
            if (packetLength <= 1)
            {
                return true;
            }
            
            return false;
            
        }

        private void Disconnect()
        {
            Instance.Disconnect();
            _stream = null;
            _receivedData = null;
            _receiveBuffer = null;
            socket = null;
        }

    }

    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
            {(int)ServerPackets.welcome, ClientHandle.Welcome }
        };
        Debug.Log("Initialized Client Data");
    }

    private void Disconnect()
    {
     
        if (isConnected)
        {
            isConnected = false;
            tcp.socket.Close();

            Debug.Log("disconnected from sever");
        }

    }

}
