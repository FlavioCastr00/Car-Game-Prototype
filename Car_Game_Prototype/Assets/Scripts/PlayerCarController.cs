using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 30000f;
    [SerializeField] private float turnVelocity = 750f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float sidewaysGrip = 5;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Lower center of mass to make car harder to flip
        rb.linearDamping = 1.0f;
        rb.angularDamping = 2.0f;
    }

    void FixedUpdate()
    {
        float speed = rb.linearVelocity.magnitude;

        // Inputs
        float accelerationInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Acceleration follows Newton's second law: F = m * a
        float speedFactor = Mathf.Clamp01(1 - (speed / maxSpeed)); // Limits acceleration at high speed
        rb.AddForce(transform.forward * accelerationInput * accelerationForce * speedFactor);

        // Turn car
        float turnAmount = turnInput * turnVelocity * Mathf.Clamp01(speed / maxSpeed) * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f));

        // Lateral Friction
        Vector3 lateralVelocity = Vector3.Dot(rb.linearVelocity, transform.right) * transform.right;
        rb.AddForce(-lateralVelocity * sidewaysGrip, ForceMode.Acceleration); // Reduce lateral velocity
    }
}