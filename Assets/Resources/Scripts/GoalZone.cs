using UnityEngine;
using System.Collections;

public class GoalZone : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    
	}
	

	// Update is called once per frame
	void Update ()
    {
	    
	}


    public void CheckForGoalEnter(KeyCode actionKey)
    {
        if (Input.GetKeyUp(actionKey))
        {
            // End the level.
        }
    }
}
