using UnityEngine;
using System.Collections;

public class SkyRenderer : MonoBehaviour {

    public GameObject sky1;
    public GameObject sky2;

    public int tilesPerRow = 75;
    public int numRows = 3;
    public float width = 6.12f;
    public float height = 5.12f;
    public float minOpacity = 0.7f;
    public float maxOpacity = 1f;

    // Private variables.
    private Vector3 startPosition;

    // References.
    private GameObject skyAnchor;

	// Use this for initialization
	void Start ()
    {
        // Obtain references.
        skyAnchor = GameObject.Find("SkyAnchor");
        // Get the start position.
        startPosition = skyAnchor.transform.position;
        // Actually draw the sky.
        DrawSky();
	}


    private void DrawSky()
    {
        GameObject newSkyTile;

        // For every row.
        for (int i = 0; i < numRows; i++)
        {
            // For every tile in the row
            for (int j = 0; j < tilesPerRow; j++)
            {
                // Create a tile for this position.
                newSkyTile = GetCheckeredSkyTile(i, j);

                // Determine a random opacity between min and max.
                SpriteRenderer spriteRend = newSkyTile.GetComponent<SpriteRenderer>();
                Color spriteColor = newSkyTile.GetComponent<SpriteRenderer>().color;
                spriteColor.a = GetRandomOpacity();
                spriteRend.color = spriteColor;

                // Set the position of the new tile.
                newSkyTile.transform.position = new Vector3(startPosition.x + (width * j), startPosition.y + (height * i), 1);

                // Set the parent.
                newSkyTile.transform.parent = skyAnchor.transform.parent;

                // Randomly flip the sprite.
                newSkyTile.transform.localScale = FlipSprite(Mathf.Round(Random.value), Mathf.Round(Random.value));
            }
        }
    }


    private float GetRandomOpacity()
    {
        return Random.Range(minOpacity, maxOpacity);
    }


    // Will return game objects for checked style.
    private GameObject GetCheckeredSkyTile(int i, int j)
    {
        GameObject newTile;

        if (i % 2 == 0)
        {
            if (j % 2 == 0)
            {
                newTile = Instantiate(sky1);
            }
            else
            {
                newTile = Instantiate(sky2);
            }
        }
        else
        {
            if (j % 2 == 0)
            {
                newTile = Instantiate(sky2);
            }
            else
            {
                newTile = Instantiate(sky1);
            }
        }

        return newTile;
    }


    private Vector3 FlipSprite(float x, float y)
    {
        Vector3 theScale = transform.localScale;

        if (x == 1)
        {
            theScale.x *= -1;
        }

        if (y == 1)
        {
            theScale.y *= -1;
        }

        return theScale;
    }
}
