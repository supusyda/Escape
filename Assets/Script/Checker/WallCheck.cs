using UnityEngine;

public class WallCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float rayLength = 1f; // Length of the ray
    public LayerMask targetLayer; // LayerMask to identify ground
    public Vector2 rayOriginOffset = Vector2.zero; // Offset for the ray origin (e.g., from player's feet)

    // Property to check if the player is grounded
    public bool IsWalled { get; private set; } = true;

    private void Update()
    {
        // Ray origin is the current position plus the offset
        Vector2 rayOrigin = (Vector2)transform.position + rayOriginOffset;

        // Cast a ray downward to detect ground
        Vector2 rayDir = new Vector2(transform.parent.localScale.x, 0);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDir, rayLength, targetLayer);

        // Update grounded status
        IsWalled = hit.collider != null;

    }

    private void OnDrawGizmos()
    {
        // Visualize the ray in the editor
        Gizmos.color = IsWalled ? Color.green : Color.red;
        Vector2 rayOrigin = (Vector2)transform.position + rayOriginOffset;
        Vector2 rayDir = new Vector2(transform.parent.localScale.x, 0);


        Gizmos.DrawLine(rayOrigin, rayOrigin + rayDir * rayLength);
    }
}
