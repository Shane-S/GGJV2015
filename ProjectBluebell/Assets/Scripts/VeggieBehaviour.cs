using UnityEngine;
using System.Collections;

public class VeggieBehaviour : MonoBehaviour
{

    /// <summary>
    /// The valid string for planting this veggie (case insensitive).
    /// </summary>
    public string validString;

    /// <summary>
    /// The evil version of this veggie.
    /// </summary>
    public GameObject evilVersion;

    private bool _isSeen = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
            _isSeen = true;

        if (_isSeen && !renderer.isVisible)
            Destroy(gameObject);
    }
}
