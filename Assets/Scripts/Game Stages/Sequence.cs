using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour {

    public List<GameObject> stages;

    public GameObject current = null;
    private int index = 0;

    protected void Start()
    {
        if (stages[0] != null) { current = stages[0]; }
        if (current == null)
        {
            Debug.LogError("Cannot start, the first stage = null");
        }
        else
        {
            //check next
        }

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


    }

}
