using UnityEngine;

public class CenterBar : MonoBehaviour
{
    [SerializeField] private RectTransform bar;

    // value from 0 to 1
    public void SetValue(float value)
    {
        float fillAmount;

        if (value >= 3f)
            fillAmount = 1f;
        else
            fillAmount = value - Mathf.Floor(value);

        bar.localScale = new Vector3(fillAmount, 1f, 1f);
    }
}