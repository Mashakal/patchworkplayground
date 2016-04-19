using UnityEngine;
using System.Collections;

public abstract class Stamper : MonoBehaviour { 

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


    protected bool SearchVacinityForStamp(string pSearchForTag, Vector2 pTargetPosition)
    {
        Collider2D[] collidersFound;            // Holds all the colliders found in the vacinity.
        float distanceToSearchAmplifier = 1f;   // Control the distance between stamps, higher numbers make a false statement more difficult to achieve.

        // Search with different radii to avoid the "Polka Dot Effect" (equal distances between stamps).
        float[] radii = { 0.4f, 0.6f, 0.8f, 1.0f, 1.2f, 1.4f };
        int randomIndex = (int)(UnityEngine.Random.value * radii.Length);
        float radiusToSearch = radii[randomIndex];

        // Do the searching.
        collidersFound = Physics2D.OverlapCircleAll(pTargetPosition, radiusToSearch * distanceToSearchAmplifier);
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
