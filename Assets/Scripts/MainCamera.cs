using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	public bool leftToRightScroll;
    public Transform[] backgrounds;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (leftToRightScroll)
		{
			float x = GameObject.Find("Player").transform.position.x;

            var oldPosition = transform.position;

			transform.position = new Vector3(
				x,
				transform.position.y,
				transform.position.z);

            var xDiff = oldPosition.x - transform.position.x;

            if (backgrounds != null)
            {
                foreach (var bg in backgrounds)
                {
                    if (bg)
                    {
                        // bg.transform.position.z의 값은 20~30까지임
                        var normalizedZ = (bg.transform.position.z - 20) / 10; // 이건 0~1
                        var xDiffRatio = normalizedZ;
                        bg.transform.Translate(-xDiff * xDiffRatio * 0.1f, 0, 0, Space.World);
                    }
                }
            }
		}
		else
		{
			float y = GameObject.Find("Player").transform.position.y;

			transform.position = new Vector3(
				transform.position.x,
				y,
				transform.position.z);
		}
	}
}