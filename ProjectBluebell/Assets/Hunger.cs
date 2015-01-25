using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour {

    /// <summary>
    /// The time (in seconds) for the hunger count to increase.
    /// </summary>
    public float interval;
    
    /// <summary>
    /// The amount by which hunger increases per auto-increment interval.
    /// </summary>
    public float increasePerInterval;

    /// <summary>
    /// The current world hunger level.
    /// </summary>
    public float hunger = 0;


    // Time until the next auto-increment
    private float timeToIncrease = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeToIncrease += Time.deltaTime;
        if (timeToIncrease >= interval)
        {
            hunger += increasePerInterval;
            timeToIncrease = 0;
        }      
	}

    /// <summary>
    /// Resets the hunger's auto-increment timer.
    /// </summary>
    public void resetHungerTimer()
    {
        timeToIncrease = 0;
    }

    /// <summary>
    /// Gets the amount of time (in milliseconds) until the hunger meter will auto-increment.
    /// </summary>
    /// <returns>Time in millisconds to the next increase.</returns>
    public float getTimeToIncrease()
    {
        return timeToIncrease;
    }
}
