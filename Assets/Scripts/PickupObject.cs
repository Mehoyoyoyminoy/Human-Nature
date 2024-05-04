using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour
{
    private Transform target;
    private bool canTrigger = true;
    private Rigidbody rb;
    private Collider coll; // object must have collider attached to it 

    // Offset to adjust the position of the object in the hand
    public Vector3 positionOffset;

    // Offset to adjust the rotation of the object in the hand
    public Vector3 rotationOffset;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
        coll = GetComponent<Collider>(); // Get the Collider component
        if(rb == null)
        {
            Debug.Log("object must have rigid body");
        } else if(coll == null)
        {
            Debug.Log("Object must have collider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // when the hand enters the area we turn off the other objects collider and rigid body and make the object follow our hand
        if (canTrigger && other.CompareTag("Hand"))
        {
            rb.isKinematic = true;
            coll.enabled = false;
            canTrigger = false;
            target = other.transform;
        }
    }

    private void Update()
    {
        if (!canTrigger && target != null)
        {
            // Apply offset position to the target position
            Vector3 desiredPosition = target.position + target.TransformDirection(positionOffset);

            // Follow the target transform position with the offset
            transform.position = desiredPosition;

            // Apply offset rotation to the target rotation
            Quaternion desiredRotation = target.rotation * Quaternion.Euler(rotationOffset);

            // Follow the target transform rotation with the offset
            transform.rotation = desiredRotation;

            if (OVRInput.Get(OVRInput.Button.One))
            {
                //Do Something When the Player Has Something in their hand

                // Hide the Item
                //gameObject.SetActive(false);


                // Drop the Item
                DropObject();
            }
        }
    }
    void DropObject()
    {
        if (!canTrigger && target != null)
        {
            // Enable the collider and kinematic properties
            rb.isKinematic = false;
            coll.enabled = true;
            canTrigger = true;
            target = null;

            // Apply a downward force to simulate dropping the object
            rb.AddForce(Vector3.down * 2.0f, ForceMode.VelocityChange);
        }
    }

}