using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour {

    /// <summary>
    /// The time (in seconds) for the hunger count to increase.
    /// </summary>
    private float interval;
    
    /// <summary>
    /// The amount by which hunger increases per auto-increment interval.
    /// </summary>
    private float increasePerInterval;

    /// <summary>
    /// The current world hunger level.
    /// </summary>
    public float hunger = 0;

    /// <summary>
    /// The number of carrots planted.
    /// </summary>
    public float veggiesPlanted = 0;

    /// <summary>
    /// Time until the next auto-increment.
    /// </summary>
    private float timeToIncrease = 0;

    /// <summary>
    /// Whether the hunger timer is running.
    /// </summary>
    private bool running = true;

	// Use this for initialization
	void Start () {
        GlobalState current = GameObject.Find("Globals").GetComponent<GlobalState>();
        LevelProperties curLevel = current.levels[current.currentLevel];

        interval = curLevel.hungerInterval;
        increasePerInterval = curLevel.hungerIncreasePerTick;
	}
	
	// Update is called once per frame
	void Update () {
        if(!running) return;
		
        // Tick the timer and increment the hunger value if the timer value is above the specified interval
        timeToIncrease += Time.deltaTime;
		if (timeToIncrease >= interval)
		{
			hunger += increasePerInterval;
            hunger = hunger > 100 ? 100 : hunger;
			timeToIncrease = 0;
		}
	}

    /// <summary>
    /// Stops the hunger timer from ticking (and it can't be restarted).
    /// </summary>
    public void stopHungerTimer()
    {
        running = false;
    }

    /// <summary>
    /// Resets the hunger's auto-increment timer.
    /// </summary>
    public void resetHungerTimer()
    {
        timeToIncrease = 0;
    }

    /// <summary>
    /// Clamps values in the editor.
    /// </summary>
    void OnValidate()
    {
        veggiesPlanted = 0;
        interval = Mathf.Clamp(interval, 0.5f, 20);
        increasePerInterval = Mathf.Clamp(increasePerInterval, 1, 15);
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

    /// <summary>
    /// Gets the amount of time (in milliseconds) until the hunger meter will auto-increment.
    /// </summary>
    /// <returns>Time in millisconds to the next increase.</returns>
    public float getTimeToTick()
    {
        return timeToIncrease;
    }
}
