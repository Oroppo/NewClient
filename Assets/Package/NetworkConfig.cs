using System;
using KaymakNetwork.Network;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine.UI;

internal static class NetworkConfig
{
    internal static Client socket;

    internal static void InitNetwork()
    {
        if (!ReferenceEquals(socket, null)) return;
        socket = new Client(100);
        NetworkReceive.PacketRouter();
    }

    internal static void ConnectToServer(string ip)
    {   
        socket.Connect(ip, 8888);
    }

    internal static void DisconnectFromServer()
    {
        socket.Dispose();
    }
}

