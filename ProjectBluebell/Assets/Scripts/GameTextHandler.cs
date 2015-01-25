using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTextHandler : MonoBehaviour
{

    private char character;
    private string textPrompt;
    private string textInput;
    private string textFeedback;
    private string menuOptions;
    public string gameScene;
    public GUIStyle textInputBoxStyle;
    public GUIStyle textFeedbackStyle;
    private float textInputHeight;
    private float textInputWidth;
    private float textFeedbackHeight;
    private float textFeedbackWidth;
    private bool handled;
    private Event previous;
    private float feedBackTime;
    private float feedBackTimeLeft;
    private bool displayFeedback;
    private bool enteredText; // Whether the player entered text that needs to be evaluated

    // Blinking Cursor
    private float m_TimeStamp;
    private bool cursor = false;
    private string cursorChar;
    private int maxStringLength = 124;

    // Use this for initialization
    void Start()
    {
        textPrompt = "What do you do now? ";
        textFeedback = "";
        textInput = "";
        cursorChar = "";
        SetDimensions();
        handled = false;
        feedBackTime = 2f;
        feedBackTimeLeft = 0;
        displayFeedback = false;
    }

    void SetDimensions()
    {
        textInputBoxStyle.border.left = (int)(Screen.width/1.3);
        textFeedbackStyle.border.left = (int)(Screen.width / 1.46);
        textInputHeight = Screen.height / 20;
        textInputWidth = Screen.width / 12;
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
                && e.character != '\t')
            {
                character = e.character;
                textInput += character;
            }

            e.Use();
        }

        GUI.Label(new Rect(Screen.width / 8f, Screen.height / 1.2f, textInputWidth, textInputHeight), textPrompt + textInput + cursorChar, textInputBoxStyle);

        if (displayFeedback)
        {
            GUI.Label(new Rect(Screen.width / 6f, Screen.height / 1.1f, textFeedbackWidth, textFeedbackHeight), textFeedback, textFeedbackStyle);
        }
    }

    /// <summary>
    /// Returns whether the player has entered text awaiting validation.
    /// </summary>
    /// <returns>Whether the player has entered text for validation.</returns>
    public bool textWasEntered()
    {
        return enteredText;
    }

    /// <summary>
    /// Clears the current input.
    /// </summary>
    public void clearInput()
    {
        enteredText = false;
        textInput = "";
    }

    public void showFeedback()
    {
        displayFeedback = true;
        feedBackTimeLeft = feedBackTime;
        textInput = "";
        textFeedback = "Say What??";
    }

    // Gets the text that was input.
    public string getInput()
    {
        return textInput;
    }

    private void ExitPressed()
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            if (textInput.Length - 1 >= 0)
                textInput = textInput.Remove(textInput.Length - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            enteredText = true;
        }

        if (feedBackTimeLeft >= 0)
        {
            feedBackTimeLeft -= Time.deltaTime;
        }
        else if (feedBackTimeLeft < 0)
        {
            displayFeedback = false;
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
