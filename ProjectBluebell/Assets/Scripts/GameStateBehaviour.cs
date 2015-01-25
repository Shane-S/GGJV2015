using UnityEngine;
using System.Collections;

public class GameStateBehaviour : MonoBehaviour {
    public const int NOT_FINISHED = -1;
    public const int LOST = 0;
    public const int WON = 1;

    private bool finished = false;
    private int result = -1;

    public string failState;

    private Hunger hungerLevel;           // Reference to the hunger component in ScoreMeter
    private GameTextHandler textInput;    // Reference to the text box
    private PlayerBehaviourScript player; // Reference to the player

	void Start () {
        hungerLevel = GameObject.Find("ScoreMeter").GetComponent<Hunger>();
        textInput = GameObject.Find("GUIManager").GetComponent<GameTextHandler>();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviourScript>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!finished)
            checkForEndGame();

        if (textInput.textWasEntered())
            checkInput();
	}

    void checkInput()
    {
        if (string.Equals(textInput.getInput(), "Plant a Carrot", System.StringComparison.CurrentCultureIgnoreCase))
        {
            hungerLevel.resetHungerTimer();
            player.PlantCarrot();
        }
        else textInput.showFeedback();

        textInput.clearInput();
    }

    void checkForEndGame()
    {
        if (hungerLevel.hunger >= 100)
        {
            // Stop the hunger from incrementing
            hungerLevel.stopHungerTimer();
            finished = true;
            result = LOST;

            CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();
            
            if (fade != null)
            {
                fade.FadeOut(FailGame);
            }
            else
            {
                Debug.LogWarning("CameraFader not found");
            }
        }
    }

    /// <summary>
    /// Fail the game
    /// </summary>
    void FailGame()
    {
        Application.LoadLevel(failState);
    }

    /// <summary>
    /// Gets the result of the game.
    /// </summary>
    /// <returns>Whether the player won, lost, or the game is still going.</returns>
    public int getResult()
    {
        return result;
    }
}
