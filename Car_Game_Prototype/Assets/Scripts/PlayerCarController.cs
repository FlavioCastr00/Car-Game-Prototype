using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [SerializeField] private float accelerationForce = 10000f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float accelerationInput = Input.GetAxis("Vertical");

        // Acceleration follows Newton's second law: F = m * a
        rb.AddForce(Vector3.forward * accelerationInput * accelerationForce, ForceMode.Force);
    }
}
