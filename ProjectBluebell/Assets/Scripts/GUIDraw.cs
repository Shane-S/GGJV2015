using UnityEngine;
using System.Collections;
public class GUIDraw : MonoBehaviour
{
    private static float METER_HEIGHT = 30f;

    public Vector2 position = new Vector2(16, 16); // The screen position of the worldHunger bar
    public Texture2D meterFill;                    // The fill for the worldHunger bar

    public int borderDimensions; // The height and width of the worldHunger bar border (in pixels)

    private Rect meterDims; // Actual dimensions of worldHunger meter (stretches w/ screen)
    private GUIStyle fillStyle;
    private GUIStyle outsideStyle;

    public float worldHunger = 0;

    private float fillPercent = 0.5f;

    void Start()
    {
        meterDims = new Rect(position.x, position.y, Screen.width - position.x * 2, METER_HEIGHT);

        fillStyle = new GUIStyle();
        fillStyle.border = new RectOffset(0, 0, 0, 0);

        outsideStyle = new GUIStyle();
        outsideStyle.border = new RectOffset(borderDimensions, borderDimensions, borderDimensions, borderDimensions);
    }

    void OnGUI()
    {
        DrawStarvationMeterText();
        //DrawStarvationMeter();
    }

    /// <summary>
    /// Shows the score as text (for now).
    /// </summary>
    void DrawStarvationMeterText()
    {
        GUI.Label(meterDims, "Starvation level: " + (int)worldHunger);
    }

    /// <summary>
    /// Draws the worldHunger meter.
    /// </summary>
    void DrawStarvationMeter()
    {
        GUI.BeginGroup(meterDims);
            GUI.Box(new Rect(0, 0, meterDims.width, meterDims.height), GUIContent.none, outsideStyle);

            // draw the filled-in part:
            GUI.BeginGroup(new Rect(0, 0, 500, meterDims.height));
                GUI.Box(new Rect(borderDimensions, borderDimensions,
                                 meterDims.width, meterDims.height - borderDimensions * 2),
                                 meterFill, fillStyle);
            GUI.EndGroup();
        GUI.EndGroup();
    }

    void Update()
    {
        if(worldHunger < 100)
            worldHunger += 0.05f;
    }
}