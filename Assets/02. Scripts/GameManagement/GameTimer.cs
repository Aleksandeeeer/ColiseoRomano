using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    [SerializeField]
    [Tooltip("How much time (in seconds) will the game last?")]
    private float _gameDuration = 60f;
    [SerializeField]
    [Tooltip("How much time (in seconds) will the preparation phase (players joining) last")]
    private float _preparationDuration = 5f;

    public static Action OnTimerEnded;
    public static Action OnPreparationEnded;

    public float Timer => _timer;
    public bool GameRunning => _running;
    public bool GamePreparing => _preparationRunning;

    private float _timer = 0f;
    private bool _running = false;
    private bool _preparationRunning = false;

    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        _timer = _preparationDuration;
    }

    private void OnEnable()
    {
        PlayerJoinNotifier.OnPlayerJoins += OnPlayerJoins;
    }

    private void OnDisable()
    {
        PlayerJoinNotifier.OnPlayerJoins -= OnPlayerJoins;
    }

    private void OnPlayerJoins(PlayerInput input)
    {
        if (!_running)
        {
            _preparationRunning = true;
        }
    }

    void Update()
    {
        if (_running)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _timer = 0f;
                _running = false;
                OnTimerEnded?.Invoke();
            }
        } else if (_preparationRunning)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _preparationRunning = false;
                _running = true;
                OnPreparationEnded?.Invoke();
                _timer = _gameDuration;
            }
        }
    }
}
