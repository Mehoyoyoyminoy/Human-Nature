using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus;

public class changeSceneButton : MonoBehaviour
{
    public Animator btnAnim; // Animator for button press animation
    public string sceneToLoad; // Name of the scene to load

    private bool isPlayerNearby = false; // Flag to check if the player is close enough to interact

    void Update()
    {
        // Check if the player is nearby and the "A" button on the right Oculus Touch controller is pressed
        if (isPlayerNearby && OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            Debug.Log("Player interaction and Oculus Button One press detected, changing scene...");
            if (btnAnim != null) btnAnim.Play("btnPress"); // Play button press animation if animator is assigned
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player (or an object tagged as 'Player') enters the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player (or an object tagged as 'Player') exits the trigger area
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
