using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class BackgroundColorBlender : MonoBehaviour
{
    [Range(0, 1)]
    public float blend;
    public float blendSpeed = 0.2f;
    public GUISkin BtnSkin;

    // Update is called once per frame
    void Update()
    {
        blend = Mathf.Clamp01(blend - blendSpeed * Time.deltaTime);

        foreach (var go in GameObject.FindGameObjectsWithTag("Background"))
        {
            go.GetComponent<Renderer>().sharedMaterial.SetFloat("_Blend", blend);
        }
    }

    void OnGUI()
    {
        if (Debug.isDebugBuild)
        {
            if (GUI.Button(new Rect(Screen.width - 100, 50, 100, 50), "Toggle", BtnSkin.button))
            {
                blendSpeed = -blendSpeed;
            }

            GUI.Label(new Rect(Screen.width - 100, 100, 100, 50), new GUIContent(blend.ToString()));
        }
    }
}
