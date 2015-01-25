using UnityEngine;
using System.Collections;
public class GUIDraw : MonoBehaviour
{
    private float loadBarProgress = 0.0f;
    private const float loadBarSpeed = 0.25f;
    public Texture loadBarTexture = null;
    void OnGUI()
    {
        //Draw loading bar with offset texture coordinates
        GUI.DrawTextureWithTexCoords(new Rect(Screen.width * 0.1f, Screen.height * 0.05f, Screen.width * 0.8f, Screen.height * 0.05f), loadBarTexture, new Rect(loadBarProgress, 0.0f, 1.0f, 1.0f), false);
    }
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        //Move the bar along; keep its position between zero and one for best float point precision
        loadBarProgress += Time.deltaTime * loadBarSpeed;
        if (loadBarProgress >= 1.0f) loadBarProgress -= 1.0f;
    }
}