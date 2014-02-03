using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour
{
    public GUISkin skin;
    public string[] text;
    public string buttonText;
    public Texture tex;
    public bool destoryAfterRead = true;
    private bool dialogOn;
    private int index;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (dialogOn)
        {
            
            GUILayout.BeginArea(new Rect(0, 3*Screen.height/4, Screen.width, Screen.height/4), skin.textArea);

            GUILayout.Label(text[index], skin.label);
            if (GUILayout.Button(buttonText, skin.button))
            {
                ++index;

                if (text.Length <= index)
                {
                    dialogOn = false;

                    if (destoryAfterRead)
                    {
                        Destroy(gameObject);
                    }
                }
            }

            GUILayout.EndArea();

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        }
    }

    void OnTriggerEnter2D()
    {
        dialogOn = true;
        index = 0;
    }

    void OnTriggerExit2D()
    {
        dialogOn = false;
    }
}
