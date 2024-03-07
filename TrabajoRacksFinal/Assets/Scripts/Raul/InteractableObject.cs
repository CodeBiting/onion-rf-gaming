using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private bool isBeingCarried = false;
    private Transform playerTransform;
    private Vector3 offset;

    private void Update()
    {
        if (isBeingCarried)
        {
            // Update the position based on player movement
            transform.position = playerTransform.position + offset;

            // Update the rotation based on player rotation
            transform.rotation = playerTransform.rotation;
        }
    }

    public void ToggleInteraction()
    {
        if (isBeingCarried)
        {
            // Logic to drop the object
            isBeingCarried = false;
            transform.parent = null; // Unlink the object from the player
            // Make the object non-kinematic
            GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            // Logic to pick up the object
            isBeingCarried = true;
            transform.parent = playerTransform; // Link the object to the player
            // Make the object kinematic
            GetComponent<Rigidbody>().isKinematic = true;
            offset = transform.position - playerTransform.position;
        }
    }

    public void SetTransform(Transform t)
    {
        playerTransform = t;
    }
}
