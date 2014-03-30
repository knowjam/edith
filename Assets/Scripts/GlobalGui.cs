using UnityEngine;
using System.Collections;

public class GlobalGui : MonoBehaviour
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
        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Exit", BtnSkin.button))
        {
            Application.LoadLevel("MainMenu");
        }

        if (GUI.Button(new Rect(Screen.width - 200, 0, 100, 50), "Retry", BtnSkin.button))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
