using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour {

    public float hunger = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (hunger < 100)
            hunger += 0.05f;
	}
}
