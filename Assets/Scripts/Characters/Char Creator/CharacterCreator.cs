using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterCreator : MonoBehaviour {
    public GUISkin skin;
    GameObject newHero; 
    Hero heroScript;

    bool dispose = true, delay = false;

	void Start()
    {
        newHero = new GameObject("New Hero");
        newHero.transform.parent = this.transform;
        newHero.AddComponent<Hero>();

        heroScript = newHero.GetComponent<Hero>();
        heroScript.charName = "Name";
        heroScript.charSurname = "Surname";
    }

    void OnGUI()
    {
        GUI.skin = skin;



        heroScript.charName = GUI.TextField(new Rect(Screen.width * 0.4f, Screen.height * 0.1f, Screen.width * 0.2f, 40f), heroScript.charName);
        heroScript.charSurname = GUI.TextField(new Rect(Screen.width * 0.4f, Screen.height * 0.1f + 40f, Screen.width * 0.2f, 40f), heroScript.charSurname);
        if (GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.1f + 80f, Screen.width * 0.2f, 40f),"Mage? " + heroScript.mage.ToString()))
        {
            heroScript.mage = !heroScript.mage;
        }

        if (!dispose)
        {
            GUI.Label(new Rect(Screen.width * 0.6f, Screen.height * 0.5f, Screen.width * 0.2f, 40f), "Successfully saved.");
        }

        if(GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.5f, Screen.width * 0.2f, 40f), "Save") && !delay)
        {
            heroScript.SaveOnDisk();
            StartCoroutine("Delay");
            dispose = false;
            delay = true;
        }

        if (GUI.Button(new Rect(Screen.width * 0.4f, Screen.height * 0.8f, Screen.width * 0.2f, 40f), "Quit"))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(4f);
        delay = false;
        dispose = true;
    }
}
