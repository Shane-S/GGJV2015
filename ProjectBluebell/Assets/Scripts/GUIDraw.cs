using UnityEngine;
using System.Collections;
public class GUIDraw : MonoBehaviour
{
    // Please assign a material that is using position and color.
    public Material material;
    public Rect position = new Rect(16, 16, 128, 24);
    public Color color = Color.red;

    void OnGUI()
    {
        DrawStarvationMeter();
    }

    /// <summary>
    /// Draws the starvation meter.
    /// </summary>
    void DrawStarvationMeter()
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

    /// <summary>
    /// Draws the degree of starvation within
    /// </summary>
    void DrawStarvationBar()
    {

    }
}