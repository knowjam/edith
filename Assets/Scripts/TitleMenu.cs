using UnityEngine;
using System.Collections;

public class TitleMenu : EdMonoBehaviour
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
        var gg = GameObject.FindObjectOfType<GlobalGui>();
        if (gg.isFading)
        {
            GUI.enabled = false;
        }

        GUI.color = new Color(1, 1, 1, 1.0f - gg.fadeImageAlpha);

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 0, 130, 50), "Start", BtnSkin.button))
        {
            LoadLevelWithSceneFade("Stage1");
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 1, 130, 50), "Team", BtnSkin.button))
        {
            LoadLevelWithSceneFade("Team");
        }

        //if (Debug.isDebugBuild)
        {
            int i = 0;
            foreach (var s in testStages)
            {
                if (GUI.Button(new Rect(140 * (i%5), Screen.height / 2 + 40 + 50 * (2 + i/5), 130, 50), s, BtnSkin.button))
                {
                    LoadLevelWithSceneFade(s);
                }
                ++i;
            }
        }
    }
}
