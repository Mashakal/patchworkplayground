using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public void StartAdventure()
    {
        SceneManager.LoadScene("betaLevel1");
    }
}