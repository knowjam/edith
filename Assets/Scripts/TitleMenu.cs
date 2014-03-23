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
        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 0, 130, 50), "Start", BtnSkin.button))
        {
            Application.LoadLevel("Stage1");
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 + 40 + 50 * 1, 130, 50), "Team", BtnSkin.button))
        {
            Application.LoadLevel("Team");
        }

        if (Debug.isDebugBuild)
        {
            if (GUI.Button(new Rect(Screen.width / 5 + 140 * 0, Screen.height / 2 + 40 + 50 * 2, 130, 50), "Stage 2", BtnSkin.button))
            {
                Application.LoadLevel("Stage2");
            }

            if (GUI.Button(new Rect(Screen.width / 5 + 140 * 1, Screen.height / 2 + 40 + 50 * 2, 130, 50), "Ghost", BtnSkin.button))
            {
                Application.LoadLevel("Ghost");
            }

            if (GUI.Button(new Rect(Screen.width / 5 + 140 * 2, Screen.height / 2 + 40 + 50 * 2, 130, 50), "Cogwheel", BtnSkin.button))
            {
                Application.LoadLevel("Cogwheel");
            }

            if (GUI.Button(new Rect(Screen.width / 5 + 140 * 3, Screen.height / 2 + 40 + 50 * 2, 130, 50), "Magpie", BtnSkin.button))
            {
                Application.LoadLevel("Magpie");
            }

            if (GUI.Button(new Rect(Screen.width / 5 + 140 * 4, Screen.height / 2 + 40 + 50 * 2, 130, 50), "BearAttack", BtnSkin.button))
            {
                Application.LoadLevel("BearAttack");
            }
        }
    }
}
