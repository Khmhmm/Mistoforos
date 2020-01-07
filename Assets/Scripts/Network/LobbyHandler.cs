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
    public int count=0;
    public GameObject sequence;
    public bool spawned = false;

    private static float xOffset = MainmenuGUI.xOffset, yOffset = MainmenuGUI.yOffset;


    void Start()
    {
        InvokeRepeating("PlayersChecker", 0.5f, 1f);
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
                    player.GetComponent<UserScript>().id = players.Count;
                    count = 0;
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
                    //current hero of the player
                    if (players[i].GetComponent<UserScript>().myHero != null) { GUI.Label(new Rect(Screen.width - xOffset - 45f, Screen.height * 0.1f + yOffset + i * 20f + 10f, Screen.width * 0.1f, 20f), players[i].GetComponent<UserScript>().myHero.charName + "," + players[i].GetComponent<UserScript>().myHero.level); }
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

    public void Attach(GameObject g, int listIndex)
    {
        g.transform.parent = players[listIndex].transform;
        players[listIndex].GetComponent<UserScript>().myHero = g.GetComponent<Hero>();
    }
}