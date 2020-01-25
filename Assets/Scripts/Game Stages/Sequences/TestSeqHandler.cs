using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSeqHandler : MonoBehaviour {

    public GUISkin skin;
    public GameObject sequence;
    public bool delay = false;
    public void LateUpdate()
    {
        if(Input.GetButton("Submit") && !delay && sequence!=null)
        {
            sequence.GetComponent<Sequence>().Next();
            StartCoroutine("Delay");
            delay = true;
        }

        if (sequence != null && sequence.GetComponent<Sequence>().destroying)
        {
            sequence = null;
        }
    }

    void OnGUI()
    {
        if (sequence == null)
            return;

        GUI.skin = skin;
        sequence.GetComponent<Sequence>().current.GetComponent<Stage>().ShowGUI();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.7f);
        delay = false;
    }
}
