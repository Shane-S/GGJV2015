using UnityEngine;
using System.Collections;
using System;

public class CameraFader : MonoBehaviour {

    public Color fadeColor = Color.black;
    public float fadeTime = 5f;
    public bool fadeInOnStart;

	// Use this for initialization
	void Start () {
        if (fadeInOnStart)
        {
            FadeIn();
        }
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void FadeOut()
    {
        CameraFade.StartAlphaFade(fadeColor, false, fadeTime);
        //fadingOut = true;
    }

    public void FadeOut(Action onFadeFinish)
    {
        CameraFade.StartAlphaFade(fadeColor, false, fadeTime, 0.0f, onFadeFinish);
        //fadingOut = true;
    }

    public void FadeIn()
    {
        CameraFade.StartAlphaFade(fadeColor, true, fadeTime);
    }
}
