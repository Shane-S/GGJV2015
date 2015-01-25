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

    // Use this for initialization
    void Start()
    {
        storyString = "You successfully saved humanity.";
        textPrompt = "What do you want to do now? ";
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
                && e.character != '\t')
            {
                character = e.character;
                textInput += character;
            }

            e.Use();
        }

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, textMenuOptionsWidth, textMenuOptionsHeight), storyString, menuOptionStyle);
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, textInputWidth, textInputHeight), textPrompt + textInput, textInputBoxStyle);
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
        
    }

    void ShowWinState()
    {
        string input = textInput;
        textInput = "";
        storyString = GlobalState.playerName += " spent the rest of thier life " + input;
        textPrompt = "Play Again? ";
        state = (int)states.playagain;
        hasWon = true;
    }
}
