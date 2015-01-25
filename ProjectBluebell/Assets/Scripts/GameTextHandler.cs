using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTextHandler : MonoBehaviour
{

    private char character;
    private string textPrompt;
    private string textInput;
    private string menuOptions;
    public string gameScene;
    public GUIStyle textInputBoxStyle;
    private float textInputHeight;
    private float textInputWidth;
    private bool handled;
    private Event previous;

    // Use this for initialization
    void Start()
    {
        textPrompt = "What do you want to do? ";
        textInput = "";
        SetDimensions();
        handled = false;
    }

    void SetDimensions()
    {
        textInputHeight = Screen.height / 20;
        textInputWidth = Screen.width / 12;
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

        GUI.Label(new Rect(Screen.width / 4f, Screen.height / 1.2f, textInputWidth, textInputHeight), textPrompt + textInput, textInputBoxStyle);
    }

    bool checkInputValidity()
    {

        textInput = "";

        if (string.Equals(textInput, "Plant a Carrot", System.StringComparison.CurrentCultureIgnoreCase))
        {
            PlantCarrot();
            return true;
        }

        return false;
    }

    private void PlantCarrot()
    {
        PlayerBehaviourScript playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviourScript>();

        if (playerBehaviour != null)
        {
            playerBehaviour.PlantCarrot();
        }
        else
        {
            Debug.LogWarning("PlayerBehaviourScript not found");
        }
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
            checkInputValidity();
        }
    }
}
