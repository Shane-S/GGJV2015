using UnityEngine;
using System.Collections;
public class GUIDraw : MonoBehaviour
{
    private static float METER_HEIGHT = 30f;

    public Vector2 position = new Vector2(16, 16); // The screen position of the worldHunger bar
    public Texture2D meterFill;                    // The fill for the worldHunger bar
    public GUIStyle hungerFontStyle;
    public int borderDimensions; // The height and width of the worldHunger bar border (in pixels)

    private Rect meterDims; // Actual dimensions of worldHunger meter (stretches w/ screen)
    private GUIStyle fillStyle;
    private GUIStyle outsideStyle;

    private Hunger hungerLevel;
    private GlobalState gState;
    private GameStateBehaviour curState;

    private float fillPercent = 0.5f;

    void Awake()
    {
        hungerLevel = GameObject.Find("ScoreMeter").GetComponent<Hunger>();
        gState = GameObject.Find("Globals").GetComponent<GlobalState>();
        curState = GameObject.Find("GameState").GetComponent<GameStateBehaviour>();
        meterDims = new Rect(position.x, position.y, Screen.width - position.x * 2, METER_HEIGHT);

        fillStyle = new GUIStyle();
        fillStyle.border = new RectOffset(0, 0, 0, 0);

        outsideStyle = new GUIStyle();
        outsideStyle.border = new RectOffset(borderDimensions, borderDimensions, borderDimensions, borderDimensions);
    }

    void OnGUI()
    {
        DrawStarvationMeterText();
    }

    /// <summary>
    /// Shows the score as text (for now).
    /// </summary>
    void DrawStarvationMeterText()
    {
        string curVeggie = gState.levels[gState.currentLevel].veggies[curState.selectedVeggie].name;
        GUI.Label(meterDims, "Starvation level: " + (int)hungerLevel.hunger + '\n' +
                             "Veggies planted: " + hungerLevel.veggiesPlanted + '\n' +
                             "Current level: " + (gState.currentLevel + 1) + '\n' + 
                             "Current veggie: " + curVeggie,
                             hungerFontStyle);
    }

    /// <summary>
    /// Draws the world hunger meter.
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
}