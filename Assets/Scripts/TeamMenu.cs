using UnityEngine;
using System.Collections;

public class TeamMenu : EdMonoBehaviour
{
    public GUISkin BtnSkin;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 110/2, Screen.height - 100, 110, 50), "Back", BtnSkin.button))
        {
            LoadLevelWithSceneFade("MainMenu");
        }
    }
}
