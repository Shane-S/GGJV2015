using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour {

    public float hunger = 0;
    public string failState;
    public bool fading;

	// Use this for initialization
	void Start () {
        fading = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (!fading)
        {
            if (hunger < 100)
                hunger += 0.05f;

            if (hunger >= 100)
            {
                CameraFader fade = GameObject.Find("Main Camera").GetComponent<CameraFader>();

                if (fade != null)
                {
                    fading = true;
                    fade.FadeOut(FailGame);
                }
                else
                {
                    Debug.LogWarning("CameraFader not found");
                }
            }
        }
	}

    void FailGame()
    {
        Application.LoadLevel(failState);
    }
}
