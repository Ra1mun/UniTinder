using System;
using UniTinder.Network;
using UnityEditor.PackageManager;
using UnityEngine;



public class ClientHandle
{
    public static Action<bool> GoToMainWindow { get; set; }

    private readonly NetworkService _networkService;
    private readonly ClientSend _clientSend;

    public ClientHandle(NetworkService networkService, ClientSend clientSend)
    {
        _networkService = networkService;
        _clientSend = clientSend;
    }
    
    public void Welcome(Packet packet)
    {
        string message = packet.ReadString();
        int id = packet.ReadInt();

        Debug.Log($"Message from server: {message}");
        _networkService.SetUserID(id);
        //Debug.Log(_networkService.GetUserID());
        //_clientSend.TryAuthorize("test1@unitinder.com");
    }

    public void ReceiveMessageFromUser(Packet packet)
    {
        int senderID = packet.ReadInt();
        string message = packet.ReadString();
        int id = packet.ReadInt();

        Debug.Log($"Message from user {senderID}: {message}");

        _networkService.AddMessage(senderID, message);

        //_clientSend.WelcomeReceived();
    }




    public void RegisteredNewUser(Packet packet)
    {
        string message = packet.ReadString();
        int myId = packet.ReadInt();

        Debug.Log($"Message from server: {message}");
       // Client.Instance.myId = myId;
       // ClientSend.WelcomeReceived();
    }

    public void SendIntoApp(Packet packet)
    {
        bool check = packet.ReadBool();
        int connectedUsersCount = packet.ReadInt();
        int dbID = packet.ReadInt();
        int id = packet.ReadInt();

        _networkService.connectedUsersCount = connectedUsersCount;
        _networkService.SetDbID(dbID);

        GoToMainWindow?.Invoke(check);
        //Client.Instance.IdInDatabase = packet.ReadInt();
        //UIManager.Instance.ToAppTrigger();
    } 
}
