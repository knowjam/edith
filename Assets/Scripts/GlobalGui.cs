using UnityEngine;
using System.Collections;

public class GlobalGui : EdMonoBehaviour
{
    public GUISkin BtnSkin;
    public Texture fadeImage;
    public float fadeImageAlpha { get { return _fadeImageAlpha; } }
    private float _fadeImageAlpha;
    public float fadeSpeed = 1;
    private int currentFadeSign = -1;
    public bool showRetry;
    public bool showExit;

    void Start()
    {
        _fadeImageAlpha = 1;
    }

    void Update()
    {
        _fadeImageAlpha = Mathf.Clamp(fadeImageAlpha + currentFadeSign * fadeSpeed * Time.deltaTime, 0, 1);
    }

    public bool isFading { get { return currentFadeSign == 1 || fadeImageAlpha > 0; } }

    void OnGUI()
    {
        if (isFading)
        {
            GUI.enabled = false;
        }

        GUI.Label(new Rect(0, 0, 100, 100), Player.playerName);

        if (showExit && GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Exit", BtnSkin.button))
        {
            LoadLevelWithSceneFade("MainMenu");
        }

        if (showRetry && GUI.Button(new Rect(Screen.width - 200, 0, 100, 50), "Retry", BtnSkin.button))
        {
            GameObject.Find("Player").GetComponent<Player>().RestartAtCheckpoint();
        }

        //if (Application.isEditor && GUI.Button(new Rect(Screen.width - 300, 0, 100, 50), "Fade", BtnSkin.button))
        //{
        //    StartCoroutine("StartFadeTest");
        //}

        if (fadeImage && fadeImageAlpha > 0)
        {
            GUI.color = new Color(1, 1, 1, fadeImageAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeImage, ScaleMode.StretchToFill, true);
        }
    }

    public void StartFade(float fadeDuration)
    {
        this.fadeSpeed = 1.0f / fadeDuration;
        currentFadeSign = 1;
    }

    IEnumerator StartFadeTest()
    {
        currentFadeSign = +1;

        yield return new WaitForSeconds(1.0f);

        currentFadeSign = -1;
    }
}
