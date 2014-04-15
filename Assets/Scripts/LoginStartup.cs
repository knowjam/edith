using UnityEngine;
using System.Collections;
using Facebook.MiniJSON;

public class LoginStartup : EdMonoBehaviour
{
    public GUISkin btnSkin;
    private bool isIniting = false;
    // Use this for initialization
    void Start()
    {
        
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
        isIniting = false;

        FB.Login("email", LoginCallback);
    }

    private void LoginCallback(FBResult result)
    {
        if (result.Error != null)
        {
            Debug.Log("Error Response:\n" + result.Error);

            isIniting = false;
        }
        else if (!FB.IsLoggedIn)
        {
            Debug.Log("Login cancelled by Player");

            isIniting = false;
        }
        else
        {
            Debug.Log("Login was successful! UserId=" + FB.UserId);

            FB.API("/me?fields=name,id,first_name,friends.limit(100).fields(first_name,id)", Facebook.HttpMethod.GET, UserCallback);
        }
    }

    private void UserCallback(FBResult result)
    {
        var dict = Json.Deserialize(result.Text) as IDictionary;
        Debug.Log("\nEnglish Name: " + dict["name"].ToString());
        isIniting = false;

        Player.playerName = dict["name"].ToString();

        LoadLevelWithSceneFade("MainMenu");
    }

    private void OnHideUnity(bool isGameShown)
    {
        Debug.Log("Is game showing? " + isGameShown);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        GUI.enabled = !isIniting && !FB.IsLoggedIn;

        var gg = GameObject.FindObjectOfType<GlobalGui>();
        if (gg.isFading)
        {
            GUI.enabled = false;
        }

        GUI.color = new Color(1, 1, 1, 1.0f - gg.fadeImageAlpha);

        if (GUI.Button(new Rect(Screen.width/2.0f - Screen.width/3.0f/2, Screen.height/2.0f - Screen.height/6.0f/2, Screen.width/3.0f, Screen.height/6.0f), FB.IsLoggedIn ? "로그인 중..." : "페이스북으로 로그인", btnSkin.button))
        {
            isIniting = true;
            FB.Init(OnInitComplete, OnHideUnity);
        }
    }
}
