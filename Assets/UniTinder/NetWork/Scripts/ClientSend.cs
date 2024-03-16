using System;
using UniTinder.Network;
using UnityEditor.PackageManager;
using UnityEngine;

public class ClientSend
{


    private readonly NetworkService _networkService;

    public ClientSend(NetworkService networkService)
    {
        _networkService = networkService;
    }
    
    private void SendTCPData(Packet packet)
    {
        packet.WriteLength();
        _networkService.TCP.SendData(packet);
    }

    public void TryAuthorize(string email, string password) // записать сюда емэйл и пароль
    {
        using (Packet packet = new Packet((int)ClientPackets.connectUser))
        {

            packet.Write(_networkService.GetUserID());
            packet.Write(email);
            packet.Write(password);

            SendTCPData(packet);
        }
    }

    public void RegisterNewUser(string nickname, string email, string city, string job, int experienceTime)
    {
        using (Packet packet = new Packet((int)ClientPackets.registerNewUser))
        {
            
            packet.Write(_networkService.GetUserID());
            packet.Write(nickname);
            packet.Write(email);
            packet.Write(city);
            packet.Write(job);
            packet.Write(experienceTime);

            SendTCPData(packet);
        }
    }

    public void SendMessageToUser(int toUser, string message) 
    {
        using (Packet packet = new Packet((int)ClientPackets.sendMessageToUser))
        {
            packet.Write(_networkService.GetUserID());

            packet.Write(toUser);

            packet.Write(message);

            SendTCPData(packet);
        }
    }

}
