using UnityEngine;
using System.Collections;

public class JumpStamper : Stamper {

    // Inspector variables.
    public GameObject jumpStamp;            // The sprite that should be rendered as the jump stamp.
    public int maxJumpStamps = 50;          // The maximum number of jump stamps to be rendered.

    // Private variables.
    private int stampIndex = 0;
    private GameObject[] allStamps;
    private string currentStyle;

    // Paths.
    private string jumpStampsPath = "Prefabs/JumpStamps/";

    // The name of the prefab for each jump stamp.
    private string jumpGrass = "GrassVineTree";
    private string jumpBrick = "BrickStatue";
    private string jumpWood = "WoodNest";


    // Use this for initialization
    private void Start ()
    {
        allStamps = new GameObject[maxJumpStamps];
        FindStampContainer();
	}

    public void Reset()
    {
        FindStampContainer();
    }
	
    public void Stamp(Vector3 pTargetPosition)
    {
        string stampTag = "JumpStamp";          // The string tag to add to the Sprite after it is created.
        GameObject newSprite;                   // The GameObject that will carry the newly created sprite.
        float distanceAmplifier = 2f;           // How much to amplify the search radius, jump stamps are bigger than trailing stamps and need a larger search space.

        if (!SearchVacinityForStamp(stampTag, pTargetPosition, distanceAmplifier) && currentStyle != null)
        {
            // Instantiate the sprite object.
            newSprite = Instantiate(jumpStamp);
            // Change it's position.
            newSprite.transform.position = pTargetPosition;
            // Add the tag.
            newSprite.tag = stampTag;
            // Make it a child of the container GameObject.
            newSprite.transform.parent = stampContainer.transform;
            // Add the sprite to the tracking array.
            allStamps = AddSprite(newSprite, allStamps, stampIndex);
            // Increment the stampIndex
            stampIndex = (stampIndex + 1) % maxJumpStamps;
        }
    }


    // Sets the appropriate jump stamp
    public void Set(string pName)
    {
        if (currentStyle == null || !pName.Contains(currentStyle))
        {
            if (pName.Contains("Grass"))
            {
                currentStyle = "Grass";
                jumpStamp = Resources.Load(jumpStampsPath + jumpGrass) as GameObject;
            }
            else if (pName.Contains("Brick"))
            {
                currentStyle = "Brick";
                jumpStamp = Resources.Load(jumpStampsPath + jumpBrick) as GameObject;
            }
            else if (pName.Contains("Wood"))
            {
                currentStyle = "Wood";
                jumpStamp = Resources.Load(jumpStampsPath + jumpWood) as GameObject;
            }
        }
    }
}
