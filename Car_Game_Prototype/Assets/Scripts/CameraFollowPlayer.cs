using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform cameraLookPosition;

    [SerializeField] private Vector3 cameraPos1 = new Vector3(0f, 1.52f, -3.5f);
    [SerializeField] private float smoothTime = 0.1f;

    private Vector3 velocity = Vector3.zero;

    // LateUpdate called after all Update functions
    void LateUpdate()
    {
        // Camera focus point
        transform.LookAt(cameraLookPosition);
    }

    void FixedUpdate()
    {
        // Camera offset position
        Vector3 targetPosition = cameraLookPosition.position + cameraLookPosition.rotation * cameraPos1;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
