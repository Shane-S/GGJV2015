using UnityEngine;
using System.Collections;

public class CloudBehavior : MonoBehaviour {

    public GameObject world;
    private float bounce;

	// Use this for initialization
	void Start () {
        //this.transform.parent = world.transform;
        bounce = 1f;
	}
	
	// Update is called once per frame
	void Update () {        
        transform.RotateAround(world.transform.position, new Vector3(0.0f, 0.0f, 1.0f), 0.02f);
       
	}
}
