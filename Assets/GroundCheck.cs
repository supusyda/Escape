using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float rayLength = 1f; // Length of the ray
    public LayerMask groundLayer; // LayerMask to identify ground
    public Vector2 rayOriginOffset = Vector2.zero; // Offset for the ray origin (e.g., from player's feet)

    // Property to check if the player is grounded
    public bool IsGrounded { get; private set; } = true;

    private void Update()
    {
        // Ray origin is the current position plus the offset
        Vector2 rayOrigin = (Vector2)transform.position + rayOriginOffset;

        // Cast a ray downward to detect ground
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);

        // Update grounded status
        IsGrounded = hit.collider != null;

    }

    private void OnDrawGizmos()
    {
        // Visualize the ray in the editor
        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Vector2 rayOrigin = (Vector2)transform.position + rayOriginOffset;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector2.down * rayLength);
    }
}
