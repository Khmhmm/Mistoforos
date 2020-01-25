using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleReplics : Stage {

    public string speaker, text;

    public SingleReplics(string speaker,string text) { this.speaker = speaker; this.text = text; SetEasyContinuable(true); }

    public void Start()
    {
        base.Start();

        if (!isLocalPlayer)
            return; 
        
        if (speaker.Contains("$player"))
        {
            var host = GameObject.Find("Handler").GetComponent<LobbyHandler>().players[0].GetComponent<UserScript>().charObject.GetComponent<Hero>();

            speaker = speaker.Replace("$player", host.charName + " " + host.charSurname);
        }
        if (text.Contains("$player"))
        {
            var host = GameObject.Find("Handler").GetComponent<LobbyHandler>().players[0].GetComponent<UserScript>().charObject.GetComponent<Hero>();
            
            text = text.Replace("$player", host.charName + " " + host.charSurname);
        }

     
    }

    public void LateUpdate()
    {
        easyContinuable = true;
    }

    public override void ShowGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, Screen.width * 0.5f, Screen.height * 0.05f), speaker);
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.2f, Screen.width * 0.5f, Screen.height * 0.25f), text);

    }

    public override string GetType()
    {
        return "SingleReplics";
    }

    public override string GetContent()
    {
        return "Speaker:" + speaker + ";Text:" + text;
    }
}
