using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCustomGUI : MonoBehaviour {

    public NetworkManager network;
    public GUISkin gskin;
    float xOffset = MainmenuGUI.xOffset;
    float yOffset = MainmenuGUI.yOffset;
    bool action = false;

    void Start()
    {
        network = GetComponent<NetworkManager>();
    }

	void OnGUI()
    {
        if(action) { return; }

        GUI.skin = gskin;
        GUI.Label(
            new Rect(Screen.width - xOffset, Screen.height * 0.01f, Screen.width * 0.5f, Screen.height * 0.09f), 
            "Please, choose an option");

        //HOST
        if(GUI.Button(
            new Rect(Screen.width - xOffset, Screen.height * 0.01f + yOffset, Screen.width * 0.35f, 20f),
            "LAN HOST"))
        {
            network.StartHost();
            action = true;
        }

        //CONNECT
        var field = GUI.TextField(
            new Rect(Screen.width - xOffset, 250f, xOffset - 10f, 20f), 
            "localhost");

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
