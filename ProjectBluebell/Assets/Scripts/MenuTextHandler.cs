using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuTextHandler : MonoBehaviour {

    private char character;
    private string textPrompt;
    private string textInput;
    private string namePrompt;
    private string menuOptions;
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

    // Blinking Cursor
    private float m_TimeStamp;
    private bool cursor = false;
    private string cursorChar;
    private int maxStringLength = 50;
    private GlobalState gState;

    private string[] play = { "Play", "Start", "Begin", "Start Game", "Play Game", "Begin Game", "Launch", "Launch Game", "Plant a carrot" };
    private string[] exit = { "Exit", "Quit", "Stop", "Exit Game", "Quit Game", "Stop Game", "Escape", "Escape Game" };
    
	// Use this for initialization
	void Start () {
        menuOptions = "";
        textPrompt = "What do you do now? ";
        namePrompt = "What is your name? ";
        textInput = "";
        SetDimensions();
        handled = false;
        gState = GameObject.Find("Globals").GetComponent<GlobalState>();
        gState.playerName = null;
	}

    void SetDimensions()
    {
        textInputHeight = Screen.height / 20;
        textInputWidth = Screen.width / 12;
        textMenuOptionsHeight = Screen.height / 20;
        textMenuOptionsWidth = Screen.width / 12;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
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

        //GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, textMenuOptionsWidth, textMenuOptionsHeight), menuOptions, menuOptionStyle);
        
        // If the player hasn't entered their name, show the name prompt; otherwise, show the "What do you want to
        // do" prompt
        if(gState.playerName == null)
            GUI.Label(new Rect(Screen.width / 5, Screen.height / 4, textInputWidth, textInputHeight),
                      namePrompt + textInput + cursorChar, textInputBoxStyle);
        else
            GUI.Label(new Rect(Screen.width / 5, Screen.height / 4, textInputWidth, textInputHeight),
                      textPrompt + textInput + cursorChar, textInputBoxStyle);
    }

    bool checkInputValidity()
    {
        // Set the player's name if it hasn't yet been set
        if (gState.playerName == null)
        {
            Debug.Log("got here");
            gState.playerName = textInput;
            textInput = "";
            return true;
        }

        if (checkValidity(play))
        {
            PlayPressed();
            return true;
        }
        else if (checkValidity(exit))
        {
            ExitPressed();
            return true;
        }
        textInput = "";
        return false;
    }

    bool checkValidity(string[] words)
    {
        foreach(string s in words)
        {
            if (string.Equals(textInput, s, System.StringComparison.CurrentCultureIgnoreCase) ||
                textInput.Contains("plant") || textInput.Contains("Plant"))
                return true;
        }
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

    void StartGame()
    {
        Application.LoadLevel(gameScene);
    }

    void QuitGame()
    {
        Destroy(GameObject.Find("Globals"));
        Application.LoadLevel(menuScene);
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (textInput.Length - 1 >= 0)
                textInput = textInput.Remove(textInput.Length-1);
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
}
