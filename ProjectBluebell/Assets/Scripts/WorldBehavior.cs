using UnityEngine;
using System.Collections;

public class WorldBehavior : MonoBehaviour {

    public float speed = 0.04f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.RotateAround(this.transform.position, new Vector3(0, 0, 1), speed);//(0.0f, 0.0f, speed);
	}
}
