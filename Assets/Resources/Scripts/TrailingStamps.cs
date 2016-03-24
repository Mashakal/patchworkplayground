using UnityEngine;


public class TrailingStamps : Stamper {

    // Inspector variables.
    public GameObject trailingStamp;        // The sprite that should be rendered as the trailing move stamp.
    public int maxTrailingStamps;           // The maximum number of trailing stamps to be rendered.
    public GameObject container;            // The parent GameObject for which to add the trailing stamps created.

    // Private variables.
    private int stampIndex = 0;             // The index of the current trailing stamp being rendered.
    private GameObject[] allStamps;         // An array to hold the trailing stamps created during run-time.


    // Use this for initialization
    void Start ()
    {
        allStamps = new GameObject[maxTrailingStamps];
	}
	

    public void Stamp(Vector3 pTargetPosition)
    {
        string stampTag = "TrailingStamp";      // The tag to add to the sprite after it is created.
        Vector2 targetPosition;                 // The position for which to render the newly created sprite.
        float yDelta;                           // The vertical difference between player position and stamp position.
        float scaleMultiplier;                  // The value for which to multiple the local scale of the sprite.
        int randomIndex;                        // Used to get a random value from an array.
        GameObject newSprite;                   // The GameObject that will carry the sprite.

        // Possible offsets for rendering a stamp, to be added to the character's ground position.
        float[] possibleDeltaValues = { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
        // Possible multipliers to change the scale of the sprite that may be rendered.
        float[] possibleScaleValues = { 0.85f, 0.9f, 0.95f, 1.0f, 1.05f, 1.1f, 1.15f, 1.2f, 1.25f, 1.3f };

        // Extract a delta value for this stamp rendering, an offset along the y-axis of the player.
        randomIndex = (int)(UnityEngine.Random.value * possibleDeltaValues.Length);
        yDelta = possibleDeltaValues[randomIndex];

        // Use the character's GroundCheck to determine where to position this Sprite, using yDelta as an offset for the y-axis.
        targetPosition = new Vector2(pTargetPosition.x, pTargetPosition.y + yDelta);

        // Check if there is another stamp within this vacinity.
        if (!SearchVacinityForStamp(stampTag, targetPosition))  // SearchVacinity returns false if there is no stamp in the vacinity.
        {
            // If there is not, instantiate a new Sprite object.
            newSprite = Instantiate(trailingStamp);

            // Change its position.
            newSprite.transform.position = targetPosition;

            // Determine a random scale
            randomIndex = (int)(UnityEngine.Random.value * possibleScaleValues.Length);
            scaleMultiplier = possibleScaleValues[randomIndex];
            newSprite.transform.localScale *= scaleMultiplier;

            // Add the tag.
            newSprite.tag = stampTag;

            // Make it a child of the empty MoveStamp game object.
            newSprite.transform.parent = container.transform;

            // Add the stamp to the stamp array and destroy a stamp at the newly created stamps index, if it exists.
            allStamps = AddSprite(newSprite, allStamps, stampIndex);

            // Increment the stampIndex
            stampIndex = (stampIndex + 1) % maxTrailingStamps;
        }
    }
}
