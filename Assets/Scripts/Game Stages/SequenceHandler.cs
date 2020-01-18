using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SequenceHandler : MonoBehaviour {
    public GUISkin skin;
    public GameObject sequence;
    public GameObject lobby;
    public bool goNext = false;


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
                sequence.GetComponent<Sequence>().Next();
                lobby.GetComponent<LobbyHandler>().count = 0;
                lobby.GetComponent<LobbyHandler>().MakeUsersUnclicked();


                lobby.GetComponent<LobbyHandler>().everyoneIsReady = false;
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
}
