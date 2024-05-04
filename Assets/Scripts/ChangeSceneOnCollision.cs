using UnityEngine;
using UnityEngine.SceneManagement;
using Oculus;

public class ChangeSceneOnCollision : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player (or an object tagged as 'Player') enters the trigger area
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has collided with the object, changing scene...");
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
        }
    }
}
