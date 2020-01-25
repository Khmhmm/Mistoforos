using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Dialogue : Stage
{
    public List<DialogueBranch> answers;
    public static string ANSWER_SEPARATOR = "##";
    public GameObject currentPlayer;

    public new void Start()
    {
        base.Start();
    }

    public override string GetContent()
    {
        string ret = "";

        foreach (var ans in answers)
        {
            ret += ans.ToString() + ANSWER_SEPARATOR;
        }

        return ret;
    }

    public override string GetType()
    {
        return "Dialogue";
    }

    void LateUpdate()
    {
        if (currentPlayer == null)
        {
            currentPlayer = GameObject.Find("Me");
        }
    }

    public override void ShowGUI()
    {
        if (easyContinuable)
            return;

        for(int i = 0; i < answers.Count; i++)
        {
            if(GUI.Button(new Rect(Screen.width*0.1f,Screen.height*0.5f + i*40f, Screen.width * 0.8f, 40f), answers[i].content))
            {
                currentPlayer.GetComponent<UserScript>().SetNextStage(answers[i].nextStageIndexLink);
                
                easyContinuable = true;
            }
        }
    }
}
