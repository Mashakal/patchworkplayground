using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Text.RegularExpressions;
using System.IO;

public class GameController : MonoBehaviour
{
    // Inspector variables.

    // Public variables.
    public GameState state;
    public enum GameState
    {
        Playing,
        StartLevel,
        EndLevel,
        Paused
    };

    // Private variables.
    private int currentLevel = 1;

    // References.
    public HUDController hudController;
    public FillPatternController patternController;

    // Use this for initialization
    void Start()
    {
        // Obtain the references we need.
        hudController = GameObject.Find("HUDCanvas").GetComponent<HUDController>();
        patternController = GameObject.Find("PatternController").GetComponent<FillPatternController>();

        // Set the initial game state.
        state = GameState.Playing;
    }


    // Swith from paused to playing or vice versa.
    public void ChangePauseState()
    {
        if (GameState.Paused == state)
        {
            Time.timeScale = 1;
            state = GameState.Playing;
        }
        else
        {
            Time.timeScale = 0f;
            state = GameState.Paused;
        }
    }


    // Load the next level/scene.
    public void LoadNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }


    // Sets the current level to the argument passed in and loads that level.
    public void LoadLevel(int pLevel)
    {
        currentLevel = pLevel;
        SceneManager.LoadScene(currentLevel);
    }


    // Take a generic screenshot.
    public void TakeScreenshot(int pSuperSize)
    {
        Debug.Log("TakeScreenshot was called.");
        // Determine the filename, based on the current date and time.
        string filename = DateTime.Now.ToString();
        // Remove spaces and colons from the filename.
        string pattern = "[' '|:|/]";
        filename = Regex.Replace(filename, pattern, "");
        // Make sure the screenshots are saved in the screenshots folder.
        filename = Path.GetFullPath("Screenshots/") + (filename + ".png");
        Application.CaptureScreenshot(filename);
    }
}