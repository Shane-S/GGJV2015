using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuTextHandler : MonoBehaviour {

    private char character;
    private string textPrompt;
    private string textInput;
    private string menuOptions;
    public string gameScene;
    public GUIStyle textInputBoxStyle;
    public GUIStyle menuOptionStyle;
    private float textInputHeight;
    private float textInputWidth;
    private float textMenuOptionsHeight;
    private float textMenuOptionsWidth;
    private bool handled;
    private Event previous;

	// Use this for initialization
	void Start () {
        menuOptions = "|  Play  |  Exit  |";
        textPrompt = "What do you want to do? ";
        textInput = "";
        SetDimensions();
        handled = false;
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

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, textMenuOptionsWidth, textMenuOptionsHeight), menuOptions, menuOptionStyle);
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, textInputWidth, textInputHeight), textPrompt + textInput, textInputBoxStyle);
    }

    bool checkInputValidity()
    {
        if (string.Equals(textInput, "Play", System.StringComparison.CurrentCultureIgnoreCase))
        {
            PlayPressed();
            return true;
        }
        else if (string.Equals(textInput, "Exit", System.StringComparison.CurrentCultureIgnoreCase))
        {
            ExitPressed();
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

    void StartGame()
    {
        Application.LoadLevel(gameScene);
    }

    void QuitGame()
    {
        Application.Quit();
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
	}
}
