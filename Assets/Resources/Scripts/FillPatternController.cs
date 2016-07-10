using UnityEngine;
using System.Collections.Generic;

public class FillPatternController : MonoBehaviour {

    // Public variables.
    public GameObject blankStamp;             // Defined only when the player is standing on a blank stamp that can be filled.
	public int currentFillIndex = 0;
    // Private variables.
    private List<GameObject> allPatterns;     // Holds every pattern the player has obtained.
    private GameObject fillPattern;           // The current pattern to be used for filling in stamps.
    private int blankStampCount;              // How many blank stamps have not been filled within the current level.

    // Properties.
    public GameObject FillPattern { get { return fillPattern; } set { fillPattern = value; } }
    public GameObject[] AllPatterns { get { return allPatterns.ToArray(); } }
    public int BlankStampCount { get { return blankStampCount; } }

    // Use this for initialization
    void Start ()
    {
        allPatterns = new List<GameObject>();
        blankStampCount = CountBlankStampsInLevel();
    }


    public void AddPatternToCollection(GameObject pPattern)
    {
        allPatterns.Add(pPattern);

		// update to newest color
		fillPattern = pPattern;
		currentFillIndex = allPatterns.Count - 1;

        pPattern.SetActive(false);
    }

	public void CycleColor()
	{
		if (allPatterns.Count > 0) {
			currentFillIndex = (currentFillIndex + 1) % allPatterns.Count;
			fillPattern = allPatterns [currentFillIndex];
		}
	}

    // Fills in the current blank stamp, should only be called when the player is in front of a blank stamp.
    public void FillBlankStamp()
    {
        blankStamp.GetComponent<SpriteRenderer>().color = fillPattern.GetComponent<SpriteRenderer>().color;
        blankStamp = null;
        blankStampCount--;
    }


    // Returns the INITIAL number of blank stamps in the current level.
    private int CountBlankStampsInLevel()
    {
        return GameObject.FindGameObjectsWithTag("BlankStamp").Length;
    }


    // Should be called when a new level is being loaded, prepares the script for a new level.
    public void InitLevel()
    {
        blankStampCount = CountBlankStampsInLevel();
    }
}