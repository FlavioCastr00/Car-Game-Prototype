using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private PlayerCarController playerCarController;

    // Score Target Variables
    private float highSpeedScoreTarget = 28f;

    // Score Value
    private float highSpeedScore = 10f;
    private float airborneScore = 20f;

    // Score Timer Variables
    private float highSpeedScoreTimer = 0f;
    private float highSpeedScoreCooldown = 3f;
    private float airborneScoreTimer = 0f;
    private float airborneScoreTimerCooldown = 2f;

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
        if (playerRB.linearVelocity.magnitude > highSpeedScoreTarget && highSpeedScoreTimer < highSpeedScoreCooldown)
        {
            highSpeedScoreTimer += Time.deltaTime;
        }
        else if (highSpeedScoreTimer > highSpeedScoreCooldown)
        {
            currentScore += highSpeedScore * scoreMultiplier;
            highSpeedScoreTimer = 0f;
        }
        else if (playerRB.linearVelocity.magnitude < highSpeedScoreTarget && highSpeedScoreTimer != 0f)
        {
            highSpeedScoreTimer = 0f;
        }

        // Score for being Airborne
        if (!playerCarController.getIsGrounded() && airborneScoreTimer < airborneScoreTimerCooldown)
        {
            airborneScoreTimer += Time.deltaTime;
        }
        else if (airborneScoreTimer > airborneScoreTimerCooldown)
        {
            currentScore += airborneScore * scoreMultiplier;
            airborneScoreTimer = 0f;
        }
        else if (playerCarController.getIsGrounded() && airborneScoreTimer != 0f)
        {
            airborneScoreTimer = 0f;
        }

        // Update UI
        scoreText.text = currentScore.ToString();
    }
}
