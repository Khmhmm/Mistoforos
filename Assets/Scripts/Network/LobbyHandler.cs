using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LobbyHandler : MonoBehaviour {
    public GUISkin skin;
    public GameObject playerPrefab;
    public List<GameObject> players;
    private static float xOffset = MainmenuGUI.xOffset, yOffset = MainmenuGUI.yOffset;
    public int count=0;
    public GameObject sequence;

    public UserScript lastUser;

    public bool spawned = false;
    void Start()
    {
        InvokeRepeating("PlayersChecker", 0.5f, 1f);
    }

    void LateUpdate()
    {

    }

    void PlayersChecker()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("Player");
        if (tmp.Length == 0)
        {
            var player = Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f));
            CancelInvoke();
            players.Add(player);
        }
        else
        {
            foreach (var player in tmp)
            {
                if (!players.Contains(player))
                {
                    players.Add(player);
                    count = 0;
                    player.GetComponent<UserScript>().proverka = 100 + new System.Random().Next(1, 50);
                    foreach(var clearClick in players)
                    {
                        clearClick.GetComponent<UserScript>().clicked = false;
                    }
                }
            }
        }
    }




    void OnGUI()
    {
        if (spawned)
        {
            return;
        }
        else
        {
            GUI.Label(new Rect(0f, Screen.height * 0.1f, Screen.width * 0.1f, Screen.height * 0.1f), count + "/" + players.Count);
            GUI.skin = skin;
            GUI.Label(new Rect(Screen.width - xOffset, Screen.height * 0.1f, Screen.width * 0.1f, 40f), "Party: ");
            //TODO: optimize
            for (int i = 0; i < players.Count; i++)
            {
                try
                {
                    GUI.Label(new Rect(Screen.width - xOffset, Screen.height * 0.1f + yOffset + i * 20f, Screen.width * 0.1f, 40f), players[i].GetComponent<UserScript>().userName);
                }
                catch (Exception e) {
                    players.RemoveAt(i);
                    count = 0;
                    foreach (var clearClick in players)
                    {
                        clearClick.GetComponent<UserScript>().clicked = false;
                    }
                }
            }
        }
    }
}