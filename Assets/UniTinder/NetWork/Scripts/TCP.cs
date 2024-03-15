using System;
using System.Net.Sockets;
using UnityEngine;

namespace UniTinder.Network
{
    public class TCP
        {
            private readonly NetworkService _networkService;
            private readonly string _ip;
            private readonly int _port;

            private TcpClient _socket;

            private NetworkStream _stream;
            private Packet _receivedData;
            private byte[] _receiveBuffer;
            
            
            public TCP(NetworkService networkService, string ip, int port)
            {
                _networkService = networkService;
                _ip = ip;
                _port = port;
            }

            public void Connect()
            {
                _socket = new TcpClient
                {
                    ReceiveBufferSize = _networkService.DataBufferSize,
                    SendBufferSize = _networkService.DataBufferSize,
                };

                _receiveBuffer = new byte[_networkService.DataBufferSize];
                _socket.BeginConnect(_ip, _port, ConnectCallback, _socket);
            }

            public void CloseSocket()
            {
                _socket.Close();
            }

            private void ConnectCallback(IAsyncResult result)
            {
                _socket.EndConnect(result);

                if (!_socket.Connected)
                {
                    return;
                }

                _stream = _socket.GetStream();

                _receivedData = new Packet();

                _stream.BeginRead(_receiveBuffer, 0, _networkService.DataBufferSize, ReceiveCallback, null);
            }

            public async void SendData(Packet packet)
            {
                try
                {
                    if (_socket != null)
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
                    _stream.BeginRead(_receiveBuffer, 0, _networkService.DataBufferSize, ReceiveCallback, null);
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
                            _networkService.PacketHandlers[packetId](packet);
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
                _networkService.Disconnect();
                
                _stream = null;
                _receivedData = null;
                _receiveBuffer = null;
                _socket = null;
            }

        }
}