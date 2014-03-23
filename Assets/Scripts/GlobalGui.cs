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
        if (GUI.Button(new Rect(0, 0, 100, 50), "Exit", BtnSkin.button))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
