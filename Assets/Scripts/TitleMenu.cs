using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour
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
        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40, 110, 50), "Start", BtnSkin.button))
        {
            Application.LoadLevel("Stage1");
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 90, 110, 50), "Team", BtnSkin.button))
        {
            Application.LoadLevel("Team");
        }
    }
}
