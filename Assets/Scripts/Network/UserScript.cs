using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserScript : NetworkBehaviour {
    public GameObject currentStage;
    public GUISkin skin;
    public string userName = "default";
    public GameObject lobby;
    public bool clicked = false;
    bool advice = true;
    public int proverka = 0;

    

    void Start()
    {
        lobby = GameObject.Find("Handler");
        if (isLocalPlayer)
        {
            this.name = "Host";
        }
        Invoke("CancelAdvice", 5f);
    }

    void LateUpdate()
    {
        if (Input.GetButton("Submit"))
        {
            CmdIncrement();
        }
    }

    [Command]
    public void CmdIncrement()
    {
        RpcIncrement();
    }

    [ClientRpc]
    void RpcIncrement()
    {
        if (!clicked)
        {
            lobby.GetComponent<LobbyHandler>().count += 1;
            clicked = true;
        }
    }

    void OnGUI()
    {
        GUI.skin = skin;

        if(advice) GUI.Label(new Rect(Screen.width*0.4f, Screen.height*0.05f,Screen.width*0.2f,40f), "Press `submit` key (ENTER by default) to continue...");

        if (GUI.Button(new Rect(Screen.width - MainmenuGUI.xOffset, Screen.height * 0.8f + MainmenuGUI.yOffset, 200f, 40f), "Quit"))
        {
            Application.Quit();
        }
    }

    void CancelAdvice()
    {
        advice = false;
    }

}
