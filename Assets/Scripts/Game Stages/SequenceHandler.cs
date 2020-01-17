using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SequenceHandler : MonoBehaviour { 
    public GameObject sequence;
    public GameObject lobby;
    public bool goNext = false;
    public bool delay = false;

    void Start()
    {

    }

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

            if (lobby.GetComponent<LobbyHandler>().everyoneIsReady && !delay)
            {
                sequence.GetComponent<Sequence>().Next();
                lobby.GetComponent<LobbyHandler>().everyoneIsReady = false;
                lobby.GetComponent<LobbyHandler>().count = 0;
                lobby.GetComponent<LobbyHandler>().MakeUsersUnclicked();
                delay = true;
                StartCoroutine("Delay");
            }

            if (sequence.GetComponent<Sequence>().destroying)
            {
                sequence = null;
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        delay = false;
    }

    void OnGUI()
    {
        if (sequence != null && lobby.GetComponent<LobbyHandler>().heroesLoaded)
        {
            sequence.GetComponent<Sequence>().current.GetComponent<Stage>().ShowGUI();
        }
    }
}
