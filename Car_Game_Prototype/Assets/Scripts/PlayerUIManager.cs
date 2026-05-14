using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private PlayerCarController carController;
    [SerializeField] private Image speedMeterImage;

    private float[] maxSpeedGuears;
    private int maxSpeedGuearsIndex = 0;

    public void SetMaxSpeedGuearsIndex(int index)
    {
        maxSpeedGuearsIndex = index;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxSpeedGuears = carController.GetSpeedToChangeGuears();
    }

    // Update is called once per frame
    void Update()
    {
        float currentSpeedKM = carController.GetSpeedKM();
        float fillRatio = currentSpeedKM / maxSpeedGuears[maxSpeedGuearsIndex];

        speedMeterImage.fillAmount = Mathf.Clamp01(fillRatio);
    }
}
