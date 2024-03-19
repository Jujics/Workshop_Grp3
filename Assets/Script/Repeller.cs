using UnityEngine;

public class Repeller : MonoBehaviour
{
    public float repelForce = 100000f; // Adjust this value to control the strength of the repulsion
    public float repelRadius = 5f; // Adjust this value to control the radius of the repulsion area

    void Update()
{
    // Find all colliders within the repel radius
    Collider[] colliders = Physics.OverlapSphere(transform.position, repelRadius);

    // Apply repulsion force to each nearby object with a Rigidbody
    foreach (Collider col in colliders)
    {
        // Check if the collider has a transform and a Rigidbody
        if (col.transform != null && col.attachedRigidbody != null)
        {
            // Calculate direction from repeller to the object
            Vector3 direction = col.transform.position - transform.position;

            // Apply repulsion force in the opposite direction
            col.attachedRigidbody.AddForce(direction.normalized * repelForce * Time.deltaTime, ForceMode.Impulse);
        }
    }
}

    // Visualize the repulsion area in the Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, repelRadius);
    }
}