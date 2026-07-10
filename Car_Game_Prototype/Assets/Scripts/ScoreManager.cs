using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
    private float driftScore = 30;

    // Score Timer Variables
    private float highSpeedScoreTimer = 0f;
    private float highSpeedScoreCooldown = 3f;
    private float airborneScoreTimer = 0f;
    private float airborneScoreTimerCooldown = 2f;
    private float driftScoreTimer = 0f;
    private float driftScoreTimerCooldown = 1f;

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
            UpdateScore(highSpeedScore);
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
            UpdateScore(airborneScore);
            airborneScoreTimer = 0f;
        }
        else if (playerCarController.getIsGrounded() && airborneScoreTimer != 0f)
        {
            airborneScoreTimer = 0f;
        }

        // Score for Drifting
        if(playerCarController.getIsDrifting() && driftScoreTimer < driftScoreTimerCooldown)
        {
            driftScoreTimer += Time.deltaTime;
        }
        else if (driftScoreTimer > driftScoreTimerCooldown)
        {
            UpdateScore(driftScore);
            driftScoreTimer = 0f;
        }
        else if (!playerCarController.getIsDrifting() && driftScoreTimer != 0f)
        {
            driftScoreTimer = 0;
        }     
    }

    // Update Score Method
    private void UpdateScore(float score)
    {
        currentScore += score * scoreMultiplier;

        // Update UI
        scoreText.text = currentScore.ToString();
    }
}
