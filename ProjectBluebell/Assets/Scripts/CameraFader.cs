using UnityEngine;
using System.Collections;
using System;

public class CameraFader : MonoBehaviour {

    public Color fadeColor = Color.black;
    public float fadeTime = 5f;
    private bool fadingOut;

	// Use this for initialization
	void Start () {
        fadingOut = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if (fadingOut)
        //{
        //    if (fadeTime <= 0.0f)
        //    {
        //        Respawn();
        //    }
        //    else
        //    {
        //        fadeTime -= Time.deltaTime;
        //    }
        //}
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
