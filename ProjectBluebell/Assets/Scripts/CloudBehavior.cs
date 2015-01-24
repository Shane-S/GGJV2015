using UnityEngine;
using System.Collections;

public class CloudBehavior : MonoBehaviour {

    public GameObject world;

	// Use this for initialization
	void Start () {
        //this.transform.parent = world.transform;
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(world.transform.position, new Vector3(0.0f, 0.0f, 1.0f), 0.02f);
	}
}
