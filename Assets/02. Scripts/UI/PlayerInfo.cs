using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The text object that shows the score of the player")]
    private TextMeshProUGUI _scoreText;

    // To activate the UI, activate the UI game object and subscribe to the coin obtain
    public void Activate(GameObject playerObject)
    {
        gameObject.SetActive(true);
        CoinObtainer coinObtainer = playerObject.GetComponent<CoinObtainer>();
        coinObtainer.OnCoinObtained += OnCoinObtained;
    }

    // When you obtain a coin, show how many coins you have now
    private void OnCoinObtained(int coins)
    {
        _scoreText.text = coins.ToString();
    }
}
