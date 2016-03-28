using UnityEngine;
using System.Collections.Generic;

public class FillPatternController : MonoBehaviour {

    // Public variables.
    public GameObject blankStamp;             // Defined only when the player is standing on a blank stamp that can be filled.

    // Private variables.
    private List<GameObject> allPatterns;     // Holds every pattern the player has obtained.
    private GameObject fillPattern;           // The current pattern to be used for filling in stamps.

    // References.
    private GameController gameController;    // A reference to the GameController script.

    // Properties.
    public GameObject FillPattern { get { return fillPattern; } set { fillPattern = value; } }
    public GameObject[] AllPatterns { get { return allPatterns.ToArray(); } }

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        allPatterns = new List<GameObject>();
    }


    public void AddPatternToCollection(GameObject pPattern)
    {
        allPatterns.Add(pPattern);

        // If there is no equipped pattern, equip this one.
        if (allPatterns.Count == 1)
        {
            fillPattern = pPattern;
        }

        pPattern.SetActive(false);
    }


    public void FillBlankStamp()
    {
        blankStamp.GetComponent<SpriteRenderer>().color = fillPattern.GetComponent<SpriteRenderer>().color;
        blankStamp = null;
    }
}
