using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TextMeshProUGUI scoreText;

    // Score Target Variables
    private float highSpeedScoreTarget = 28f;

    // Score Value
    private float highSpeedScore = 10f;

    // Score Timer Variables
    private float highSpeedScoreTimer = 0f;
    private float scoreCooldown = 2f;

    // Variables to Calculate new Score
    private int scoreMultiplier = 1;
    private float currentScore = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        // Score for Driving in High Speed
        if (playerRB.linearVelocity.magnitude > highSpeedScoreTarget && highSpeedScoreTimer < scoreCooldown)
        {
            highSpeedScoreTimer += Time.deltaTime;
        }
        else if (highSpeedScoreTimer > scoreCooldown)
        {
            currentScore += highSpeedScore * scoreMultiplier;
            highSpeedScoreTimer = 0f;
            scoreText.text = currentScore.ToString();
        }
        else if (playerRB.linearVelocity.magnitude < highSpeedScoreTarget && highSpeedScoreTimer != 0f)
        {
            highSpeedScoreTimer = 0f;
        }
    }
}
