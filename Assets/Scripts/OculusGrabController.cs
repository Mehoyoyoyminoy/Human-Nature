using UnityEngine;


public class OculusGrabController : MonoBehaviour
{
    private OVRInput.Controller controller;
    private GameObject grabbedObject;
    private bool isGrabbing = false;
    private AudioSource audioSource;
    void Start()
{
    controller = OVRInput.Controller.RTouch; // Adjust based on your controller setup
    audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
}


    void Update()
    {
        // Check for button A press to grab/drop object
        if (OVRInput.GetDown(OVRInput.Button.One, controller))
        {
            if (grabbedObject == null)
            {
                GrabObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void GrabObject()
{
    RaycastHit hit;
    if (Physics.Raycast(transform.position, transform.forward, out hit))
    {
        if (hit.collider.GetComponent<Rigidbody>())
        {
            grabbedObject = hit.collider.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.SetParent(transform);
            isGrabbing = true;

            Animator animator = grabbedObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            // Notify the ObjectManager that an object has been grabbed, passing the grabbed object
            ObjectManager.instance.ObjectGrabbed(grabbedObject);
        }
    }
}


    void DropObject()
{
    if (grabbedObject != null)
    {
        Rigidbody objectRb = grabbedObject.GetComponent<Rigidbody>();
        objectRb.isKinematic = false;
        objectRb.useGravity = false; // Optionally, re-enable gravity if needed
        
        objectRb.AddForce(Vector3.up * 1.5f, ForceMode.VelocityChange);

        // Re-enable the Animator component if it exists
        Animator animator = grabbedObject.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = true;
        }

        grabbedObject.transform.SetParent(null);
        grabbedObject = null;
        isGrabbing = false;

        // Play the drop sound
        audioSource.Play();
    }
}

}
