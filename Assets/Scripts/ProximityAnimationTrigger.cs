using UnityEngine;

public class ProximityAnimationTrigger : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public string animationTriggerName = "Play"; // The name of the trigger parameter in your Animator

    void Start()
    {
        if (animator == null)
        {
            // Automatically try to get the Animator component on the same GameObject
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the Collider is tagged as "Player" (or use another method to identify the player)
        if (other.CompareTag("Player"))
        {
            // Trigger the animation
            animator.SetTrigger(animationTriggerName);
        }
    }
}
