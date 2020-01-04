using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuGUI : MonoBehaviour {

    public GUISkin skin;
    public static string mistoforos = "μισθοφόρος";
    public static float xOffset = mistoforos.Length * 20 + 10f;
    public static float yOffset = Screen.height * 0.05f + 40f;

    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(
            new Rect(Screen.width - xOffset, Screen.height * 0.1f, 300, 40f), mistoforos);


        if (GUI.Button(
            new Rect(Screen.width - xOffset, Screen.height * 0.1f + yOffset, 300f, 40f), "Play solo"
            ))
        {
            SceneManager.LoadScene(2);
        }

        if(GUI.Button(
            new Rect(Screen.width - xOffset, Screen.height * 0.1f + yOffset * 2, 300f, 40f), "LAN"
            ))
        {
            SceneManager.LoadScene(1);
        }

        if (GUI.Button(
            new Rect(Screen.width - xOffset, Screen.height * 0.1f + yOffset * 3, 300f,40f), "Exit"
            ))
        {
            Application.Quit();
        }
    }
}
