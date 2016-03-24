using UnityEngine;
using System.Collections;

public class RoutineMoving : MonoBehaviour {

    public bool moveHorizontally;
    public bool moveVertically;

    [Range(0, 15)]
    [SerializeField]
    float hDistance = 0.10f;

    [Range(0,15)]
    [SerializeField]
    float vDistance = 0.10f;



    //private Vector3 startPosition;
    
	// Use this for initialization
	void Start () {
        //startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
    }

    
    private void Rotate()
    {
        Vector3 xMoveAmount = Vector3.zero;
        Vector3 yMoveAmount = Vector3.zero;

        if (moveHorizontally)
        {
            xMoveAmount += transform.right * Mathf.Sin(Time.time) * hDistance * Time.deltaTime;
        }

        if (moveVertically)
        {
            yMoveAmount += transform.up * Mathf.Sin(Time.time) * vDistance * Time.deltaTime;
        }

        if (moveHorizontally || moveVertically)
        {
            transform.position += new Vector3(xMoveAmount.x, yMoveAmount.y, transform.position.z);
        }
    }
}
