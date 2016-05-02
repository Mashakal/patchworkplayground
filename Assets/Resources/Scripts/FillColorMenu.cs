using UnityEngine;
using UnityEngine.UI;


public class FillColorMenu : MonoBehaviour {

    // References.
    private PauseMenu pauseMenu;
    private FillPatternController patternController;
    private Image displayImage;
    private Text displayText;
    private Text displayCount;

    // Private variables.
    private GameObject[] patterns;
    private int currentIndex = 0;


    // Use this for initialization.
    private void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        patternController = GameObject.Find("PatternController").GetComponent<FillPatternController>();
        displayImage = GameObject.Find("ColorImage").GetComponent<Image>();
        displayText = GameObject.Find("ColorName").GetComponent<Text>();
        displayCount = GameObject.Find("ColorCountText").GetComponent<Text>();
    }


    // Turn on the fill color menu.
    public void TurnOn()
    {
        this.gameObject.SetActive(true);
        patterns = patternController.AllPatterns;
        UpdatePatternDisplay();
    }


    // Turn off the fill color menu.
    public void TurnOff()
    {
        this.gameObject.SetActive(false);
        pauseMenu.ReturnToPauseMenu();
    }


    // Process the user input for the fill menu.
    public void ProcessUserInput(int vertical, int horizontal, bool enter)
    {
        // Allow both horizontal and vertical keys to move through menu selections.
        if (vertical != 0 || horizontal != 0)
        {
            if (patterns.Length != 0)
            {
                currentIndex = (currentIndex + 1) % patterns.Length;
                UpdatePatternDisplay();
            }
        }
        else if (enter)
        {
            patternController.FillPattern = patterns[currentIndex];
            TurnOff();
        }
    }


    // Update the pattern displayed in the menu.
    public void UpdatePatternDisplay()
    {
        if (patterns.Length != 0)
        {
            // Update the color text label.
            displayText.text = patterns[currentIndex].name;

            // Update the color of the display image.
            Color newColor = patterns[currentIndex].GetComponent<SpriteRenderer>().color;
            displayImage.color = newColor;
        }

        // Update the count display.
        displayCount.text = "count:" + patterns.Length;
    }
}
