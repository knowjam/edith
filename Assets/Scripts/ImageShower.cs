using UnityEngine;
using System.Collections;

public class ImageShower : MonoBehaviour
{
    public Texture[] tex;
    public bool destoryAfterRead = true;
    public GUISkin skin;
    public string[] text;
    public string buttonText;
    
    Texture curTex;
    bool dialogOn;
    int index;

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
        if (curTex)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), curTex);
        }

        if (dialogOn)
        {
            GUILayout.BeginArea(new Rect(0, 3 * Screen.height / 4, Screen.width, Screen.height / 4), skin.textArea);

            GUILayout.Label(text[index], skin.label);
            if (GUILayout.Button(buttonText, skin.button))
            {
                ++index;

                if (text.Length <= index)
                {
                    Destroy(GameObject.Find("Player"));
                    Application.LoadLevel("MainMenu");

                    dialogOn = false;

                    curTex = null;

                    if (destoryAfterRead)
                    {
                        Destroy(gameObject);
                    }
                }
            }

            GUILayout.EndArea();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine("ShowSequence");
        }
    }

    void OnTriggerExit2D()
    {
    }

    IEnumerator ShowSequence()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;
        //var player = GameObject.FindGameObjectWithTag("Player");
        //player.SetActive(false);

        foreach (var t in tex)
        {
            curTex = t;
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2.0f);

        dialogOn = true;

        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = true;

        //Destroy(gameObject);
        //player.SetActive(true);
    }
}
