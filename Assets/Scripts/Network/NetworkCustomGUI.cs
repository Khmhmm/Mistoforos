using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCustomGUI : MonoBehaviour {

    public NetworkManager network;
    public NetworkDiscovery discovery;
    public GUISkin gskin;
    float xOffset = MainmenuGUI.xOffset;
    float yOffset = MainmenuGUI.yOffset;
    bool action = false;
    public string field = "localhost", hostip = "localhost";

    void Start()
    {
        network = GetComponent<NetworkManager>();
    }

	void OnGUI()
    {
        //flag that hides gui when server was created or client was connected
        if(action) { return; }

        GUI.skin = gskin;
        GUI.Label(
            new Rect(Screen.width - xOffset, Screen.height * 0.01f, Screen.width * 0.5f, Screen.height * 0.09f), 
            "Please, choose an option");

        //HOST
        if(GUI.Button(
            new Rect(Screen.width - xOffset - xOffset + 10f, Screen.height * 0.01f + yOffset, xOffset - 10f, 20f),
            "LAN HOST"))
        {
            network.StartHost();
            action = true;
        }

      // GUI.Label(
      //      new Rect(Screen.width - xOffset, Screen.height * 0.01f + yOffset, xOffset - 30f, 40f),
      //      "Number of players: ");
      //  numPlayersField = GUI.TextField(new Rect(Screen.width - 30f, Screen.height * 0.01f + yOffset, 30f, 40f), numPlayersField);

        //CONNECT
        field = GUI.TextField(new Rect(Screen.width - xOffset, 250f, xOffset - 10f, 20f), field);

        if(GUI.Button(
            new Rect(Screen.width - xOffset*2f, 250f, xOffset - 10f, 20f),
            "LAN CONNECT"))
        {
            network.networkAddress = field.ToString();
            network.StartClient();
            action = true;
        }
    }
}
