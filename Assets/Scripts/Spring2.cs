using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;

// Simulates a mass hanging between two springs:
//
//   Links[0] ----spring 1---- [this object] ----spring 2---- Links[2]
//
// Each spring pulls this object toward its anchor (Links[0] or Links[2])
// with a force that follows Hooke's law (F = k * x), where x is how much
// the spring is stretched compared to its starting length.
// A LineRenderer draws the springs as a line through all the Links.
// Requires a Rigidbody and a LineRenderer on the same GameObject.
public class Spring2 : MonoBehaviour
{
    // The points the line is drawn through, in order.
    // Links[0] = first anchor, Links[1] = this object (the mass),
    // Links[2] = second anchor. Assign them in the Inspector.
    public GameObject[] Links;

    // Draws the visible line (the "springs") between the Links.
    LineRenderer Line;

    // The Rigidbody we apply the spring forces to.
    Rigidbody rb;

    // Spring constant k: how stiff both springs are.
    public float K;

    // Length of each spring when it is at rest (no stretch = no force).
    // RestLength1: distance to Links[0], RestLength2: distance to Links[2].
    float RestLength1, RestLength2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Line = GetComponent<LineRenderer>();

        // The LineRenderer needs one point per link, otherwise
        // SetPosition below gives an "index out of bounds" error.
        Line.positionCount = Links.Length;

        rb = GetComponent<Rigidbody>();

        // The distances at the start are the rest lengths of the springs,
        // so the system starts in balance.
        RestLength1 = Vector3.Distance(transform.position, Links[0].gameObject.transform.position);
        RestLength2 = Vector3.Distance(transform.position, Links[2].gameObject.transform.position);
    }

    // FixedUpdate runs at a fixed time step (default 50 times per second).
    // Physics forces should be applied here, not in Update.
    void FixedUpdate()
    {
        // Move the points of the line to the current positions of the Links,
        // so the drawn springs follow the objects.
        for(int i = 0; i < Links.Length; i++)
        {
            Line.SetPosition(i, Links[i].transform.position);
        }

        // x in Hooke's law: how much each spring is stretched (positive)
        // or compressed (negative) compared to its rest length.
        float DeltaY1 = Vector3.Distance(transform.position, Links[0].gameObject.transform.position) - RestLength1;
        float DeltaY2 = Vector3.Distance(transform.position, Links[2].gameObject.transform.position) - RestLength2;

        // F = k * x: the size of each spring's force.
        float SpringForce1 = DeltaY1 * K;
        float SpringForce2 = DeltaY2 * K;

        // Direction from this object toward each anchor.
        Vector3 Direction1 = Links[0].gameObject.transform.position - transform.position;
        Vector3 Direction2 = Links[2].gameObject.transform.position - transform.position;

        // Apply each spring force along its direction.
        // normalized makes the direction length 1, so only SpringForce sets the strength.
        // Stretched spring (positive force): pulls toward the anchor.
        // Compressed spring (negative force): pushes away from the anchor.
        rb.AddForce(Direction1.normalized * SpringForce1);
        rb.AddForce(Direction2.normalized * SpringForce2);
    }
}
