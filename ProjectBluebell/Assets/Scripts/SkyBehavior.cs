using UnityEngine;
using System.Collections;

public class SkyBehavior : MonoBehaviour {

    public GameObject world;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.parent = world.transform;
	}
}
