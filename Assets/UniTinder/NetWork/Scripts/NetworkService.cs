using System.Collections.Generic;
using UniTinder.Application.Installer;
using UnityEngine;
using Zenject;

namespace UniTinder.Network
{
    public class NetworkService : ILateDisposable
    {
        public delegate void PacketHandler(Packet packet);
        public readonly int DataBufferSize = 4096;
        
        private readonly TCP _tcp;
        private readonly ClientSend _clientSend;
        private readonly ClientHandle _clientHandle;


        private bool _isConnected;
        private Dictionary<int, PacketHandler> _packetHandlers;
        private int _id;
        
        public Dictionary<int, PacketHandler> PacketHandlers => _packetHandlers;
        public TCP TCP => _tcp;

        
        
        public NetworkService(DevelopmentSettings developmentSettings)
        {
            _clientSend = new ClientSend(this);
            _clientHandle = new ClientHandle(this, _clientSend);
            
            _tcp = new TCP(this, developmentSettings.IP, developmentSettings.Port);
        }

        public void SetUserID(int id)
        {
            _id = id;
        }

        public int GetUserID()
        {
            return _id;
        }

        public void ConnectToServer()
        {
            InitializeClientData();

            _isConnected = true;
            _tcp.Connect();
        }

        public void Disconnect()
        {
     
            if (_isConnected)
            {
                _isConnected = false;
                _tcp.CloseSocket();

                Debug.Log("disconnected from sever");
            }

        }
        
        public void LateDispose()
        {
            Disconnect();
        }

        private void InitializeClientData()
        {
            _packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ServerPackets.welcome, _clientHandle.Welcome }
            };
            Debug.Log("Initialized Client Data");
        }
    }
}
