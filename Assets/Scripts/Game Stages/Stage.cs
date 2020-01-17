using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage : MonoBehaviour {
    public GameObject next;
    /*
     * it should become switch to `true` when, for example
     * the fight is over or dialogue choice has been done.
     */
    private bool easyContinuable = false;
     

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
