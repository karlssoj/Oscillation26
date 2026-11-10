using UnityEditor.Callbacks;
using UnityEngine;

// Simulates a simple vertical spring (Hooke's law: F = k * x).
// The object is pulled back toward the height it started at.
// The further it is moved away, the stronger it is pulled back,
// which makes it oscillate up and down.
// Requires a Rigidbody on the same GameObject.
public class Spring : MonoBehaviour
{
    // Spring constant k: how stiff the spring is.
    // Higher value = stronger pull = faster oscillation.
    public float K = 102;

    // The height (y) where the spring is at rest (no stretch, no force).
    float RestPositionY;

    // The Rigidbody we apply the spring force to.
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Use the starting height as the rest position of the spring.
        RestPositionY = transform.position.y;
        rb = GetComponent<Rigidbody>();
    }

    // FixedUpdate runs at a fixed time step (default 50 times per second).
    // Physics forces should be applied here, not in Update.
    void FixedUpdate()
    {
        // x in Hooke's law: how far the object is from its rest height.
        // Positive when the object is below the rest position,
        // negative when it is above it.
        float DeltaY = RestPositionY - transform.position.y;

        // F = k * x. The sign of DeltaY makes the force always point
        // back toward the rest position (a restoring force).
        float SpringForce = DeltaY * K;

        // Push the object only along the y axis (up or down).
        rb.AddForce(0, SpringForce, 0);
    }
}
