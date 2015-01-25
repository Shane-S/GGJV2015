using UnityEngine;
using System.Collections;

public class FailTextHandler : MonoBehaviour {

    private char character;
    private string textPrompt;
    private string textInput;
    private string storyString;
    public string gameScene; 
    public string menuScene;
    public GUIStyle textInputBoxStyle;
    public GUIStyle menuOptionStyle;
    private float textInputHeight;
    private float textInputWidth;
    private float textMenuOptionsHeight;
    private float textMenuOptionsWidth;
    private bool handled;
    private Event previous;
    public GameObject carrot;
    private float xPos = -5.5f;

    // Blinking Cursor
    private float m_TimeStamp;
    private bool cursor = false;
    private string cursorChar;
    private int maxStringLength = 50;

    // feedback text
    private string textFeedback;
    public GUIStyle textFeedbackStyle;
    private float textFeedbackHeight;
    private float textFeedbackWidth;
    private bool displayFeedback;
    private float feedBackTime;
    private float feedBackTimeLeft;

    private string[] play = { "Play", "Start", "Begin", "Start Game", "Play Game", "Begin Game", "Launch", "Launch Game", "Play Again", "Try Again", "replay", "Go Again", "Again" };
    private string[] exit = { "Exit", "Quit", "Stop", "Exit Game", "Quit Game", "Stop Game", "Escape", "Escape Game" };
    private string[] menu = { "Main Menu", "Menu", "Start Screen" };

    // Use this for initialization
    void Start()
    {
        storyString = "You failed to end world hunger.";
        textPrompt = "What do you do now? ";
        textInput = "";
        SetDimensions();
        handled = false;
        displayFeedback = false;
        feedBackTime = 2;
        feedBackTimeLeft = 0;
    }

    void SetDimensions()
    {
        textInputHeight = Screen.height / 20;
        textInputWidth = Screen.width / 12;
        textMenuOptionsHeight = Screen.height / 20;
        textMenuOptionsWidth = Screen.width / 12;
        textFeedbackHeight = Screen.height / 20;
        textFeedbackWidth = Screen.width / 12;
    }

    void OnGUI()
    {
        Event e = Event.current;

        if (e.isKey)
        {

            Debug.Log("char keycode: " + e.keyCode + " character: " + e.character);

            if (e.keyCode == KeyCode.None
                && e.character != '\n'
                && e.character != '\t'
                && (textPrompt + textInput).Length < maxStringLength)
            {
                character = e.character;
                textInput += character;
            }

            e.Use();
        }

        GUI.Label(new Rect(Screen.width / 5, Screen.height / 4, textMenuOptionsWidth, textMenuOptionsHeight), storyString, menuOptionStyle);
        GUI.Label(new Rect(Screen.width / 5, Screen.height / 2, textInputWidth, textInputHeight), textPrompt + textInput + cursorChar, textInputBoxStyle);
        
        if (displayFeedback)
        {
            GUI.Label(new Rect(Screen.width / 5, Screen.height / 1.8f, textFeedbackWidth, textFeedbackHeight), textFeedback, textFeedbackStyle);
        }
    }

    public void showFeedback()
    {
        displayFeedback = true;
        feedBackTimeLeft = feedBackTime;
        textInput = "";
        textFeedback = "Hint: Play again";
    }

    bool checkInputValidity()
    {
        if (checkValidity(play))
        {
            PlayPressed();
            textInput = "";
            return true;
        }
        else if (checkValidity(exit))
        {
            MainMenuPressed();
            textInput = "";
            return true;
        }
        else if (checkValidity(menu))
        {
            MainMenuPressed();
            textInput = "";
            return true;
        }
        else if (string.Equals(textInput, "Plant A Carrot", System.StringComparison.CurrentCultureIgnoreCase))
        {
            CarrotPressed();
            textInput = "";
            return true;
        }
        textInput = "";
        return false;
    }

    void PlayPressed()
    {
        CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();

        if (fade != null)
        {
            fade.FadeOut(StartGame);
        }
        else
        {
            Debug.LogWarning("CameraFader not found");
        }
    }

    void ExitPressed()
    {
        CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();

        if (fade != null)
        {
            fade.FadeOut(QuitGame);
        }
        else
        {
            Debug.LogWarning("CameraFader not found");
        }
    }

    void MainMenuPressed()
    {
        CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();

        if (fade != null)
        {
            fade.FadeOut(MainMenu);
        }
        else
        {
            Debug.LogWarning("CameraFader not found");
        }
    }

    void CarrotPressed()
    {
        Instantiate(carrot, new Vector3(xPos, -1.25f, 0), new Quaternion());
        if(xPos <= 15)
            xPos += 1;
    }


    void StartGame()
    {
        Application.LoadLevel(gameScene);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void MainMenu()
    {
        Destroy(GameObject.Find("Globals"));
        Application.LoadLevel(menuScene);
    }

    // Update is called once per frame
    void Update()
    {

        if (feedBackTimeLeft >= 0)
        {
            feedBackTimeLeft -= Time.deltaTime;
        }
        else if (feedBackTimeLeft < 0)
        {
            displayFeedback = false;
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (textInput.Length - 1 >= 0)
                textInput = textInput.Remove(textInput.Length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            checkInputValidity();
        }

        if (Time.time - m_TimeStamp >= 0.5)
        {
            m_TimeStamp = Time.time;
            if (cursor == false)
            {
                cursor = true;
                if ((textPrompt + textInput).Length < maxStringLength)
                {
                    cursorChar += "_";
                }
            }
            else
            {
                cursor = false;
                if (cursorChar.Length != 0)
                {
                    cursorChar = cursorChar.Substring(0, cursorChar.Length - 1);
                }
            }
        }
    }
    bool checkValidity(string[] words)
    {
        foreach (string s in words)
        {
            if (string.Equals(textInput, s, System.StringComparison.CurrentCultureIgnoreCase))
                return true;
        }
        showFeedback();
        return false;
    }
}
