using UnityEngine;
using System.Collections;

public class PlayerRunner : MonoBehaviour
{
    public GUISkin skin;
    public string[] text;
    public string buttonText;

    bool dialogOn;
    int index;
    float targetVelocity;

    // Use this for initialization
    void Start()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = false;

        targetVelocity = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {

        transform.FindChild("PlayerRunnerMesh").GetComponent<Animator>().SetInteger("state", 1);

        GetComponent<Rigidbody2D>().velocity = new Vector2(targetVelocity, 0);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player Runner Remover")
        {
            // 플레이어 클론이 달려가는 연출이 끝나는 지점에 들어갔을 때

            Destroy(other.gameObject);

            // 더 달려가지 말자.
            targetVelocity = 0;

            // 대화창 시작!
            dialogOn = true;
        }
    }

    void OnGUI()
    {
        if (dialogOn)
        {
            GUILayout.BeginArea(new Rect(0, 3 * Screen.height / 4, Screen.width, Screen.height / 4), skin.textArea);

            GUILayout.Label(text[index], skin.label);
            if (GUILayout.Button(buttonText, skin.button))
            {
                ++index;

                if (text.Length <= index)
                {
                    // 플레이어 클론은 없앤다.
                    Destroy(gameObject);

                    dialogOn = false;

                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enabled = true;
                }
            }

            GUILayout.EndArea();
        }
    }
}
