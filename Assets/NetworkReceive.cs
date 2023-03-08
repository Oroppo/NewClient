using System;
using System.Collections.Generic;
using KaymakNetwork;
using UnityEngine;

enum ServerPackets
{
    SWelcomeMsg = 1,
    SInstantiatePlayer,
    SPlayerMove,
}
internal static class NetworkReceive
{
    internal static void PacketRouter()
    {
        NetworkConfig.socket.PacketId[(int)ServerPackets.SWelcomeMsg] = new KaymakNetwork.Network.Client.DataArgs(Packet_WelcomeMsg);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SInstantiatePlayer] = new KaymakNetwork.Network.Client.DataArgs(Packet_InstantiateNetworkPlayer);
        NetworkConfig.socket.PacketId[(int)ServerPackets.SPlayerMove] = new KaymakNetwork.Network.Client.DataArgs(Packet_PlayerMove);
    }
    private static void Packet_WelcomeMsg(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        string msg = buffer.ReadString();
        buffer.Dispose();

        NetworkManager.instance.myConnectionID = connectionID;

        NetworkSend.SendPing();
    }
    private static void Packet_InstantiateNetworkPlayer(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();

        if (connectionID == NetworkManager.instance.myConnectionID)
            NetworkManager.instance.InstantiateNetworkPlayer(connectionID, true);
        else
            NetworkManager.instance.InstantiateNetworkPlayer(connectionID, false);
    }
    private static void Packet_PlayerMove(ref byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer(data);
        int connectionID = buffer.ReadInt32();
        float x = buffer.ReadSingle();
        float y = buffer.ReadSingle();
        float z = buffer.ReadSingle();

        buffer.Dispose();
        if (!GameManager.instance.playerList.ContainsKey(connectionID)) return;

        foreach (KeyValuePair<int, GameObject> kvp in GameManager.instance.playerList)
        {
            //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value.name);
        }

        Debug.Log("X: " + x + "Y: "+ y + "Z: " + z);
        
        GameManager.instance.playerList[connectionID].transform.position = new Vector3(x, y, z);
        


    }
}

