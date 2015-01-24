using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour {
    public static string playerName = null;

	// Use this for initialization
	void Start () {
	
	}

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
