using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelProperties {
    public GameObject[] veggies;
    public int veggiesToWin;
    public float hungerInterval;
    public float hungerIncreasePerTick;
    public string cutsceneText;
}