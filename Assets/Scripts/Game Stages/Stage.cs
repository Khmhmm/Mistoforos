using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Stage : NetworkBehaviour {

    public void Start()
    {
        gameObject.AddComponent<NetworkIdentity>();
        gameObject.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
    }

    public abstract string GetType();
    public abstract string GetContent();

    public void Run()
    {
        //it is helping method for situations, when you need to make a background
        //or start a music for example
    }

    public int nextStageIndex;
    /*
     * it should become switch to `true` when, for example
     * the fight is over or dialogue choice has been done.
     */
    protected bool easyContinuable = false;
     

    public abstract void ShowGUI();

    public void SetEasyContinuable(bool set)
    {
        easyContinuable = set;
    }

    public bool IsEasyToContinue()
    {
        return easyContinuable;
    }
}
