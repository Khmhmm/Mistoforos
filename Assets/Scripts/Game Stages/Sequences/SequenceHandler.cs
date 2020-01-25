using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SequenceHandler : NetworkBehaviour {

    public GUISkin skin;
    public GameObject sequence;
    public GameObject lobby;

    [SyncVar(hook = "SetNextStage")] public int handlerNextStage = 0;


    void LateUpdate()
    {
        if (lobby == null)
        {
            lobby = GameObject.Find("Handler");
        }
        else
        {
            if (sequence == null || !lobby.GetComponent<LobbyHandler>().heroesLoaded)
                return;

            lobby.GetComponent<LobbyHandler>().CmdSetStage(sequence.GetComponent<Sequence>().current);

            if(sequence.GetComponent<Sequence>().current.GetComponent<Stage>().GetType().Equals("EndingStage"))
            {
                //TODO:
                //update player's hero savefile
                //load next sequence from long-time memory by [clientrpc] method
            }

            if (lobby.GetComponent<LobbyHandler>().everyoneIsReady)
            {
                if (isServer) { SetNextStage(lobby.GetComponent<LobbyHandler>().players[0].GetComponent<UserScript>().nextStage); }
            

                lobby.GetComponent<LobbyHandler>().count = 0;
                lobby.GetComponent<LobbyHandler>().MakeUsersUnclicked();


                lobby.GetComponent<LobbyHandler>().everyoneIsReady = false;

                sequence.GetComponent<Sequence>().Next(handlerNextStage);

                if (isServer)
                {
                    foreach(var player in lobby.GetComponent<LobbyHandler>().players)
                    {
                        player.GetComponent<UserScript>().SetNextStage(0);
                    }
                }
            }

            if (sequence.GetComponent<Sequence>().destroying)
            {
                sequence = null;
            }
        }
    }

    
    void OnGUI()
    {
        GUI.skin = skin;
        if (sequence != null && lobby.GetComponent<LobbyHandler>().heroesLoaded)
        {
            sequence.GetComponent<Sequence>().current.GetComponent<Stage>().ShowGUI();
        }
    }

    public void SetNextStage(int next)
    {
        handlerNextStage = next;
    }
}
