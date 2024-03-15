using UniTinder.Network;
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

    public void WelcomeReceived()
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            packet.Write(_networkService.GetUserID());

            SendTCPData(packet);
        }
    }

    public void SendMessageToServer() 
    {
        using (Packet packet = new Packet((int)ClientPackets.sendMessageToServer))
        {
            packet.Write(_networkService.GetUserID());

            SendTCPData(packet);
        }
    }

}
