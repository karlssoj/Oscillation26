using System;
using UnityEngine;

// Simulates a simple (mathematical) pendulum by numerically integrating
// the equation of motion  θ'' = -(g / r) · sin(θ)  and rotating the pivot.
// The script also measures the pendulum's period and compares it with the
// theoretical small-angle period.
public class Pendulum : MonoBehaviour
{
    // Gravitational acceleration (m/s²)
    float G = 9.82f;
    // Arm length (distance from pivot to bob), computed in Start()
    float r;

    // Theoretical small-angle period: T = 2π·√(r/g)
    float ExpectedT;
    // Time (Time.time) of the most recent turning point in the same direction
    float PrevTurnMoment = 0;

    // Angular velocity ω (rad/s)
    float AVelocity = 0;
    // Angular velocity from the previous FixedUpdate, used to detect turning points
    float PrevAVelocity = 0;

    // The pendulum bob. The arm length is the distance between this object and the bob.
    public GameObject Bob;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Arm length = distance from the pivot (this object) to the bob
        r = Vector3.Distance(transform.position, Bob.transform.position);
        Debug.Log("Armlength: " + r);

        // Theoretical period (approximately valid for small amplitudes)
        ExpectedT = (2*Mathf.PI)*Mathf.Sqrt(r/G);
        Debug.Log("Expected period (T): " + ExpectedT);
    }

    // FixedUpdate is called at a fixed time step (Time.fixedDeltaTime), which
    // makes it suitable for physics simulation. Inside FixedUpdate,
    // Time.deltaTime returns the same value as Time.fixedDeltaTime.
    void FixedUpdate()
    {
        // Current angle θ in degrees (rotation around the z-axis).
        // NOTE: eulerAngles returns 0–360°, but sin() still gives the correct
        // sign since sin(360° - x) = -sin(x).
        float Theta = transform.eulerAngles.z;

        // Angular acceleration α = -(g/r)·sin(θ), multiplied by Δt.
        // So this variable actually holds the change in angular velocity (Δω) for this step.
        float Acceleration = (-G/r) * Mathf.Sin(Theta * Mathf.Deg2Rad) * Time.deltaTime;

        // Update angular velocity: ω += α·Δt
        AVelocity += Acceleration;

        // Update angle: θ += ω·Δt (converted from radians to degrees for Rotate).
        // Since ω is updated before θ, this is semi-implicit Euler, which keeps
        // energy more stable than plain (explicit) Euler.
        transform.Rotate(0, 0, AVelocity * Mathf.Rad2Deg * Time.deltaTime);

        // Turning point: angular velocity changes sign from positive to negative.
        // This happens once per full oscillation, so the time between two such
        // events is the period T.
        if(PrevAVelocity > 0 && AVelocity < 0)
        {
            // Measured period = time since the previous turning point.
            // NOTE: the first measurement is counted from time 0 and is therefore not a full period.
            float MeasuredT = Time.time - PrevTurnMoment;
            Debug.Log("Measured Period (T): " + MeasuredT);

            // Store the time of this turning point for the next measurement
            PrevTurnMoment = Time.time;
        }


        // Store angular velocity for the next step to detect a sign change
        PrevAVelocity = AVelocity;
    }
}
