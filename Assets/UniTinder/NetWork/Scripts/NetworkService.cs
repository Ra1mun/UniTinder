using System.Collections.Generic;
using UniTinder.Application.Installer;
using UnityEngine;
using Zenject;

namespace UniTinder.Network
{
    public class NetworkService : ILateDisposable, ITickable
    {
        private readonly TickableManager _tickableManager;
        private readonly ThreadManager _threadManager;

        public delegate void PacketHandler(Packet packet);
        public readonly int DataBufferSize = 4096;

        private readonly TCP _tcp;
        private readonly ClientSend _clientSend;
        private readonly ClientHandle _clientHandle;


        private bool _isConnected;
        private Dictionary<int, PacketHandler> _packetHandlers;
        private int _id;
        private int _dbID;
        private bool _isActive;

        public Dictionary<int, PacketHandler> PacketHandlers => _packetHandlers;
        public TCP TCP => _tcp;



        public NetworkService(DevelopmentSettings developmentSettings, TickableManager tickableManager)
        {
            _tickableManager = tickableManager;
            _threadManager = new ThreadManager();
            _clientSend = new ClientSend(this);
            _clientHandle = new ClientHandle(this, _clientSend);

            _tcp = new TCP(this, developmentSettings.IP, developmentSettings.Port, _threadManager);
            ConnectToServer();
            Enable();
        }

        private void Enable()
        {
            if (!_isActive)
            {
                _tickableManager.Add(this);
                _isActive = true;
            }

        }

        private void Disable()
        {
            if (_isActive)
            {
                _tickableManager.Remove(this);
                _isActive = false;
            }
        }

        public void SetUserID(int id)
        {
            _id = id;
        }

        public int GetUserID()
        {
            return _id;
        }

        public void SetDbID(int dbID)
        {
            _dbID = dbID;
        }

        public int GetDbID()
        {
            return _dbID;
        }

        public void ConnectToServer()
        {
            InitializeClientData();

            _isConnected = true;
            _tcp.Connect();
        }

        public void TryAuthorize(string email, string password)
        {
            _clientSend.TryAuthorize(email, password);
        }

        public void RegisterNewUser(string nickname, string email, string city, string job, int experienceTime)
        {
            _clientSend.RegisterNewUser(nickname, email, city, job, experienceTime);
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
        
        public void Tick()
        {
            
            _threadManager.UpdateMain();
        }
        
        public void LateDispose()
        {
            Disable();
            Disconnect();
        }

        private void InitializeClientData()
        {
            _packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ServerPackets.welcome, _clientHandle.Welcome },
                {(int)ServerPackets.registeredNewUser, _clientHandle.RegisteredNewUser },
                {(int)ServerPackets.sendIntoApp, _clientHandle.SendIntoApp },

            };
            Debug.Log("Initialized Client Data");
        }

        
    }
}
