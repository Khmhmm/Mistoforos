using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingStage : Stage {

    private new int nextStageIndex = 0;

    public string endCredits;
    public int nextQuestID;

    public override string GetContent()
    {
        return "EndCredits:" + endCredits;
    }

    public override string GetType()
    {
        return "EndingStage";
    }

    public override void ShowGUI()
    {
        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height*0.4f, Screen.width * 0.4f, Screen.height*0.2f),endCredits);
    }
}
