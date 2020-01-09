using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeoCustomGUI : MonoBehaviour {
    public NeoNetworkManager network;
    public GUISkin gskin;
    float xOffset = MainmenuGUI.xOffset;
    float yOffset = MainmenuGUI.yOffset;
    bool action = false;
    public string field = "localhost", hostip = "localhost";

    void Start()
    {
        network = GetComponent<NeoNetworkManager>();
    }

    void OnGUI()
    {
        if (action) { return; }

        GUI.skin = gskin;
        GUI.Label(
            new Rect(Screen.width - xOffset, Screen.height * 0.01f, Screen.width * 0.5f, Screen.height * 0.09f),
            "Please, choose an option");

        //HOST
        if (GUI.Button(
            new Rect(Screen.width - xOffset - xOffset + 10f, Screen.height * 0.01f + yOffset, xOffset - 10f, 20f),
            "LAN HOST"))
        {
            network.SetupServer();
            network.SetupLocalClient();
            action = true;
        }

        //hostip = GUI.TextField(new Rect(Screen.width - xOffset, Screen.height*0.01f + yOffset, xOffset - 10f, 20f), hostip);

        //CONNECT
        field = GUI.TextField(new Rect(Screen.width - xOffset, 250f, xOffset - 10f, 20f), field);

        if (GUI.Button(
            new Rect(Screen.width - xOffset * 2f, 250f, xOffset - 10f, 20f),
            "LAN CONNECT"))
        {
            network.SetupClient(field.ToString());
            action = true;
        }
    }
}
