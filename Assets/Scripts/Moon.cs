using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour
{
    [Range(0,1)]
    public float startRatio = 1; // 시작 시 비율 - 1.0이면 보름달, 0.0이면 사라짐

    [Range(0.1f,300.0f)]
    public float totalTime = 10; // 1.0에서 0.0으로 되는 시간 (초)

    public bool reversed; // 체크되어 있으면 사라진 상태에서 보름달이 되는 방향으로 진행됨

    float ratio;
    bool finished;

    void Start()
    {
        ratio = startRatio;
    }

    void Update()
    {
        ratio = Mathf.Clamp01(ratio - Time.deltaTime / totalTime * (reversed ? -1 : 1));

        if (!finished)
        {
            if (reversed && ratio >= 1.0f)
            {
                Debug.Log("Full Moon!");
                finished = true;
            }
            else if (!reversed && ratio <= 0)
            {
                Debug.Log("Moon disappeared!");
                finished = true;

                GameObject.Find("Player").GetComponent<Player>().RestartAtCheckpoint();
            }
        }
        
        float offsetX = 0.9f * ratio + 0.1f;
        
        renderer.material.SetTextureOffset("_Mask", new Vector2(offsetX, 0.1f));
    }

    void LateUpdate()
    {
        if (!GameObject.Find("Player"))
        {
            return;
        }

        var playerPositionX = GameObject.Find("Player").transform.position.x;
        var startPositionX = GameObject.Find("StartPosition").transform.position.x;
        var finishPositionX = GameObject.Find("FinishPosition").transform.position.x;

        var xRatio = (playerPositionX - startPositionX) / (finishPositionX - startPositionX);

        var pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * (1 - xRatio), 3.0f * Screen.height / 4.0f, 0));
        pos.z = 29;

        transform.position = pos;
    }
}
