using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerJoinNotifier : MonoBehaviour
{

    public static Action<PlayerInput> OnPlayerJoins;

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        OnPlayerJoins?.Invoke(playerInput);
    }
}
