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

    public void TryAuthorize() // записать сюда емэйл и пароль
    {
        using (Packet packet = new Packet((int)ClientPackets.welcomeReceived))
        {

            //packet.Write(_networkService.GetUserID());
            // packet.Write(Email)
            // packet.Write(password)

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
