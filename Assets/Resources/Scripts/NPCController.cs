using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour {

    // Interface variables.
    public bool isFacingRight = true;

    // References.
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	private void FixedUpdate()
    {
        if (player.transform.position.x < this.transform.position.x)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            if(!isFacingRight)
            {
                Flip();
            }
        }
    }


    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        isFacingRight = !isFacingRight;
    }
}
