using UnityEngine;


public class HUDController : MonoBehaviour {

    // Private variables.
    private string fillPatternName;
    private GameController.GameState lastGameState;

    // References.
    private GameController gameController;
    private SpriteRenderer selectedFill;
    private GameObject pauseMenu;

    // Use this for initialization
    void Start ()
    {
        // Obtain references.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        selectedFill = GameObject.Find("SelectedFillColor").GetComponent<SpriteRenderer>();
        pauseMenu = GameObject.Find("PauseMenu");

        // Disable special GUI elements now that we have obtained a reference to them.
        pauseMenu.SetActive(false);
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
    }


    //public void ProcessUserInput(int vertical, int horizontal, bool enter)
    //{
    //    UnhighlightSelection();

    //    // Look for changing of vertical selections.
    //    if (1 == vertical)
    //    {
    //        selectedIndex = (selectedIndex + 1) % selections.Length;
    //    }
    //    else if (-1 == vertical)
    //    {
    //        selectedIndex -= 1;
    //        if (selectedIndex < 0)
    //        {
    //            selectedIndex = selections.Length - 1;
    //        }
    //    }

    //    // Look for a selection being made from the enter/return key being pressed.
    //    if (enter)
    //    {
    //        ProcessSelection(selections[selectedIndex].name);
    //    }

    //    HighlightSelection();
    //}


    //private void ProcessSelection(string pSelectionName)
    //{
    //    switch (pSelectionName)
    //    {
    //        case "Resume": gameController.ChangePauseState(); break;
    //    }
    //}


    private void DisplayPauseGUI()
    {
        pauseMenu.SetActive(true);
    }


    //private void HighlightSelection()
    //{
    //    selections[selectedIndex].GetComponent<Text>().color = selectedItemColor;
    //}


    //private void UnhighlightSelection()
    //{
    //    selections[selectedIndex].GetComponent<Text>().color = defaultColor;
    //    //Debug.Log(selections[selectedIndex].GetComponent<Text>().text);
    //}


    private void DisplayPlayingGUI()
    {
        pauseMenu.SetActive(false);
    }

}
