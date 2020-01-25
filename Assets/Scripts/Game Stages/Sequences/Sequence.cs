using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sequence : MonoBehaviour {

    public string family = "Default";
    public string codename = "01";

    public List<GameObject> stages;

    public GameObject current = null;
    private int index = 0;
    public bool destroying = false;

    protected void Start()
    {
        if (stages[0] != null) { current = stages[0]; }
        if (current == null)
        {
            Debug.LogError("Cannot start, the first stage = null");
            return;
        }

        bool endingStageIsExists = false;
        foreach(var stage in stages)
        {
            if (stage.GetComponent<Stage>().GetType().Equals("EndingStage"))
            {
                endingStageIsExists = true;
            }
        }

        if (!endingStageIsExists)
        {
            Debug.LogWarning("The ending stage object is not exist in this object. It is not good and may destroy player's save file");
        }
    }

    public void Next()
    {
        if (!current.GetComponent<Stage>().GetType().Equals("EndingStage") && current.GetComponent<Stage>().IsEasyToContinue())
        {
            current.GetComponent<Stage>().SetEasyContinuable(false);
            
            index = (current.GetComponent<Stage>().nextStageIndex != 0) ? current.GetComponent<Stage>().nextStageIndex : ++index;

            current = (stages.Count > index) ? stages[index] : null;
            if (current == null)
            {
                destroying = true;
                Destroy(this.gameObject);
            }
        }
        else
        {
            //TODO:
            // make method that say handler to load next sequence by property Quest in EndingStage class.
        }
    }

    public void Next(int nextStage)
    {
        GameObject.Find("Handler").GetComponent<LobbyHandler>().MakeUsersUnclicked();
        if (nextStage == 0) { Next(); }
        else
        {
            if (!current.GetComponent<Stage>().GetType().Equals("EndingStage") && current.GetComponent<Stage>().IsEasyToContinue())
            {
                current.GetComponent<Stage>().SetEasyContinuable(false);

                index = (nextStage != 0) ? nextStage : ++index;

                current = (stages.Count > index) ? stages[index] : null;
                if (current == null)
                {
                    destroying = true;
                    Destroy(this.gameObject);
                }
            }
        }
    }

}


/*
 * 
            GameObject endCredit = new GameObject("endSeq");
            endCredit.transform.parent = this.transform;
            endCredit.AddComponent<SingleReplics>();
            endCredit.GetComponent<SingleReplics>().speaker = "";
            endCredit.GetComponent<SingleReplics>().text = "The end";
            stages.Add(endCredit);

    
        foreach (var stage in stages)
        {
            stage.SetActive(false);
        }
        current.SetActive(true);
*/
