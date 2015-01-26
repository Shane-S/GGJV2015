using UnityEngine;
using System.Collections;

[System.Serializable]
public class LevelProperties {
    public GameObject[] veggies;
    public int veggiesToWin;
    public float timePerTick;
    public float increasePerTick;
    public float startingHunger;
    public float increaseOnFailure;
    public string cutsceneText;
}