using UnityEngine;
using System.Collections;

public class GameStateBehaviour : MonoBehaviour
{
    #region Possible Game States
    public const int NOT_FINISHED = -1;
    public const int LOST = 0;
    public const int WON = 1;
    #endregion
    #region Current Game State
    private bool finished = false;
    private int result = -1;
    #endregion
    #region End Scenes
    public string failState;
    public string winState;
    #endregion
    #region References to Other Components
    private Hunger hungerLevel;           // Reference to the hunger component in ScoreMeter
    private GameTextHandler textInput;    // Reference to the text box
    private PlayerBehaviourScript player; // Reference to the player
    private GlobalState globals;          // Reference to the script with level & prefab info
    #endregion
    public int selectedVeggie;
    private LevelProperties thisLevel;
    private System.Random random;
    private string selectedValid;

    void Start () {
        hungerLevel = GameObject.Find("ScoreMeter").GetComponent<Hunger>();
        textInput = GameObject.Find("GUIManager").GetComponent<GameTextHandler>();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviourScript>();
        globals = GameObject.Find("Globals").GetComponent<GlobalState>();

        random = new System.Random();
        thisLevel = globals.levels[globals.currentLevel];
        getNextVeggie();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!finished)
        {
            checkForEndGame();
            if (textInput.textWasEntered())
                checkInput();
        }
	}

    /// <summary>
    /// Selects the next vegetable to be displayed.
    /// </summary>
    void getNextVeggie()
    {
        selectedVeggie = random.Next(0, thisLevel.veggies.Length);
        selectedValid = thisLevel.veggies[selectedVeggie].GetComponent<VeggieBehaviour>().validString;
    }

    /// <summary>
    /// Checks whether the input was a valid string and tells the player to plant the appropriate vegetable.
    /// </summary>
    void checkInput()
    {
        if (string.Equals(textInput.getInput(), selectedValid, System.StringComparison.CurrentCultureIgnoreCase))
        {
            hungerLevel.resetHungerTimer();
            player.PlantVeggie(selectedVeggie, false);
            hungerLevel.veggiesPlanted++;
            getNextVeggie();
        }
        else
        {
            player.PlantVeggie(selectedVeggie, true);
            textInput.showFeedback();
        }

        textInput.clearInput();
    }

    /// <summary>
    /// Checks whether the win or lose condition has happened and transitions to the appropriate
    /// state if so.
    /// </summary>
    void checkForEndGame()
    {
        finished = (hungerLevel.veggiesPlanted == thisLevel.veggiesToWin) || (hungerLevel.hunger >= 100);

        if(finished)
        {
            CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();
            hungerLevel.stopHungerTimer();

            if (hungerLevel.veggiesPlanted == thisLevel.veggiesToWin)
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
