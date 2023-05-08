using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text object that shows the remaining time to finish the game")]
    private TextMeshProUGUI _timerText;

    private void Update()
    {
        if (GameTimer.instance.GameRunning)
        {
            float timer = GameTimer.instance.Timer;
            _timerText.text = timer.ToString("N0");
        }
    }
}
