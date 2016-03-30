using UnityEngine;
using System.Collections;

public class ActivateOnEnter : MonoBehaviour {

    public GameObject whatToActivate;


    public void Activate()
    {
        whatToActivate.SetActive(true);
    }


    public void Deactivate()
    {
        whatToActivate.SetActive(false);
    }


    public void Deactivate(float delay)
    {
        StartCoroutine(DeactivateWithDelay(delay));
    }


    IEnumerator DeactivateWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Deactivate();
    }
}
