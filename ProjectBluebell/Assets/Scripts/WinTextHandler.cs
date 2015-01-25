using UnityEngine;
using System.Collections;

public class WinTextHandler : MonoBehaviour
{

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
    private bool hasWon;
    private int state;
    private bool fading;
    private enum states { playagain, mainmenu, exit, none };

    // Blinking Cursor
    private float m_TimeStamp;
    private bool cursor = false;
    private string cursorChar;
    private int maxStringLength = 50;

    private GlobalState gState;

    // Use this for initialization
    void Start()
    {
        storyString = "You successfully ended world hunger.";
        textPrompt = "What do you do now? ";
        textInput = "";
        SetDimensions();
        handled = false;
        hasWon = false;
        state = (int)states.none;
        fading = false;
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

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, textMenuOptionsWidth, textMenuOptionsHeight), storyString, menuOptionStyle);
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, textInputWidth, textInputHeight), textPrompt + textInput + cursorChar, textInputBoxStyle);
    }

    bool checkInputValidity()
    {
        if (string.Equals(textInput, "Yes", System.StringComparison.CurrentCultureIgnoreCase))
        {
            switch (state)
            {
                case (int)states.playagain:
                    {
                        PlayPressed();
                        break;
                    }
                case (int)states.mainmenu:
                    {
                        MainMenuPressed();
                        break;
                    }
                case (int)states.exit:
                    {
                        ExitPressed();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            textInput = "";
            return true;
        }
        else if (string.Equals(textInput, "No", System.StringComparison.CurrentCultureIgnoreCase))
        {
            switch (state)
            {
                case (int)states.playagain:
                    {
                        state = (int)states.mainmenu;
                        textPrompt = "Main Menu? ";
                        textInput = "";
                        break;
                    }
                case (int)states.mainmenu:
                    {
                        state = (int)states.exit;
                        textPrompt = "Exit? ";
                        textInput = "";
                        break;
                    }
                case (int)states.exit:
                    {
                        ExitPressed();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return true;
        }
       
        textInput = "";
        return false;
    }

    void PlayPressed()
    {
        fading = true;
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
        fading = true;
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
        fading = true;
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


    void StartGame()
    {
        Application.LoadLevel(gameScene);
        gState = GameObject.Find("Globals").GetComponent<GlobalState>();
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void MainMenu()
    {
        Application.LoadLevel(menuScene);
    }

    // Update is called once per frame
    void Update()
    {
        if(!fading)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                if (textInput.Length - 1 >= 0)
                    textInput = textInput.Remove(textInput.Length - 1);
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (!hasWon)
                    ShowWinState();
                else
                    checkInputValidity();
            }
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

    void ShowWinState()
    {
        string input = textInput;
        textInput = "";
        storyString = gState.playerName += " spent the rest of their life " + input;
        textPrompt = "Play Again? ";
        state = (int)states.playagain;
        hasWon = true;
    }
}
