using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour {

    // Private variables.
    private string fillPatternName;
    private GameController.GameState lastGameState;

    // References.
    private GameController gameController;
    private Image selectedFill;
    private GameObject pauseMenu;
    private GameObject fillMenu;

    // Use this for initialization
    void Start ()
    {
        // Obtain references.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        selectedFill = GameObject.Find("FillColor").GetComponent<Image>();
        pauseMenu = GameObject.Find("PauseMenu");
        fillMenu = GameObject.Find("FillColorMenu");

        // Disable special GUI elements now that we have obtained a reference to them.
        pauseMenu.SetActive(false);
        fillMenu.SetActive(false);
    }

	

    private	void Update ()
    {
        // Check if the state of the game has changed since the last frame.
        if (gameController.state != lastGameState)
        {
            // Update the last known game state.
            lastGameState = gameController.state;

            // Update the user interface based on the new game state.
            switch (lastGameState)
            {
                case GameController.GameState.Paused: DisplayPauseGUI(); break;
                case GameController.GameState.Playing: DisplayPlayingGUI(); break;
                default: break;
            }
        }

        // Update the selected fill pattern GUI icon, if it has changed.
        GameObject selectedPattern = gameController.patternController.FillPattern;
        if (selectedPattern != null && selectedPattern.name != fillPatternName)
        {
            SetFillPattern(gameController.patternController.FillPattern);
        }
	}


    // Update the fill pattern image.
    public void SetFillPattern(GameObject newPattern)
    {
        // Update the fill pattern display color to reflect the change.
        selectedFill.color = newPattern.GetComponent<SpriteRenderer>().color;
        fillPatternName = newPattern.name;
    }


    private void DisplayPauseGUI()
    {
        pauseMenu.GetComponent<PauseMenu>().TurnOn();
        //pauseMenu.SetActive(true);
    }


    private void DisplayPlayingGUI()
    {
        pauseMenu.SetActive(false);
        //pauseMenu.GetComponent<PauseMenu>().TurnOff();
    }

}
