using UnityEngine;
using System.Collections;

public class PlayerBehaviourScript : MonoBehaviour {
    
    public GameObject world;

    private int updateCounter;
    private bool isAnimating;
    private GameObject[] curVeggies;
    public AudioClip[] plantingClips;
    private int veggieToPlant;
    private bool veggieIsEvil;

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

                // Actually drop the plant on the scene
                GameObject c = (GameObject)Instantiate(curVeggies[veggieToPlant],
                                               this.transform.position - new Vector3(-0.3f, 0.2f, 0),
                                               this.transform.rotation);
                c.transform.parent = world.transform;
                
                // If the plant is evil, change its sprite to the evil version
                if(veggieIsEvil)
                {
                    SpriteRenderer r = c.GetComponent<SpriteRenderer>();
                    VeggieBehaviour v = c.GetComponent<VeggieBehaviour>();
                    r.sprite = v.evilVersion;
                }
            }
        }
    }

    public void PlantVeggie(int veggieIndex, bool isEvil)
    {
        audio.PlayOneShot(plantingClips[Random.Range(0, plantingClips.Length)]);
        veggieToPlant = veggieIndex;
        veggieIsEvil = isEvil;
        Animator a = GameObject.Find("Arm").GetComponent<Animator>();
        a.SetBool("planting", true);
        isAnimating = true;
        updateCounter = 0;
    }
}
