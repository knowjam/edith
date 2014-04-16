using UnityEngine;
using System.Collections;

public class TitleMenu : EdMonoBehaviour
{
    public GUISkin BtnSkin;
    public bool enableTestStageMenu;
    public string[] testStages;

    void Start()
    {
		var player = GameObject.Find ("/Player");
		if (player)
		{
			Destroy(player);
		}
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

        //if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 0, Screen.width / 5.0f, Screen.width / 5.0f / 2), "Start", BtnSkin.button))
        GUILayout.BeginArea(new Rect(Screen.width / 1.6f, Screen.height / 3.3f, Screen.width / 4, Screen.height/2.2f));
        //if (GUILayout.Button("Start", BtnSkin.button, GUILayout.MaxWidth(Screen.width/4), GUILayout.MaxHeight(Screen.height/6)))
        if (GUILayout.Button("시작", BtnSkin.button, GUILayout.MaxHeight(Screen.height/5)))
        {
            LoadLevelWithSceneFade("Stage1");
        }

        GUILayout.Space(Screen.height / 7);
        
        //if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 1, Screen.width / 5.0f, Screen.width / 5.0f / 2), "Team", BtnSkin.button))
        if (GUILayout.Button("만든 사람들", BtnSkin.button))
        {
            LoadLevelWithSceneFade("Team");
        }
        GUILayout.EndArea();

        if (enableTestStageMenu)
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
