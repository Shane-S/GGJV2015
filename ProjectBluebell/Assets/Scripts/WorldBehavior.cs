using UnityEngine;
using System.Collections;

public class WorldBehavior : MonoBehaviour {

    public float speed = 0.04f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed -= 0.01f;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed += 0.01f;
        }

        transform.Rotate(0.0f, 0.0f, speed);
	}
}
