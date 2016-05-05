using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    // Inspector variables.
    public float moveSpeed = 3.5f;            // How quickly the character moves.
    public LayerMask whatIsGround;            // A mask determining what is ground to the player.
    public float jumpForce = 600f;            // The force applied for a jump action.

    // Private variables.
    private Transform groundCheck;            // A position marking where to check if the player is grounded.
    private float k_GroundedRadius = 0.1f;    // The radius of the overlap circle to determine if the player is grounded.
    private Vector3 startPosition;            // The player's starting position.
    private bool isGrounded;                  // Whether or not the player is grounded.
    private bool isFacingRight;               // Whether or not the player is facing right.
    private bool isMoving = false;            // Whether or not the player is moving along the x-axis, for help rendering trailing stamps.
    private bool canStamp;                    // Whether or not the player can render stamps from moving and jumping.
    private bool isInActiveGoal = false;      // Whether or not the player is within the goal and the goal is active.
    
    // References.
    private GameController gameController;    
    private TrailingStamps trailingRenderer;
    private JumpStamper jumpStampRenderer;
    private Animator animator;
    private Rigidbody2D rigidBody;

    // Use this for initialization
    void Start ()
    {
        // Obtain the references needed.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        groundCheck = transform.Find("GroundCheck");
        rigidBody = GetComponent<Rigidbody2D>();
        trailingRenderer = GetComponent<TrailingStamps>();
        jumpStampRenderer = GetComponent<JumpStamper>();
        animator = GetComponent<Animator>();
        
        // Initialize global variables.
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
                // Make sure the trailing stamp is correct based on the patch tile currently being stood on.
                if (colliders[i].CompareTag("GroundPatch"))
                {
                    trailingRenderer.Set(colliders[i].name);
                    jumpStampRenderer.Set(colliders[i].name);
                }
                // Update the value of isGrounded to reflect the appropriate state.
                isGrounded = true;
                return;
            }
        }
        trailingRenderer.Unset();
        isGrounded = false;
    }


    public void ProcessUserInput(float pHorizontalMove, bool pShouldJump, bool pIsActionPressed)
    {
        if (GameController.GameState.Playing == gameController.state)
        {
            // Set the player's horizontal velocity based on player input.
            rigidBody.velocity = new Vector2(pHorizontalMove * moveSpeed, rigidBody.velocity.y);
            // Manage horizontal movement for sprite animation.
            animator.SetFloat("hSpeed", Mathf.Abs(rigidBody.velocity.x));
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
                // Check for a fill action.
                if (gameController.patternController.blankStamp != null)
                {
                    // Fill the stamp.
                    gameController.patternController.FillBlankStamp();
                }

                // Check for entering the goal, ending the level.
                if (isInActiveGoal)
                {
                    Reset();
                    gameController.LoadNextLevel();
                }
            }
        }
    }

    public void Reset()
    {
        isInActiveGoal = false;
        canStamp = true;
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
        if (other.CompareTag("BlankStamp"))
        {
            gameController.patternController.blankStamp = other.gameObject;
        }
        else if (other.CompareTag("Pattern"))
        {
            gameController.patternController.AddPatternToCollection(other.gameObject);
        }
        else if (other.CompareTag("DisallowStamps"))
        {
            canStamp = false;
        }
        else if (other.CompareTag("ShouldBeParent"))
        {
            transform.parent = other.transform.parent;
        }
        else if (other.name.Equals("ResetBoundary"))
        {
            // Reset the velocity to 0, so the character doesn't go through the ground after being respawned.
            rigidBody.velocity = new Vector2(0, 0f);
            // Respawn the character.
            transform.position = startPosition;
        }
        else if (other.CompareTag("ActivateOnEnter"))
        {
            other.GetComponent<ActivateOnEnter>().Activate();
        }
        else if (other.CompareTag("Brick"))
        {
            Vector3 targetPosition = new Vector3(other.transform.position.x, groundCheck.position.y, groundCheck.position.z);
            trailingRenderer.StampBrick(targetPosition);
            other.enabled = false;
        }

        // Check for having entered the goal.
        if (other.name.Equals("Goal"))
        {
            if (other.GetComponent<Goal>().IsActive)
            {
                isInActiveGoal = true;
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlankStamp"))
        {
            gameController.patternController.blankStamp = null;
        }
        else if (other.CompareTag("DisallowStamps"))
        {
            canStamp = true;
        }
        else if (other.CompareTag("ShouldBeParent"))
        {
            transform.parent = null;
        }

        // Check for leaving an active goal.
        if (other.name.Equals("Goal") && isInActiveGoal)
        {
            isInActiveGoal = false;
        }
    }
}
