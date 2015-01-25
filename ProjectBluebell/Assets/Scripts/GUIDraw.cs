using UnityEngine;
using System.Collections;
public class GUIDraw : MonoBehaviour
{
    private float loadBarProgress = 0.0f;
    private const float loadBarSpeed = 0.25f;
    public Texture loadBarTexture = null;
    public Texture loadBarBackTexture = null;
    public GUIStyle worldHungerStyle;
    private string worldHungerText;

    private float percentage = 1;
    void OnGUI()
    {
        float width = Mathf.Clamp(Screen.width * 0.8f * percentage, 0, Screen.width * 0.8f);
        //Draw loading bar with offset texture coordinates
        GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.05f, Screen.width * 0.8f, Screen.height * 0.05f), loadBarBackTexture);
        GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.05f, width, Screen.height * 0.05f), loadBarTexture);
        GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.05f, Screen.width * 0.8f, Screen.height * 0.05f), worldHungerText, worldHungerStyle);
    }
    // Use this for initialization
    void Start()
    {
        worldHungerText = "World Hunger";
    }
    // Update is called once per frame
    void Update()
    {
        Hunger h = GameObject.Find("ScoreMeter").GetComponent<Hunger>();
        percentage = h.hunger / 100;
    }
}