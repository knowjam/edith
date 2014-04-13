using UnityEngine;
using System.Collections;

public class EdMonoBehaviour : MonoBehaviour
{
    protected void LoadLevelWithSceneFade(string name)
    {
        StartCoroutine("DoLoadLevelWithSceneFade", name);
    }

    IEnumerator DoLoadLevelWithSceneFade(string name)
    {
        var gg = GameObject.FindObjectOfType<GlobalGui>();
        if (!gg)
        {
            Debug.Log("Main Camera에 GlobalGui 스크립트가 붙어 있지 않아서 본 함수를 호출할 수 없다.");
            yield break;
        }

        var fadeDuration = 0.5f;
        gg.StartFade(fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        Application.LoadLevel(name);
    }
}
