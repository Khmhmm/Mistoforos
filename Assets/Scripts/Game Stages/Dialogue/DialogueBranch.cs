using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueBranch : MonoBehaviour
{
    public string content;
    public int nextStageIndexLink;

    public override string ToString() { return "ID:" + nextStageIndexLink + ";" + "Content:" + content; }
}