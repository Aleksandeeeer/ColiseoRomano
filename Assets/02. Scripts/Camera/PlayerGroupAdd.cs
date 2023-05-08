using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class PlayerGroupAdd : MonoBehaviour
{
    [SerializeField] [Tooltip("This is the group of characters the camera will follow")]
    private CinemachineTargetGroup _targetGroup;

    private void OnEnable()
    {
        PlayerJoinNotifier.OnPlayerJoins += OnPlayerJoined;
    }

    private void OnDisable()
    {
        PlayerJoinNotifier.OnPlayerJoins -= OnPlayerJoined;
    }

    // When a player joins, add it to the camera group
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        _targetGroup.AddMember(playerInput.transform, 1f, 1f);
    }
}
