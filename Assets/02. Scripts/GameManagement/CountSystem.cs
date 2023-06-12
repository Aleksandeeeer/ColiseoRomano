using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Collections;

public class CountSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera that will follow the winner")]
    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    [Tooltip("The camera that will follow the cesar")]
    private CinemachineVirtualCamera _cesarVirtualCamera;
    [SerializeField]
    [Tooltip("The game object that will activate when there is a winner")]
    private GameObject _winnerUIObject;
    [SerializeField]
    [Tooltip("The game object that will activate when there is a tie")]
    private GameObject _tieUIObject;
    [SerializeField]
    [Tooltip("The game object that will always activate to allow the player to exit or replay")]
    private GameObject _replayUI;
    [SerializeField]
    private GameObject _cesarObject;

    [Header("Cinematic timings")]
    [SerializeField]
    private float _winnerShowTime = 1f;
    [SerializeField]
    private float _cesarShowTime = 1f;
    [SerializeField]
    private float _loserShowTime = 1f;


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
        int winner = -1;
        for(int i = 0; i < _playerList.Count; ++i)
        {
            CoinObtainer co = _playerList[i].GetComponent<CoinObtainer>();
            if (co.Coins > max)
            {
                tie = false;
                max = co.Coins;
                winner = i;
            } else if (co.Coins == max)
            {
                tie = true;
                winner = -1;
            }
        }

        if (tie || _playerList.Count == 1)
        {
            _tieUIObject.SetActive(true);
            _replayUI.SetActive(true);
        } else
        {
            StartCoroutine(ShowEndgameAnimation(winner));
        }
    }

    private IEnumerator ShowEndgameAnimation(int winner)
    {
        int loser = 1 - winner;
        var winnerObject = _playerList[winner];
        var loserObject = _playerList[loser];

        // Follow the winner
        _virtualCamera.Follow = winnerObject.transform;
        _virtualCamera.LookAt = winnerObject.transform;
        _virtualCamera.Priority = 50;
        _cesarVirtualCamera.Priority = 0;
        winnerObject.GetComponentInChildren<Animator>().SetTrigger("Victory");
        yield return new WaitForSeconds(_winnerShowTime);

        // Show the cesar
        _virtualCamera.Priority = 0;
        _cesarVirtualCamera.Priority = 50;
        yield return new WaitForSeconds(0.5f);
        _cesarObject.GetComponentInChildren<Animator>().SetTrigger("Cesar");
        yield return new WaitForSeconds(_cesarShowTime);

        // Show the loser
        _virtualCamera.Follow = loserObject.transform;
        _virtualCamera.LookAt = loserObject.transform;
        _cesarVirtualCamera.Priority = 0;
        _virtualCamera.Priority = 50;
        loserObject.GetComponentInChildren<Animator>().SetTrigger("Defeat");
        yield return new WaitForSeconds(_loserShowTime);
        //_winnerUIObject.SetActive(true);

        _replayUI.SetActive(true);
    }
}
