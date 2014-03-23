using UnityEngine;
using System.Collections;

public class Magpie : MonoBehaviour
{
    void Start()
    {
        //iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("MagpiePath"), "time", 4, "looptype", "loop", "easetype", "linear"));

        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("MagpieFlyAndWaitPath"), "time", 2, "easetype", "linear", "oncompletetarget", gameObject, "oncomplete", "OnLanded"));

        
    }

    void OnLanded()
    {
        StartCoroutine(WaitAndFlyAgain());
    }

    private IEnumerator WaitAndFlyAgain()
    {
        yield return new WaitForSeconds(2.0f);

        iTween.MoveTo(gameObject, iTween.Hash("path", iTweenPath.GetPath("MagpieFlyAgainPath"), "time", 2, "easetype", "linear", "oncompletetarget", gameObject, "oncomplete", "OnDisappeared"));
    }

    void OnDisappeared()
    {
        Destroy(gameObject);
    }
}
