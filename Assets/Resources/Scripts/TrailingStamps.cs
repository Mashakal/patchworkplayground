using UnityEngine;


public class TrailingStamps : Stamper {

    // Inspector variables.
    public GameObject trailingStamp;        // The sprite that should be rendered as the trailing move stamp.
    public int maxTrailingStamps = 250;     // The maximum number of trailing stamps to be rendered.
    public GameObject container;            // The parent GameObject for which to add the trailing stamps created.

    // Private variables.
    private int stampIndex = 0;             // The index of the current trailing stamp being rendered.
    private GameObject[] allStamps;         // An array to hold the trailing stamps created during run-time.
    private string currentStyle;            // The string representation of the currently rendered trailing stamp.
    private float[] possibleDeltaValues;    // Possible vertical offsets, determined by the current stamp type.
    private float[] possibleScaleValues;    // Possible multipliers to change the scale of the sprite that may be rendered.

    // Paths.
    private string trailingStampsPath = "Prefabs/TrailingStamps/";

    // The string value for the trailing stamps of each patch type.
    private string trailingGrass = "GrassFlower";
    private string trailingBrick = "BrickTrellis";
    private string trailingWood = "WoodLeaf";


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

        // Extract a delta value for this stamp rendering, an offset along the y-axis of the player.
        randomIndex = (int)(UnityEngine.Random.value * possibleDeltaValues.Length);
        yDelta = possibleDeltaValues[randomIndex];

        // Use the character's GroundCheck to determine where to position this Sprite, using yDelta as an offset for the y-axis.
        targetPosition = new Vector2(pTargetPosition.x, pTargetPosition.y + yDelta);

        // Rendering a brick stamp is a special case.
        if (currentStyle != null && !currentStyle.Equals("Brick"))
        {
            // Check if there is another stamp within this vacinity, returns false if there is no stamp in the vacinity.
            if (!SearchVacinityForStamp(stampTag, targetPosition))
            {
                // Instantiate a new Sprite object.
                newSprite = Instantiate(trailingStamp);

                // Change its position.
                newSprite.transform.position = targetPosition;

                // Determine a random scale
                randomIndex = (int)(UnityEngine.Random.value * possibleScaleValues.Length);
                scaleMultiplier = possibleScaleValues[randomIndex];
                newSprite.transform.localScale *= scaleMultiplier;

                // Admin.
                newSprite.tag = stampTag;
                newSprite.transform.parent = container.transform;
                allStamps = AddSprite(newSprite, allStamps, stampIndex);
                stampIndex = (stampIndex + 1) % maxTrailingStamps;
            }
        }
    }


    // Update the currently rendered stamp, based on the patch type the player is standing on.
    public void Set(string pName)
    {
        if (currentStyle == null || !pName.Contains(currentStyle))
        {
            if (pName.Contains("Grass"))
            {
                currentStyle = "Grass";
                trailingStamp = Resources.Load(trailingStampsPath + trailingGrass) as GameObject;
                possibleDeltaValues = new float[] { 0f, 0.05f, 0.1f, 0.15f, 0.2f, 0.25f };
                possibleScaleValues = new float[] { 0.85f, 0.9f, 0.95f, 1.0f, 1.05f, 1.1f, 1.15f, 1.2f, 1.25f, 1.3f };
            }
            else if (pName.Contains("Brick"))
            {
                currentStyle = "Brick";
                trailingStamp = Resources.Load(trailingStampsPath + trailingBrick) as GameObject;
                possibleDeltaValues = new float[] { 0 };
                possibleScaleValues = new float[] { 0 };
            }
            else if (pName.Contains("Wood"))
            {
                currentStyle = "Wood";
                trailingStamp = Resources.Load(trailingStampsPath + trailingWood) as GameObject;
                possibleDeltaValues = new float[] { 0 };
                possibleScaleValues = new float[] { 0.85f, 0.9f, 0.95f, 1.0f, 1.05f, 1.1f, 1.15f, 1.2f, 1.25f, 1.3f };
            }
        }
    }


    public void Unset()
    {
        currentStyle = null;
    }


    // Brick's are rendered only when a trigger is activated.
    public void StampBrick(Vector3 pTargetPosition)
    {
        GameObject newSprite;
        string stampTag = "TrailingStamp";

        if (currentStyle == null || !currentStyle.Equals("Brick"))
        {
            Set("Brick");
        }

        // Create.
        newSprite = Instantiate(trailingStamp);

        // Rendering updates.
        newSprite.transform.position = pTargetPosition;

        // Admin.
        newSprite.tag = stampTag;
        newSprite.transform.parent = container.transform;
        allStamps = AddSprite(newSprite, allStamps, stampIndex);
        stampIndex = (stampIndex + 1) % maxTrailingStamps;
    }
}