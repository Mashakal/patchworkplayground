using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Inspector variables.  These defaults are the default fo Addie's Patchwork Playground.
    public float smoothTimeX = 0.3f;
    public float smoothTimeY = 0.4f;
    public float yMinCamPos = -6f;
    public float yMaxCamPos = 1000f;

    // Private variables.
    private GameObject player;
    private Vector2 velocity;


    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	

	void FixedUpdate ()
    {
        // Determine position based on smooth damp.
        float posX = Mathf.SmoothDamp(transform.position.x,
            player.transform.position.x,
            ref velocity.x,
            smoothTimeX);

        float posY = Mathf.SmoothDamp(transform.position.y,
            player.transform.position.y,
            ref velocity.y,
            smoothTimeY);

        // Don't let the camera go below or above the level detail.
        posY = Mathf.Clamp(posY, yMinCamPos, yMaxCamPos);

        // Update the position of the camera.
        transform.position = new Vector3(posX, posY, transform.position.z);
	}
}