using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform cameraLookPosition;

    [SerializeField] private Vector3 cameraPos1 = new Vector3(0f, 1.52f, -3.5f);

    // LateUpdate called after all Update functions
    void LateUpdate()
    {
        // Camera offset position
        Vector3 cameraPosition = cameraLookPosition.position + cameraLookPosition.rotation * cameraPos1;
        transform.position = cameraPosition;
        // Camera focus point
        transform.LookAt(cameraLookPosition);
    }
}
