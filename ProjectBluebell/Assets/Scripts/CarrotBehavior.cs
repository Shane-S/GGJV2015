using UnityEngine;
using System.Collections;

public class CarrotBehavior : MonoBehaviour {
    
    private bool _isSeen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (renderer.isVisible)
            _isSeen = true;

        if (_isSeen && !renderer.isVisible)
            Destroy(gameObject);
	}
}
