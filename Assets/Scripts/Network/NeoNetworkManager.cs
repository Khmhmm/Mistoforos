using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NeoNetworkManager : MonoBehaviour {

    public NetworkClient myClient;

    // Create a server and listen on a port
    public void SetupServer()
    {
        NetworkServer.Listen(7070);
    }

    // Create a client and connect to the server port
    public void SetupClient(string address)
    {
        myClient = new NetworkClient();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.Connect(address, 7070);
    }

    // Create a local client and connect to the local server
    public void SetupLocalClient()
    {
        myClient = ClientScene.ConnectLocalServer();
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
    }

    // client function
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected to server");
    }
}
