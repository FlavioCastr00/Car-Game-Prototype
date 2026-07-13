using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private PlayerCarController playerCarController;
    [SerializeField] private CenterBar centerBar;

    // Score Target Variables
    private float highSpeedScoreTarget = 28f;

    // Score Value
    private float highSpeedScore = 15f;
    private float airborneScore = 25f;
    private float driftScore = 35;

    // Score Timer Variables
    private float highSpeedScoreTimer = 0f;
    private float highSpeedScoreCooldown = 3f;
    private float airborneScoreTimer = 0f;
    private float airborneScoreTimerCooldown = 1f;
    private float driftScoreTimer = 0f;
    private float driftScoreTimerCooldown = 1f;

    // Variables to Calculate new Score
    private float scoreMultiplier = 1f;
    private float currentScore = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "0";
        multiplierText.text = "x1";
        StartCoroutine(DecreaseScoreMultiplierEverySecond());
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

        centerBar.SetValue(scoreMultiplier);
    }

    // Decrease The Score Multiplier Every Second
    private IEnumerator DecreaseScoreMultiplierEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            
            if (scoreMultiplier > 1f)
            {
                scoreMultiplier -= 0.02f;
                
                // Update Current Multiplier in the UI
                multiplierText.text = "x" + Mathf.FloorToInt(scoreMultiplier).ToString();
            }
        }
    }

    // Update Score Method
    private void UpdateScore(float score)
    {
        currentScore += score * Mathf.FloorToInt(scoreMultiplier);

        scoreMultiplier = scoreMultiplier + (score * 0.01f);

        // Update Current Score Text in the UI
        scoreText.text = currentScore.ToString();
    }
}
