using UnityEngine;
using UnityEngine.SceneManagement;


public class StartButton : MonoBehaviour {

    // References.
    private GameController gameController;

    
    private void Start()
    {
        //gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void StartAdventure()
    {
        //gameController.LoadLevel(1);
        SceneManager.LoadScene(2);
    }
}