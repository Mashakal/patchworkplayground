using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    // Inspector variables.
    public Color selectedItemColor;
    public Color defaultColor;

    // Private variables.
    private GameObject[] menuSelections;
    private int selectedIndex = 0;

    private enum PauseState
    {
        PauseMenu,
        FillColorMenu
    };
    private PauseState state = PauseState.PauseMenu;

    // References.
    private GameController gameController;
    private FillColorMenu fillMenu;


	// Use this for initialization
	void Start ()
    {
        // Obtain the needed references.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        fillMenu = GameObject.Find("FillColorMenu").GetComponent<FillColorMenu>();

        // Initialize the needed components.
        menuSelections = GameObject.FindGameObjectsWithTag("MenuSelection");
	}

    
    // Turn the pause menu on.
    public void TurnOn()
    {
        if (PauseState.FillColorMenu == state)
        {
            fillMenu.TurnOff();
        }
        this.gameObject.SetActive(true);
    }


    // Process user input for the pause menu.
    public void ProcessUserInput(int vertical, int horizontal, bool enter)
    {
        if (PauseState.PauseMenu == state)
        {
            UnhighlightSelection();

            // Look for changing of vertical menuSelections.
            if (1 == vertical)
            {
                selectedIndex = (selectedIndex + 1) % menuSelections.Length;
            }
            else if (-1 == vertical)
            {
                selectedIndex -= 1;
                if (selectedIndex < 0)
                {
                    selectedIndex = menuSelections.Length - 1;
                }
            }

            // Look for a selection being made from the enter/return key being pressed.
            if (enter)
            {
                // Process the name of the selection, not it's text.
                ProcessSelection(menuSelections[selectedIndex].name);
            }

            HighlightSelection();
        }
        else if (PauseState.FillColorMenu == state)
        {
            // Pass the user input into the fill color menu script.
            fillMenu.ProcessUserInput(vertical, horizontal, enter);
        }
    }


    // Highlights the current selection.
    private void HighlightSelection()
    {
        menuSelections[selectedIndex].GetComponent<Text>().color = selectedItemColor;
        //menuSelections[selectedIndex].GetComponent<Text>().fontStyle = FontStyle.Bold;
    }


    // Unhighlights the current selection.
    private void UnhighlightSelection()
    {
        menuSelections[selectedIndex].GetComponent<Text>().color = defaultColor;
        //menuSelections[selectedIndex].GetComponent<Text>().fontStyle = FontStyle.Normal;
    }


    // Process the name of a pause menu selection (not it's text value).
    private void ProcessSelection(string pSelectionName)
    {
        switch (pSelectionName)
        {
            case "Resume": gameController.ChangePauseState(); break;
            case "ChooseFillColor": DisplayFillColorMenu(); break;
        }
    }


    // Display the fill color selection menu accessed through the pause menu.
    private void DisplayFillColorMenu()
    {
        // Hide the pause menu selections.
        HideMenuSelections();
        // Enable the fill color menu.
        fillMenu.TurnOn();
        // Change the pause menu state.
        state = PauseState.FillColorMenu;
    }


    // Return to the pause menu after making a fill color menu selection
    public void ReturnToPauseMenu()
    {
        UnhideMenuSelections();
        state = PauseState.PauseMenu;
    }


    // Hides each of the pause menu selections.
    private void HideMenuSelections()
    {
        for (int i = 0; i < menuSelections.Length; i++)
        {
            menuSelections[i].SetActive(false);
        }
    }


    // Unhide the pause menu selections
    private void UnhideMenuSelections()
    {
        for (int i = 0; i < menuSelections.Length; i++)
        {
            menuSelections[i].SetActive(true);
        }
    }
}