using UnityEngine;
using System.Collections;

public class ImageShower : MonoBehaviour
{
    public Texture[] tex;
    private Texture curTex;
    
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
    }

    void OnTriggerEnter2D()
    {
        StartCoroutine("ShowSequence");
    }

    void OnTriggerExit2D()
    {
    }

    IEnumerator ShowSequence()
    {
        foreach (var t in tex)
        {
            curTex = t;
            yield return new WaitForSeconds(0.5f);
        }

        //curTex = null;
    }
}
