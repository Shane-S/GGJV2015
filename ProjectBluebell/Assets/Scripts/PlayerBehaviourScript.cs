using UnityEngine;
using System.Collections;

public class PlayerBehaviourScript : MonoBehaviour {
    
    public GameObject world;

    private int updateCounter;
    private bool isAnimating;
    private GameObject[] curVeggies;
    public AudioClip[] plantingClips;

	// Use this for initialization
	void Start () {
        GlobalState state = GameObject.Find("Globals").GetComponent<GlobalState>();
        curVeggies = state.levels[state.currentLevel].veggies;
        updateCounter = 0;
        isAnimating = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isAnimating)
        {
            updateCounter++;
            if (updateCounter >= 25)
            {
                Animator a = GameObject.Find("Arm").GetComponent<Animator>();
                a.SetBool("planting", false);
                isAnimating = false;
            }
        }
    }

    public void PlantVeggie(int veggieIndex)
    {
        audio.PlayOneShot(plantingClips[Random.Range(0, plantingClips.Length)]);
        Animator a = GameObject.Find("Arm").GetComponent<Animator>();
        a.SetBool("planting", true);
        isAnimating = true;
        updateCounter = 0;

        GameObject c = (GameObject)Instantiate(curVeggies[veggieIndex], 
                                               this.transform.position - new Vector3(-0.3f, 0.2f, 0), 
                                               this.transform.rotation);
        c.transform.parent = world.transform;
    }
}
