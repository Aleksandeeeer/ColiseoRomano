using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The list of objects that contain the UI for each player")]
    private PlayerInfo[] _playerInfoPanels;

    private void OnEnable()
    {
        PlayerJoinNotifier.OnPlayerJoins += OnPlayerJoined;
    }

    private void OnDisable()
    {
        PlayerJoinNotifier.OnPlayerJoins -= OnPlayerJoined;
    }

    // When a player joins, we activate the UI
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Joined player with index {playerInput.playerIndex}");
        _playerInfoPanels[playerInput.playerIndex].Activate(playerInput.gameObject);
    }
}
