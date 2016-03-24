using UnityEngine;
using System.Collections;

public class JumpStamper : Stamper {

    // Inspector variables.
    public GameObject jumpStamp;            // The sprite that should be rendered as the jump stamp.
    public int maxJumpStamps;               // The maximum number of jump stamps to be rendered.
    public GameObject container;            // The parent GameObject for which to add the jump stamps.

    // Private variables.
    private int stampIndex = 0;
    private GameObject[] allStamps;


	// Use this for initialization
	private void Start ()
    {
        allStamps = new GameObject[maxJumpStamps];
	}
	
    public void Stamp(Vector3 pTargetPosition)
    {
        string stampTag = "JumpStamp";          // The string tag to add to the Sprite after it is created.
        GameObject newSprite;                   // The GameObject that will carry the newly created sprite.

        // Instantiate the sprite object.
        newSprite = Instantiate(jumpStamp);
        // Change it's position.
        newSprite.transform.position = pTargetPosition;
        // Add the tag.
        newSprite.tag = stampTag;
        // Make it a child of the container GameObject.
        newSprite.transform.parent = container.transform;
        // Add the sprite to the tracking array.
        allStamps = AddSprite(newSprite, allStamps, stampIndex);

        // Increment the stampIndex
        stampIndex = (stampIndex + 1) % maxJumpStamps;
    }
}
