using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyHandler : MonoBehaviour {
    public GUISkin skin;
    public List<GameObject> players;
    public GameObject host = null;
    private static float xOffset = MainmenuGUI.xOffset, yOffset = MainmenuGUI.yOffset;
    private static Rect rect = new Rect(Screen.width - xOffset, Screen.height * 0.1f, Screen.width * 0.1f, 40f);

    void Start()
    {
        InvokeRepeating("PlayersChecker", 0.5f, 1f);
        InvokeRepeating("HostChecker",3f,5f);
    }

    void PlayersChecker()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Player");
        foreach(var player in tmp)
        {
            if (!players.Contains(player))
            {
                players.Add(player);
            }
        }
    }

    void HostChecker()
    {
        if(!players[0].GetComponent<UserScript>().hosting && host==null)
        {
            //the first player is always host
            players[0].GetComponent<UserScript>().hosting = true;
            host = players[0];
            CancelInvoke("HostChecker");
            Debug.Log("Host has been found");
        }
    }


    
    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(rect, "Party: ");
        //TODO: optimize
        for (int i = 0; i < players.Count; i++)
        {
            try
            {
                GUI.Label(new Rect(Screen.width - xOffset, Screen.height * 0.1f + yOffset + i * 20f, Screen.width * 0.1f, 40f), players[i].GetComponent<UserScript>().userName);
            }
            catch(Exception e) { players.RemoveAt(i); Debug.LogError(e); }
        }
    }
}