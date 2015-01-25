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
    private int maxLevel = 2;

    /// <summary>
    /// The array of vegetable pictures.
    /// </summary>
    public LevelProperties[] levels;

    public void changeCurrentLevel(int level)
    {
        if (level <= maxLevel)
            currentLevel = level; 
    }

	// Use this for initialization
	void Start () {
	
	}

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
