using UnityEngine;
using System.Collections;

public class SkipScene : MonoBehaviour {

    public string gameScene;
    public bool fadeOut;
    private bool fadingOut;

	// Use this for initialization
	void Start () {
        fadingOut = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            if (fadeOut && !fadingOut)
            {
                CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();

                if (fade != null)
                {
                    fade.FadeOut(NextScene);
                }
                else
                {
                    Debug.LogWarning("CameraFader not found");
                }

                fadingOut = true;
            }
            else
            {
                NextScene();
            }
        }
	}

    void NextScene()
    {
        Application.LoadLevel(gameScene);
    }
}
