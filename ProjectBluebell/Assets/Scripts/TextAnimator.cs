using UnityEngine;
using System.Collections;

public class TextAnimator : MonoBehaviour {

    private char character;
    private string displayingText;
    private string textBank;
    public GUIStyle displayingTextStyle;
    private float displayingTextHeight;
    private float displayingTextWidth;
    private float typingInterval;
    private float currentCount;
    private int index;
    public string gameScene;


	// Use this for initialization
	void Start () {
        textBank = "You are a farmer with a plentiful supply of carrots.\nThe world is hungry.\nWhat do you do now?\n\nPlant a carrot.";
        typingInterval = 0.1f;
        currentCount = 0f;
        index = 0;
	}

    void SetDimensions()
    {
        displayingTextStyle.border.left = (int)(Screen.width / 1.3);
        displayingTextHeight = Screen.height / 20;
        displayingTextWidth = Screen.width / 12;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 8f, Screen.height / 4f, displayingTextWidth, displayingTextHeight), displayingText, displayingTextStyle);
    }

    void FadeAway()
    {
        CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();

        if (fade != null)
        {
            fade.FadeOut(TriggerScene);
        }
        else
        {
            Debug.LogWarning("CameraFader not found");
        }
    }

    void TriggerScene()
    {
        Application.LoadLevel(gameScene);
    }
	
	// Update is called once per frame
	void Update () {

        if (index < textBank.Length)
        {
            if (currentCount < typingInterval)
            {
                currentCount += Time.deltaTime;
            }
            else
            {
                displayingText += textBank[index];
                index++;
                currentCount = 0;
            }

            if (index == textBank.Length)
            {
                FadeAway();
            }
        }
	    
	}
}
