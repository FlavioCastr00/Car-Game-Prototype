using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    // Variables for Movement
    [SerializeField] private float accelerationForce = 10000f;
    [SerializeField] private float reverseAccelerationForce = 8000f;
    [SerializeField] private float turnVelocity = 100f;
    [SerializeField] private float maxSpeed = 50f;
    [SerializeField] private float sidewaysGrip = 5f;
    [SerializeField] private float linearDumping = 0.005f;
    [SerializeField] private float angularDumping = 1f;
    [SerializeField] private float throttleSmoothTime = 0.3f;
    private float throttle;
    private float throttleVelocity;
    private float speed;
    private float forwardSpeed;
    private float accelerationInput;
    private float turnInput;
    
    // Variables for Ground Checking
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Transform groundCheck;

    // Other  variables
    private Rigidbody rb;
    private float gravityOffTimer;
    private float gravityOffDuration = 0.5f;
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
        // Inputs
        accelerationInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
        
        speed = rb.linearVelocity.magnitude;
        forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

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

        Debug.Log($"{speed} | {forwardSpeed}");
    }

    void FixedUpdate()
    {
        // Acceleration follows Newton's second law: F = m * a
        float speedFactor = Mathf.Clamp01(1 - Mathf.Pow(speed / maxSpeed, 3)); // Limits acceleration at high speed

        if (isGrounded) // Enable input only when the car is grounded
        {
            canFly = true;
            throttle = Mathf.SmoothDamp(throttle, accelerationInput, ref throttleVelocity, throttleSmoothTime);

            if (accelerationInput > 0) // Forward Speed Force
            {
                rb.AddForce(transform.forward * throttle * accelerationForce * speedFactor);
            }
            else if (accelerationInput < 0) // Backward Speed Force
            {
                rb.AddForce(transform.forward * throttle * reverseAccelerationForce * speedFactor);
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
        rb.linearDamping = linearDumping + (speed * 0.0002f);

        // Lateral Friction
        Vector3 lateralVelocity = Vector3.Dot(rb.linearVelocity, transform.right) * transform.right;
        rb.AddForce(-lateralVelocity * sidewaysGrip, ForceMode.Acceleration); // Reduce lateral velocity
    }
}