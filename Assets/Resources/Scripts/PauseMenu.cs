using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    // Inspector variables.
    public Color selectedItemColor;
    public Color defaultColor;

    // Private variables.
    private GameObject[] menuSelections;
    private int selectedIndex = 0;

    // References.
    private GameController gameController;

	// Use this for initialization
	void Start ()
    {
        // Obtain the needed references.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        // Initialize the needed components.
        menuSelections = GameObject.FindGameObjectsWithTag("MenuSelection");
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}


    public void ProcessUserInput(int vertical, int horizontal, bool enter)
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
            ProcessSelection(menuSelections[selectedIndex].name);
        }

        HighlightSelection();
    }


    private void HighlightSelection()
    {
        menuSelections[selectedIndex].GetComponent<Text>().color = selectedItemColor;
        menuSelections[selectedIndex].GetComponent<Text>().fontStyle = FontStyle.Bold;
    }


    private void UnhighlightSelection()
    {
        menuSelections[selectedIndex].GetComponent<Text>().color = defaultColor;
        menuSelections[selectedIndex].GetComponent<Text>().fontStyle = FontStyle.Normal;
    }


    private void ProcessSelection(string pSelectionName)
    {
        switch (pSelectionName)
        {
            case "Resume": gameController.ChangePauseState(); break;
        }
    }
}
