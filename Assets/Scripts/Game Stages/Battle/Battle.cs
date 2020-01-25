using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Battle : Stage
{
    public List<GUIStyle> styles;
    public GUISkin skin;
    
    //prefabs from this list will be spawned
    public List<GameObject> prefabSpawn;
    //this list contains player and his allies
    public List<GameObject> leftSide;
    //this list contains enemies
    public List<GameObject> rightSide;
    //turns queque
    public List<GameObject> turns;
    //events list that contains info about battle events
    public List<string> events;

    public GameObject currentAttacker;
    public bool turn = false;
    public bool result = false;
    public bool delay = false;

    public void Start()
    {
        Invoke("Initialize", 1f);
        foreach(var prefab in prefabSpawn)
        {
            Instantiate(prefab);
        }
        events.Add("The battle has been started");
        base.Start();
    }



    public void Initialize()
    {
        CmdInitialize();
    }

    [Command]
    void CmdInitialize()
    {
        RpcInitialize();
    }

    [ClientRpc]
    void RpcInitialize()
    {
        Character[] characters = GameObject.FindObjectsOfType<Character>();

        foreach (var chr in characters)
        {
            if (chr.gameObject.tag.Equals("Enemy"))
            {
                rightSide.Add(chr.gameObject);
            }
            else
            {
                leftSide.Add(chr.gameObject);
            }
        }

        turns.AddRange(leftSide);
        turns.AddRange(rightSide);

        currentAttacker = turns[0];
    }

    public override string GetContent()
    {
        return "";
    }

    public override string GetType()
    {
        return "Battle";
    }

    public override void ShowGUI()
    {

        GUI.skin = skin;
        for(int i = 0; i < leftSide.Count; i++)
        {
            GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.1f + i * 40f, 100f, 30f), leftSide[i].GetComponent<Character>().charName + ", " + leftSide[i].GetComponent<Character>().level);
        }

        for (int i = 0; i < rightSide.Count; i++)
        {
            GUI.Label(new Rect(Screen.width - 110f, Screen.height * 0.1f + i * 40f, 100f, 30f), rightSide[i].GetComponent<Character>().charName + ", " + rightSide[i].GetComponent<Character>().level);
        }

        if (events.Count > 0)
        {
            for(int i=0;i<events.Count;i++)
            {
                GUI.Label(new Rect(Screen.width * 0.2f, Screen.height * 0.6f + (events.Count - i) * 20f, Screen.width * 0.6f, 40f), events[i]);
            }
        }
    }

    public void LateUpdate()
    {
        if (transform.parent.GetComponent<Sequence>().current != this.gameObject || result)
            return;

        foreach (var character in leftSide)
        {
            if (character.GetComponent<Character>().hp <= 0)
            {
                leftSide.Remove(character);
            }
        }

        foreach (var character in rightSide)
        {
            if (character.GetComponent<Character>().hp <= 0)
            {
                rightSide.Remove(character);
            }
        }

        if (!turn && !delay)
        {
            Turn();
            turn = true;
            delay = true;
            StartCoroutine("Delay");
        }
    }

    public void Turn()
    {
        if(currentAttacker.tag == "Enemy")
        {
                System.Random rand = new System.Random();
                int listIndex = rand.Next() % leftSide.Count;

                var attacker = currentAttacker.GetComponent<Character>();
                var defender = leftSide[listIndex].GetComponent<Character>();

                events.Add(attacker.charName + " attacks " + defender.charName + " " + defender.charSurname);

                float damage = (int)(rand.Next() % attacker.attack);
                defender.hp -= damage;
            
        }
        else
        {
                System.Random rand = new System.Random();
                int listIndex = rand.Next() % rightSide.Count;

                var attacker = currentAttacker.GetComponent<Character>();
                var defender = rightSide[listIndex].GetComponent<Character>();

                events.Add(attacker.charName + " " + attacker.charSurname + " attacks " + defender.charName);

                float damage = (int)(rand.Next() % attacker.attack + attacker.attack);
                defender.hp -= damage;
        }
            NextTurn();
    }

    public void NextTurn()
    {
        if (!isServer)
            return;


        if(leftSide.Count==0 || rightSide.Count == 0)
        {
            //it will end the battle
            Result();
            result = true;
            return;
        }

        int index = turns.IndexOf(currentAttacker);

        if (index == turns.Count - 1)
        {
            index = 0;
        }
        else
        {
            index += 1;
        }

        currentAttacker = turns[index];

        if (!delay)
        {
            StartCoroutine("TurnDelay");
            delay = true;
        }
    }

    public void Result()
    {
        events.Add("The end of the battle...");
        if (leftSide.Count == 0)
        {
            this.transform.parent.GetComponent<Sequence>().destroying = true;
        }
        else
        {
            easyContinuable = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);
        delay = false;
    }

    IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(1.5f);
        delay = false;
        turn = false;
    }
}
