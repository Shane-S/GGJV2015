using UnityEngine;
using System.Collections;

public class GlobalState : MonoBehaviour {

    /// <summary>
    /// The player's name.
    /// </summary>
    public string playerName = null;

    /// <summary>
    /// The current level.
    /// </summary>
    public int currentLevel = 0;

    /// <summary>
    /// The array of vegetable pictures.
    /// </summary>
    public LevelProperties[] levels;

	// Use this for initialization
	void Start () {
	
	}

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}

    public void reset()
    {
        playerName = null;
        currentLevel = 0;
    }
}
