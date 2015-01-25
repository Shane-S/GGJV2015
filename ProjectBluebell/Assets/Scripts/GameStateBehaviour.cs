using UnityEngine;
using System.Collections;

public class GameStateBehaviour : MonoBehaviour {
    public const int NOT_FINISHED = -1;
    public const int LOST = 0;
    public const int WON = 1;

    private bool finished = false;
    private int result = -1;

    public string failState;
    public string winState;

    public int carrotsToWin = 75;

    private Hunger hungerLevel;           // Reference to the hunger component in ScoreMeter
    private GameTextHandler textInput;    // Reference to the text box
    private PlayerBehaviourScript player; // Reference to the player

    private int updateCounter;
    private bool isAnimating;
   
	void Start () {
        hungerLevel = GameObject.Find("ScoreMeter").GetComponent<Hunger>();
        textInput = GameObject.Find("GUIManager").GetComponent<GameTextHandler>();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviourScript>();
        updateCounter = 0;
        isAnimating = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isAnimating)
        {
            updateCounter++;
            if(updateCounter >= 25)
            {
                Animator a = GameObject.Find("Arm").GetComponent<Animator>();
                a.SetBool("planting", false);
            }
        }

        if (!finished)
        {
            checkForEndGame();
            if (textInput.textWasEntered())
                checkInput();
        }
	}

    void checkInput()
    {
        if (string.Equals(textInput.getInput(), "Plant a Carrot", System.StringComparison.CurrentCultureIgnoreCase))
        {
            hungerLevel.resetHungerTimer();
            player.PlantCarrot();
            Animator a = GameObject.Find("Arm").GetComponent<Animator>();
            a.SetBool("planting", true);
            isAnimating = true;
            updateCounter = 0;
            hungerLevel.carrotsPlanted++;
        }
        else textInput.showFeedback();

        textInput.clearInput();
    }

    void checkForEndGame()
    {
        finished = (hungerLevel.carrotsPlanted == carrotsToWin) || (hungerLevel.hunger >= 100);

        if(finished)
        {
            CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();
            hungerLevel.stopHungerTimer();

            if (hungerLevel.carrotsPlanted == carrotsToWin)
            {
                result = WON;
                fade.FadeOut(WinGame);
            }
            else
            {
                result = LOST;
                fade.FadeOut(FailGame);
            }

        }
    }

    void WinGame()
    {
        Application.LoadLevel(winState);
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
