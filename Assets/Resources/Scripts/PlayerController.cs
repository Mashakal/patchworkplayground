using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    // Inspector variables.
    public float moveSpeed = 2f;
    public LayerMask whatIsGround;            // A mask determining what is ground to the player.
    public float jumpForce = 600f;            // The force applied for a jump action.

    // Private variables.
    private TrailingStamps trailingRenderer;  // A reference to the TrailingStamps component.
    private JumpStamper jumpStampRenderer;    // A reference to the jumpStamper component.
    private Transform groundCheck;            // A position marking where to check if the player is grounded.
    private float k_GroundedRadius = 0.1f;    // The radius of the overlap circle to determine if the player is grounded.
    private Animator Anim;                    // A reference to the player sprite's Animator.
    private Rigidbody2D rigidBody;            // A reference to the player's Rigidbody2D.
    private Collider2D[] downColliders;       // Holds any currently deactivated stamp colliders.
    private Vector3 startPosition;            // The player's starting position.
    private GameObject stampToFill;
    private bool isGrounded;                  // Whether or not the player is grounded.
    private bool isFacingRight;               // Whether or not the player is facing right.
    private bool isInFillStamp;               // Whether or not the player is on a blank stamp that can be filled.
    private bool isMoving = false;            // Whether or not the player is moving along the x-axis, for help rendering trailing stamps.
    private bool canStamp;                    // Whether or not the player can render stamps from moving and jumping.
    private string fillPattern;               // The current pattern to be used for filling in stamps.
    private List<GameObject> allPatterns;     // Holds every pattern the player has obtained.


	// Use this for initialization
	void Start ()
    {
        groundCheck = transform.Find("GroundCheck");
        rigidBody = GetComponent<Rigidbody2D>();
        trailingRenderer = GetComponent<TrailingStamps>();
        jumpStampRenderer = GetComponent<JumpStamper>();
        Anim = GetComponent<Animator>();
        allPatterns = new List<GameObject>();
        isFacingRight = true;
        canStamp = true;
        startPosition = transform.position;
	}
	

    private void Update()
    {
        // Determine if the player is grounded.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, k_GroundedRadius, whatIsGround);
        
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
    }


    public void ProcessUserInput(float pHorizontalMove, bool pShouldJump, bool pIsActionPressed)
    {
        // Set the player's horizontal velocity based on player input.
        rigidBody.velocity = new Vector2(pHorizontalMove * moveSpeed, rigidBody.velocity.y);
        // Manage horizontal movement for sprite animation.
        Anim.SetFloat("hSpeed", Mathf.Abs(rigidBody.velocity.x));
        // Determine if the player is moving along the x-axis.
        isMoving = rigidBody.velocity.x != 0 ? true : false;

        // If the player is pressing move left button and the character is facing right.
        if (rigidBody.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        // If the player is pressing move right button and the character is facing left.
        else if (rigidBody.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }

        // Check for jump conditions.
        if (pShouldJump && isGrounded)
        {
            // Add the jump force.
            rigidBody.AddForce(new Vector2(0, jumpForce));
            isGrounded = false;

            // Render a jump stamp if allowed.
            if (canStamp)
            {
                jumpStampRenderer.Stamp(groundCheck.position);
            }
        }

        // Check if conditions for rendering a trailing stamp are met.
        if (isGrounded && isMoving)
        {
            // Make sure the player is able to stamp.
            if (canStamp)
            {
                // Attempt to render a trailing stamp, will fail if a stamp is too close.
                trailingRenderer.Stamp(groundCheck.position);
            }
        }

        // Look for actions to take from an action key press.
        if (pIsActionPressed)
        {
            if (isInFillStamp && fillPattern != null)
            {
                // Fill the stamp.
                FillBlankStamp();
            }
        }
    }


    private void Flip()
    {
        // Switch the direction of the player.
        isFacingRight = !isFacingRight;

        // Flip the player.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("BlankStamp"))
        {
            isInFillStamp = true;
            stampToFill = other.gameObject;
        }
        else if (other.tag.Equals("Pattern"))
        {
            AddPatternToCollection(other.gameObject);
        }
        else if (other.tag.Equals("DisallowStamps"))
        {
            canStamp = false;
        }
        else if (other.tag.Equals("ShouldBeParent"))
        {
            transform.parent = other.transform.parent;
        }
        else if (other.name.Equals("ResetBoundary"))
        {
            // Reset the yVelocity to 0, so the character doesn't go through the ground after being respawned.
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            // Respawn the character.
            transform.position = startPosition;
        }

    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("BlankStamp"))
        {
            isInFillStamp = false;
            stampToFill = null;
        }
        else if (other.tag.Equals("DisallowStamps"))
        {
            canStamp = true;
        }
        else if (other.tag.Equals("ShouldBeParent"))
        {
            transform.parent = null;
        }
    }


    private void AddPatternToCollection(GameObject pPattern)
    {
        allPatterns.Add(pPattern);

        // TODO: Create a gui function for this event.  Ask the player if they want to use the material.

        // If there is no equipped pattern, equip this one.
        if (allPatterns.Count == 1)
        {
            Debug.Log("Equipping the pattern.");

            fillPattern = pPattern.name;
        }

        pPattern.SetActive(false);
    }


    private void FillBlankStamp()
    {
        GameObject spritePattern;
        Transform blankTransform = stampToFill.transform;

        // Create a new sprite based on the current fill pattern.
        string pathname = "prefabs/" + stampToFill.name + fillPattern;
        spritePattern = Resources.Load("prefabs/" + stampToFill.name + fillPattern) as GameObject;
        
        // Create the new filled sprite.
        GameObject newFilledSprite = Instantiate(spritePattern);
        // Match its scale to the old, blank sprite.
        newFilledSprite.transform.localScale = blankTransform.transform.localScale;
        // Match its position to the blank sprite.
        newFilledSprite.transform.position = blankTransform.transform.position;
        // Match its name to the blank sprite.
        newFilledSprite.name = blankTransform.name;
        // Destroy the blank sprite.
        Destroy(stampToFill);
    }
}
