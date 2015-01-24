using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {

    private char character;
    private string textPrompt;
    private string textInput;
    private string menuOptions;
    public GUIStyle textInputBoxStyle;
    public GUIStyle menuOptionStyle;
    private float textInputHeight;
    private float textInputWidth;
    private float textMenuOptionsHeight;
    private float textMenuOptionsWidth;

	// Use this for initialization
	void Start () {
        menuOptions = "| Play | Exit |";
        textPrompt = "What do you want to do? ";
        textInput = "";
        SetDimensions();
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

        if (e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Return)
        {
            Debug.Log("You pressed enter...");
            checkInputValidity();
        }
        else if (e.isKey)
        {
            character = e.character;
            textInput += character;
        }

        GUI.Label(new Rect(Screen.width / 4, Screen.height / 4, textMenuOptionsWidth, textMenuOptionsHeight), menuOptions, menuOptionStyle);
        GUI.Label(new Rect(Screen.width / 4, Screen.height / 2, textInputWidth, textInputHeight), textPrompt + textInput, textInputBoxStyle);
    }

    bool checkInputValidity()
    {
        if (string.Equals(textInput, "Play"))
        {
            Debug.Log("Play the game.");
            return true;
        }
        else if (string.Equals(textInput, "Exit"))
        {
            Debug.Log("Exit the game.");
            return true;
        }
        return false;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
