using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10000f;
    [SerializeField] private float turnVelocity = 250f;

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
        // Acceleration follows Newton's second law: F = m * a
        float accelerationInput = Input.GetAxis("Vertical");
        rb.AddForce(transform.forward * accelerationInput * accelerationForce, ForceMode.Force);

        // Turn car
        float turnInput = Input.GetAxis("Horizontal");
        gameObject.transform.Rotate(Vector3.up * turnInput * turnVelocity * Time.deltaTime);
    }
}
