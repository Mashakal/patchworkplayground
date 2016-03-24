using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
public class UserInput : MonoBehaviour {

    private KeyCode actionKey = KeyCode.O;      // The action key.
    private KeyCode pauseKey = KeyCode.Escape;  // The pause key.
    private PlayerController pController;
    private bool jump;


    private void Awake()
    {
        pController = GetComponent<PlayerController>();
    }

	
	// Use update for buttons and keypresses that are rapid (not held down).
	private void Update ()
    {
        // Look for a jump button press.
        if (!jump)
        {
            jump = Input.GetKeyDown(KeyCode.Space);
        }
	}


    // Use FixedUpdate for other buttons and keypresses.
    private void FixedUpdate()
    {
        // Get the horizontal movement.
        float hMove = Input.GetAxis("Horizontal");

        // Look for the action button.
        bool action = Input.GetKeyDown(actionKey);

        // Pass all input parameters to the player controller.
        pController.ProcessUserInput(hMove, jump, action);

        // Reset jump value.
        jump = false;
    }
}
