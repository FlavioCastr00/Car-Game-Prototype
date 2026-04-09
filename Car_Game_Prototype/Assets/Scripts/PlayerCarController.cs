using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    // Variables for Movement
    [SerializeField] private float accelerationForce = 30000f;
    [SerializeField] private float turnVelocity = 400f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float sidewaysGrip = 10;
    [SerializeField] private float linearDumping = 0.12f;
    [SerializeField] private float angularDumping = 10f;
    
    // Variables for Ground Checking
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    // Other  variables
    private Rigidbody rb;
    private float reverseAccelerationForce = 15000f;
    private float gravityOffTimer;
    private float gravityOffDuration = 1f;
    private bool isGrounded;
    private bool canFly = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // Lower center of mass to make car harder to flip
        rb.linearDamping = linearDumping;
        rb.angularDamping = angularDumping;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if car is on the ground

        // Set gravity off for a short period when car gets off the ground
        if (!isGrounded && canFly)
        {
            gravityOffTimer = gravityOffDuration;
            canFly = false;
            rb.useGravity = false;
        }

        // Logic to bring gravity back on
        if (gravityOffTimer > 0)
        {
            gravityOffTimer -= Time.deltaTime;

            if (gravityOffTimer < 0 || canFly)
            {
                rb.useGravity = true;
                gravityOffTimer = 0;
            }
        }
    }

    void FixedUpdate()
    {
        float speed = rb.linearVelocity.magnitude;
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        // Inputs
        float accelerationInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Acceleration follows Newton's second law: F = m * a
        float speedFactor = Mathf.Clamp01(1 - (speed / maxSpeed)); // Limits acceleration at high speed

        if (isGrounded) // Enable input only when the car is grounded
        {
            canFly = true;

            if (Input.GetKey(KeyCode.W)) // Forward Speed Force
            {
                rb.AddForce(transform.forward * accelerationInput * accelerationForce * speedFactor);
            }
            else if (Input.GetKey(KeyCode.S)) // Backward Speed Force
            {
                rb.AddForce(transform.forward * accelerationInput * reverseAccelerationForce * speedFactor);
            }

            // Turn car
            if (Mathf.Abs(forwardSpeed) > 0.03f)
            {
                float direction = Mathf.Sign(forwardSpeed); // 1 or -1
                float turnAmount = turnInput * turnVelocity * Mathf.Clamp01(speed / maxSpeed) * direction * Time.fixedDeltaTime;
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnAmount, 0f));
            }
        }

        // Dynamic Inertia
        rb.linearDamping = linearDumping + (speed * 0.02f);

        // Lateral Friction
        Vector3 lateralVelocity = Vector3.Dot(rb.linearVelocity, transform.right) * transform.right;
        rb.AddForce(-lateralVelocity * sidewaysGrip, ForceMode.Acceleration); // Reduce lateral velocity
    }
}