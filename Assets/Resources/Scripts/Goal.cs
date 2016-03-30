using UnityEngine;
using System.Collections;

public class Goal: MonoBehaviour {

    // Inspector variables.
    public Color inactiveColor;
    public Color activeColor;
    public bool isActive = false;

    // References.
    private FillPatternController fillController;

    // Properties.
    public bool IsActive { get { return isActive; } }

	// Use this for initialization
	void Start ()
    {
        fillController = GameObject.Find("PatternController").GetComponent<FillPatternController>();
        if (isActive)
        {
            Activate();
        }
    }
	

	// Update is called once per frame
	void Update ()
    {
	    if (fillController.BlankStampCount == 0)
        {
            Activate();
        }
	}


    // Activate the goal.
    public void Activate()
    {
        GetComponent<SpriteRenderer>().color = activeColor;
        isActive = true;
    }
}
