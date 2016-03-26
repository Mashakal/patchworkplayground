using UnityEngine;
using System.Collections;

public class MovingStairs : MonoBehaviour {

    [SerializeField]
    public static float stairMovementSpeed = 2f;
    private ArrayList childrenStairs = new ArrayList();

    private GameController gameController;

    private class MovingStair 
    {
        private enum Direction
        {
            Left = 0,
            Up = 1,
            Right = 2,
            Down = 3,
            None
        };
        
        private float maxDistance;
        private float distanceTravelled;
        private Direction moveDirection;
        private Transform trans;
        //private Vector3 startPosition;
        private int speedMultiplier;

        public MovingStair(Transform pTrans, int offset)
        {
            // The tile should move in exactly this number of units in every direction.
            maxDistance = pTrans.localScale.x * offset * 4;   // 2 units per tile * 2 tiles per move
            //Debug.Log("Max distance and offset are: " + maxDistance + "\t" + offset);
            moveDirection = maxDistance > 0 ? Direction.Left : Direction.None;
            trans = pTrans;
            //startPosition = pTrans.position;
            distanceTravelled = 0;
            speedMultiplier = offset;
            //Debug.Log("Speedmultiplier is: " + speedMultiplier);
        }

        public void Move()
        {
            float moveAmount = Time.deltaTime * MovingStairs.stairMovementSpeed * speedMultiplier;
            Vector3 translateVector = GetTranslateVector(moveAmount);
            
            if (trans.tag.Equals("Debug"))
            {
                Debug.Log("Position: " + trans.position);
                Debug.Log("TranslateVector" + translateVector);
                Debug.Log("DistanceTravelled " + distanceTravelled);
                Debug.Log("Maxdistance: " + maxDistance);
            }

            trans.position += translateVector;
        }

        private Vector3 GetTranslateVector(float pMoveAmount)
        {
            Vector3 translateVector = Vector3.zero;
            
            // Make sure we don't travel past the max distance.
            // We want to maintain the uniformity of the square being traversed.
            if (pMoveAmount + distanceTravelled >= maxDistance)
            {
                pMoveAmount = maxDistance - distanceTravelled;
                distanceTravelled = 0;
            }
            else
            {
                distanceTravelled += pMoveAmount;
            }

            // Use the appropriate move vector based on current move direction.
            if (moveDirection == Direction.Down)
            {
                translateVector += new Vector3(0, -pMoveAmount, 0);
            }
            else if (moveDirection == Direction.Up)
            {
                translateVector += new Vector3(0, pMoveAmount, 0);
            }
            else if (moveDirection == Direction.Left)
            {
                translateVector += new Vector3(-pMoveAmount, 0, 0);
            }
            else if (moveDirection == Direction.Right)
            {
                translateVector += new Vector3(pMoveAmount, 0, 0);
            }
            else
            {
                translateVector = Vector3.zero;
            }

            // Change direction if distance travelled has been reset.
            if (distanceTravelled == 0 && maxDistance != 0)
            {
                ChangeDirection();
            }

            return translateVector;

        }

        private void ChangeDirection()
        {
            moveDirection = moveDirection == Direction.Up ? Direction.Right :
                moveDirection == Direction.Right ? Direction.Down :
                moveDirection == Direction.Down ? Direction.Left : Direction.Up;
        }

    }


	// Use this for initialization
	void Start () {
        // Obtain a reference to the game controller.
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        Transform[] childrenTransforms = GetComponentsInChildren<Transform>();
        int count = 0;
        int i;
        // Start at 1 to ignore (this) the parent's transform.
        for (i = 1; i < childrenTransforms.Length; i++)
        {
            if (childrenTransforms[i].tag.Equals("MovingPlatform"))
            {
                childrenStairs.Add(new MovingStair(childrenTransforms[i], count));
                count++;
            }
            // else... there should be an error catch here.
        }
    }
	
	void Update ()
    {
        foreach (MovingStair stair in childrenStairs)
        {
            stair.Move();
        }
	}


}

