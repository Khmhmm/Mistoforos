using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSequence : Sequence {

    // Use this for initialization
    protected void Start () {

        var g = new GameObject("new");
        g.AddComponent<SingleReplics>();
        g.GetComponent<SingleReplics>().speaker = "master";
        g.GetComponent<SingleReplics>().text = "It is test seq";
        g.transform.parent = this.transform;

        stages.Add(g);
        base.Start();
    }
}
