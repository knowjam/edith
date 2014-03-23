using UnityEngine;
using System.Collections;

public class MagpieStartup : MonoBehaviour
{
    public GameObject magpiePrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(magpiePrefab);
        }
    }
}
