using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour {

    public string killMe = "";

	// Use this for initialization
	void Start () {
        InvokeRepeating("Kill", 0.2f, 1f);
	}

    void Kill()
    {
        var go = GameObject.Find(killMe);
        if(go==null) { return; }
        Destroy(go.gameObject);
    }
}
