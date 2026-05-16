using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerCarController carController;
    [SerializeField] private Image speedMeterImage;
    [SerializeField] private TextMeshProUGUI currentGuearText;
    [SerializeField] private TextMeshProUGUI currentSpeedText;

    private float[] maxSpeedGuears;
    private int maxSpeedGuearsIndex; // This variable is initialized on the star method of PlayerCarController

    public void SetMaxSpeedGuearsIndex(int index)
    {
        maxSpeedGuearsIndex = index;
        currentGuearText.text = (maxSpeedGuearsIndex + 1).ToString(); // Updates UI Element
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxSpeedGuears = carController.GetSpeedToChangeGuears();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeed = carController.GetSpeed();
        float fillRatio = currentSpeed / maxSpeedGuears[maxSpeedGuearsIndex];

        currentSpeedText.text = Convert.ToInt32(currentSpeed * 3.6f).ToString();

        speedMeterImage.fillAmount = Mathf.Clamp01(fillRatio);
    }
}
