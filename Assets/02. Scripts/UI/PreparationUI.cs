using UnityEngine;
using TMPro;

public class PreparationUI : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text object that shows the remaining time to start the game")]
    private TextMeshProUGUI _timerText;

    private void OnEnable()
    {
        GameTimer.OnPreparationEnded += OnPreparationEnded;
    }

    private void OnDisable()
    {
        GameTimer.OnPreparationEnded -= OnPreparationEnded;
    }

    private void OnPreparationEnded()
    {
        _timerText.text = "";
    }

    private void Update()
    {
        if (GameTimer.instance.GamePreparing)
        {
            float timer = GameTimer.instance.Timer;
            _timerText.text = timer.ToString("N0");
        }
    }
}
