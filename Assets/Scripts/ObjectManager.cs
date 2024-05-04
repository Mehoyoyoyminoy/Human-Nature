using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ObjectGrabbed(GameObject grabbedObject)
    {
        string grabbedTag = grabbedObject.tag;

        // Find all objects with the same tag
        GameObject[] objectsInSet = GameObject.FindGameObjectsWithTag(grabbedTag);
        foreach (var obj in objectsInSet)
        {
            if (obj != grabbedObject)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false; // Ensure the Rigidbody is not kinematic
                    rb.AddForce(new Vector3(Random.Range(-1f, 1f), 1, Random.Range(-1f, 1f)) * 5, ForceMode.Impulse);
                }
            }
        }
    }
}
