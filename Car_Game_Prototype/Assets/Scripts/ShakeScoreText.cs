using UnityEngine;

public class ShakeScoreText : MonoBehaviour
{
    private RectTransform textRectTransform;
    private Vector3 originalPosition;

    [Tooltip("This value will be multiplied by the scoreMultiplier variable to produce extra shakiness")]
    [SerializeField] private float shakeIntensity = 5f;

    [SerializeField] private float shakeSpeed = 40f;
    [SerializeField] private ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        textRectTransform = GetComponent<RectTransform>();
        originalPosition = textRectTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        int multiplier = scoreManager.GetRoundScoreMultiplier();

        // Calculate random positional offsets using Perlin Noise
        float offsetX = (Mathf.PerlinNoise(Time.time * shakeSpeed * multiplier, 0f) - 0.5f) * shakeIntensity;
        float offsetY = (Mathf.PerlinNoise(0f, Time.time * shakeSpeed * multiplier) - 0.5f) * shakeIntensity;

        textRectTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);
    }
}
