using UnityEngine;
using System.Collections;

public class DisappearAfterTime : MonoBehaviour
{
    public float delayInSeconds = 5.0f; // Time in seconds after which the object will disappear

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayInSeconds);

        // Make the GameObject invisible
        gameObject.SetActive(false);
    }
}
