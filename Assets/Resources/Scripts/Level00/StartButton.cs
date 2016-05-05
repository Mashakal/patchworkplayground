using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour {

    // References.
    private GameController gameController;

    public void StartAdventure()
    {
        SceneManager.LoadScene(1);
    }
}