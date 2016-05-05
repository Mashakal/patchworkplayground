using UnityEngine;
using System.Collections;

public class OnSceneLoad : MonoBehaviour {

    // References.
    Vector3 startingPosition;
    GameObject player;

	// Use this for initialization
	void Start ()
    {
        startingPosition = GetComponentInChildren<Transform>().position;
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Transform>().position = startingPosition;
        player.GetComponent<JumpStamper>().Reset();
        player.GetComponent<TrailingStamps>().Reset();
	}
}
