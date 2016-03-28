using UnityEngine;


[RequireComponent (typeof(PlayerController))]
public class UserInput : MonoBehaviour {

    // Significant keys.
    private KeyCode actionKey = KeyCode.O;      // The action key.
    private KeyCode pauseKey = KeyCode.Escape;  // The pause/menu key.

    // References.
    private PlayerController pController;
    private GameController gameController;
    private PauseMenu pauseController;
    
    // Private variables.
    private bool jump;


    private void Awake()
    {
        // Obtain the required references.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        pauseController = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
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

        if (Input.GetKeyDown(pauseKey))
        {
            gameController.ChangePauseState();
        }

        if (GameController.GameState.Paused == gameController.state)
        {
            GetPlayerPauseInput();
        }
	}


    // Use FixedUpdate for other buttons and keypresses.
    private void FixedUpdate()
    {
        if (GameController.GameState.Playing == gameController.state)
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

    private void GetPlayerPauseInput()
    {
        // Look for switching between menu selections
        bool down = Input.GetKeyDown(KeyCode.DownArrow);
        bool up = Input.GetKeyDown(KeyCode.UpArrow);
        bool left = Input.GetKeyDown(KeyCode.LeftArrow);
        bool right = Input.GetKeyDown(KeyCode.RightArrow);

        // Convert the bools to ints.
        int vert = down ? 1 : up ? -1 : 0;
        int hor = right ? 1 : left ? -1 : 0;

        // Look for a choice being made (enter)
        bool enter = Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(actionKey);

        // Pass the inputs to the pause controller.
        pauseController.ProcessUserInput(vert, hor, enter);
    }
}
