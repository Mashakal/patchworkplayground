using UnityEngine;


[RequireComponent (typeof(PlayerController))]
public class UserInput : MonoBehaviour {

    // Significant keys.
    private KeyCode actionKey = KeyCode.UpArrow;    // The action key.
	private KeyCode cycleKey = KeyCode.DownArrow; 	// The cycle colors key (for now)
    private KeyCode pauseKey = KeyCode.Escape;      // The pause/menu key.
    private KeyCode levelKey = KeyCode.L;           // Loads the next level immediately.
    private KeyCode captureScreen = KeyCode.C;      // Capture a screen shot button, for development.

    // References.
    private PlayerController pController;
    private GameController gameController;
    private PauseMenu pauseController;
	private FillPatternController fillController;
	private ArduinoController arduinoController;
    
    // Private variables.
    private bool jump;
	private bool cyclePrev = false;


    private void Awake()
    {
        // Obtain the required references.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        pauseController = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        pController = GetComponent<PlayerController>();
		fillController = GameObject.Find ("PatternController").GetComponent<FillPatternController> ();
		arduinoController = GameObject.Find ("ArduinoLogic").GetComponent<ArduinoController> ();
    }

	
	// Use update for buttons and keypresses that are rapid (not held down).
	private void Update ()
    {
        // Look for a jump button press.
        //if (!jump)
        //{
        //    jump = Input.GetKeyDown(KeyCode.Space);
        //}

        if (Input.GetKeyDown(pauseKey))
        {
            gameController.ChangePauseState();
        }

		if ((Input.GetKey (cycleKey) || arduinoController.down) && cyclePrev == false) {
			fillController.CycleColor ();
			cyclePrev = true;
		} else if (!Input.GetKey (cycleKey) && !arduinoController.down) {
			cyclePrev = false;
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
			jump = (Input.GetKeyDown(KeyCode.Space) || arduinoController.special);

            // Get the horizontal movement.
            float hMove = Input.GetAxis("Horizontal");
			if (arduinoController.left) {
				hMove = hMove - 1;
			}
			if (arduinoController.right) {
				hMove = hMove + 1;
			}

            // Look for the action button.
			bool action = Input.GetKeyDown(actionKey) || arduinoController.up;

            // Look for Load the next level action.
            if (Input.GetKeyDown(levelKey))
            {
                gameController.LoadNextLevel();
                return;
            } else if (Input.GetKeyDown(captureScreen))
            {
                gameController.TakeScreenshot(1);
            }

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
