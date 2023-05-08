using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CountSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera that will follow the winner")]
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    [Tooltip("The game object that will activate when there is a winner")]
    private GameObject _winnerUIObject;
    [SerializeField]
    [Tooltip("The game object that will activate when there is a tie")]
    private GameObject _tieUIObject;
    [SerializeField]
    [Tooltip("The game object that will always activate to allow the player to exit or replay")]
    private GameObject _replayUI;


    private List<GameObject> _playerList = new List<GameObject>();

    private void Awake()
    {
        _winnerUIObject.SetActive(false);
        _tieUIObject.SetActive(false);
        _replayUI.SetActive(false);
        _virtualCamera.Priority = 0;
    }

    private void OnEnable()
    {
        PlayerJoinNotifier.OnPlayerJoins += OnPlayerJoined;
        GameTimer.OnTimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        PlayerJoinNotifier.OnPlayerJoins -= OnPlayerJoined;
        GameTimer.OnTimerEnded -= OnTimerEnded;
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        _playerList.Add(playerInput.gameObject);
    }

    private void OnTimerEnded()
    {
        int max = -1;
        bool tie = true;
        GameObject current = null;
        for(int i = 0; i < _playerList.Count; ++i)
        {
            CoinObtainer co = _playerList[i].GetComponent<CoinObtainer>();
            if (co.Coins > max)
            {
                tie = false;
                max = co.Coins;
                current = _playerList[i];
            } else if (co.Coins == max)
            {
                tie = true;
                current = null;
            }
        }

        if (tie)
        {
            _tieUIObject.SetActive(true);
        } else
        {
            _virtualCamera.Follow = current.transform;
            _virtualCamera.LookAt = current.transform;
            _virtualCamera.Priority = 50;
            _winnerUIObject.SetActive(true);
            current.GetComponentInChildren<Animator>().SetTrigger("Victory");
        }
        _replayUI.SetActive(true);
    }
}
