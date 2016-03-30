using UnityEngine;
using System.Collections;

public class ActivateOnStampFill : MonoBehaviour {

    // Interface variabes.
    public GameObject stampToWatchForFill;

    // Private variables.
    private Color stampStartColor;


	// Use this for initialization
	void Start ()
    {
        stampStartColor = stampToWatchForFill.GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (stampToWatchForFill.GetComponent<SpriteRenderer>().color != stampStartColor)
        {
            ActivateChildren();
        }
	}


    // Sets every child of this game object to active.
    private void ActivateChildren()
    {
        Transform[] children = this.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < children.Length; i++)
        {
            children[i].gameObject.SetActive(true);
        }
    }
}
