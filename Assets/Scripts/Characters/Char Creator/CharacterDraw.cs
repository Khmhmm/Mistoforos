using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDraw : MonoBehaviour {
    public GUISkin skin;

    public GUIStyle style;

    public List<GUIStyle> haircutStyles;
    private int currentHairCutStyle;

    public List<GUIStyle> eyesStyle;
    private int currentEyesStyle;

    public List<GUIStyle> noseStyle;
    private int currentNoseStyle;

    public List<GUIStyle> mouthStyle;
    private int currentMouthStyle;

    void Start()
    {
        currentEyesStyle = 0;
        currentNoseStyle = 0;
        currentMouthStyle = 0;
        currentHairCutStyle = 0;
    }

    void OnGUI()
    {
        /*
        //skin (background)
        GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.2f, 275f, 275f), "", style);
        //TODO: сделать константные оффсеты

        //haircut
        GUI.Box(new Rect(Screen.width * 0.5f - 100f, Screen.height * 0.2f, 200f, Screen.height*0.1f), "Hair",haircutStyles[currentHairCutStyle]);
        //eyes
        GUI.Box(new Rect(Screen.width * 0.5f - 150/2, Screen.height * 0.3f, 150f, 40f),"eyes",eyesStyle[currentEyesStyle]);
        //nose
        GUI.Box(new Rect(Screen.width * 0.5f - 20, Screen.height * 0.4f, 40, 40f), "nose",noseStyle[currentNoseStyle]);
        //mouth
        GUI.Box(new Rect(Screen.width * 0.5f - 20, Screen.height * 0.5f, 40f, 40f), "mouth",mouthStyle[currentMouthStyle]);

    */

        GUI.skin = skin;

        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.1f + 240f, 100, 40f), "Haircut");
        if (GUI.Button(new Rect(Screen.width * 0.1f + 110f, Screen.height * 0.1f +240f, 40f, 40f), "<"))
        {
            currentHairCutStyle = (currentHairCutStyle== 0) ? haircutStyles.Count - 1 : --currentHairCutStyle;
        }
        if (GUI.Button(new Rect(Screen.width * 0.1f + 150f, Screen.height * 0.1f + 240f, 40f, 40f), ">"))
        {
            currentHairCutStyle = (currentHairCutStyle == haircutStyles.Count - 1) ? 0 : ++currentHairCutStyle;
        }

        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.1f, 100, 40f), "Eyes");
        if (GUI.Button(new Rect(Screen.width * 0.1f + 110f, Screen.height * 0.1f, 40f, 40f), "<"))
        {
            currentEyesStyle = (currentEyesStyle == 0) ? eyesStyle.Count - 1 : --currentEyesStyle;
        }
        if (GUI.Button(new Rect(Screen.width * 0.1f + 150f, Screen.height * 0.1f, 40f, 40f), ">"))
        {
            currentEyesStyle = (currentEyesStyle == eyesStyle.Count - 1) ? 0 : ++currentEyesStyle;
        }

        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.1f + 80f, 100, 40f), "Nose");
        if (GUI.Button(new Rect(Screen.width * 0.1f + 110f, Screen.height * 0.1f + 80f, 40f, 40f), "<"))
        {
            currentNoseStyle = (currentNoseStyle == 0) ? noseStyle.Count - 1 : --currentNoseStyle;
        }
        if (GUI.Button(new Rect(Screen.width * 0.1f + 150f, Screen.height * 0.1f + 80f, 40f, 40f), ">"))
        {
            currentNoseStyle = (currentNoseStyle == noseStyle.Count - 1) ? 0 : ++currentNoseStyle;
        }

        GUI.Label(new Rect(Screen.width * 0.1f, Screen.height * 0.1f + 160f, 100, 40f), "Mouth");
        if (GUI.Button(new Rect(Screen.width * 0.1f + 110f, Screen.height * 0.1f + 160f, 40f, 40f), "<"))
        {
            currentMouthStyle = (currentMouthStyle == 0) ? mouthStyle.Count - 1 : --currentMouthStyle;
        }
        if (GUI.Button(new Rect(Screen.width * 0.1f + 150f, Screen.height * 0.1f + 160f, 40f, 40f), ">"))
        {
            currentMouthStyle = (currentMouthStyle == mouthStyle.Count - 1) ? 0 : ++currentMouthStyle;
        }
    }
}
