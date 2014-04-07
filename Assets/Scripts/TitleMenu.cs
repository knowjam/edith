using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour
{
    public GUISkin BtnSkin;
    public string[] testStages;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 0, 130, 50), "Start", BtnSkin.button))
        {
            Application.LoadLevel("Stage1");
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 1, 130, 50), "Team", BtnSkin.button))
        {
            Application.LoadLevel("Team");
        }

        //if (Debug.isDebugBuild)
        {
            int i = 0;
            foreach (var s in testStages)
            {
                if (GUI.Button(new Rect(140 * (i%5), Screen.height / 2 + 40 + 50 * (2 + i/5), 130, 50), s, BtnSkin.button))
                {
                    Application.LoadLevel(s);
                }
                ++i;
            }
        }
    }
}
