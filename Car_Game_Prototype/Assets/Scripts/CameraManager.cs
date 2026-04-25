using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera cameraA;
    [SerializeField] private Camera cameraB;

    void Start()
    {
        cameraA.depth = 1;
        cameraB.depth = 0;
    }

    void Update()
    {
        // Handle Switch Cameras
        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool isAActive = cameraA.depth > cameraB.depth;

            if (isAActive)
            {
                cameraA.depth = 0;
                cameraB.depth = 1;
            }
            else
            {
                cameraA.depth = 1;
                cameraB.depth = 0;
            }
        }
    }
}
