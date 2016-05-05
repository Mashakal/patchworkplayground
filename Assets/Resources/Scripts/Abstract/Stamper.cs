using UnityEngine;
using System.Collections;

public abstract class Stamper : MonoBehaviour {
    protected GameObject stampContainer;

    protected bool FindStampContainer()
    {
        stampContainer = GameObject.Find("StampContainer");
        return stampContainer == null ? false : true;
    }

    protected GameObject[] AddSprite(GameObject pSpriteToAdd, GameObject[] pAllStamps, int pStampIndex)
    {
        // Check if there is already a Sprite at the index we are to add the new Sprite.
        if (pAllStamps[pStampIndex])
        {
            // if so, destroy it.
            Destroy(pAllStamps[pStampIndex]);
        }

        // Add the newly created Sprite to the array at the given index.
        pAllStamps[pStampIndex] = pSpriteToAdd;

        return pAllStamps;
    }


    // Searches within a radious of pTargetPosition for a rendered stamp that has the tag pSearchForTag.
    // pDistanceAmplifier is used to increase the radius of the search area, consequently increasing the distance between acceptable stamp positions.
    protected bool SearchVacinityForStamp(string pSearchForTag, Vector2 pTargetPosition, float pDistanceAmplifier = 1f)
    {
        Collider2D[] collidersFound;            // Holds all the colliders found in the vacinity.

        // Search with different radii to avoid the "Polka Dot Effect" (equal distances between stamps).
        float[] radii = { 0.4f, 0.5f, 0.6f, 0.8f, 1.0f, 1.2f };
        int randomIndex = (int)(UnityEngine.Random.value * radii.Length);
        float radiusToSearch = radii[randomIndex];

        // Do the searching.
        collidersFound = Physics2D.OverlapCircleAll(pTargetPosition, radiusToSearch * pDistanceAmplifier);
        if (collidersFound.Length != 0)
        {
            foreach (Collider2D coll in collidersFound)
            {
                if (coll.tag.Equals(pSearchForTag))
                {
                    return true;
                }
            }
        }

        // If no item was found, there is no neighbor.
        return false;
    }
}
