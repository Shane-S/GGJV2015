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


	// Use this for initialization
	void Start () {
        textBank = "Carrot carrot carrot cccccccaAaaarrrrrrRRroooottTTTtttt.";
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
        GUI.Label(new Rect(Screen.width / 8f, Screen.height / 1.2f, displayingTextWidth, displayingTextHeight), displayingText, displayingTextStyle);
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
        }
	    
	}
}
