using UnityEngine;

public class EngineSoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource engineAudio;

    [SerializeField] private float idlePitch = 0.8f;
    [SerializeField] private float maxPitch = 2.0f;

    [SerializeField] private float minVolume = 0.3f;
    [SerializeField] private float maxVolume = 1.0f;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rb.linearVelocity.magnitude;

        float speedPercent = Mathf.Clamp01(speed / 50);

        engineAudio.pitch = Mathf.Lerp(idlePitch, maxPitch, speedPercent);

        engineAudio.volume = Mathf.Lerp(minVolume, maxVolume, speedPercent);
    }
}
