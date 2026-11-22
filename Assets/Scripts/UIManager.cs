using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI stopwatchText;
    public TextMeshProUGUI knockoutCounterText;

    void Update()
    {
        if (GameManager.Instance == null) return;

        if (stopwatchText != null)
        {
            float t = GameManager.Instance.elapsedTime;
            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            stopwatchText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (knockoutCounterText != null)
        {
            knockoutCounterText.text = GameManager.Instance.knockouts.ToString();
        }
    }
}
