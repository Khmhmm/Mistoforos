using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCustomGUI : MonoBehaviour {

    public NetworkManager network;
    public GUISkin gskin;

    void Start()
    {
        network = GetComponent<NetworkManager>();
    }

	void OnGUI()
    {
        GUI.skin = gskin;
        GUI.Label(new Rect(Screen.width * 0.15f, Screen.height * 0.01f, Screen.width * 0.5f, Screen.height * 0.09f), "Please, choose an option");

    }
}
