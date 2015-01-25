using UnityEngine;
using System.Collections;
public class GUIDraw : MonoBehaviour
{
    private float loadBarProgress = 0.0f;
    private const float loadBarSpeed = 0.25f;
    public Texture loadBarTexture = null;

    private float percentage = 1;
    void OnGUI()
    {
        //Draw loading bar with offset texture coordinates
        GUI.DrawTexture(new Rect(Screen.width * 0.1f, Screen.height * 0.05f, Screen.width * 0.8f * percentage, Screen.height * 0.05f), loadBarTexture);
    }
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Hunger h = GameObject.Find("ScoreMeter").GetComponent<Hunger>();
        percentage = h.hunger / 100;
    }
}