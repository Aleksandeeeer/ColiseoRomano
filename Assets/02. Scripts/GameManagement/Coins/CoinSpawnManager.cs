using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The pool of coins from which to retrieve them")]
    private CoinSpawnPool _coinsPool;
    [SerializeField]
    [Tooltip("How many coins will appear on start?")]
    private int _startingCoins = 10;
    [SerializeField]
    [Tooltip("How many coins will appear at a time?")]
    private int _parallelSpawns = 1;
    [SerializeField]
    [Tooltip("How much time will pass between coin spawns?")]
    private float _timeBetweenSpawns = 3f;

    private List<Transform> _coinSpawnPositions;

    private void Awake()
    {
        _coinSpawnPositions = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _coinSpawnPositions.Add(transform.GetChild(i));
        }
    }

    private void OnEnable()
    {
        GameTimer.OnTimerEnded += OnTimerEnded;
    }

    private void OnDisable()
    {
        GameTimer.OnTimerEnded -= OnTimerEnded;
    }

    private void OnTimerEnded()
    {
        StopAllCoroutines();
    }

    private Transform GetSpawnPosition()
    {
        if (_coinSpawnPositions.Count > 0)
        {
            int index = Random.Range(0, _coinSpawnPositions.Count);
            var t = _coinSpawnPositions[index];
            _coinSpawnPositions.Remove(t);
            return t;
        }
        return null;
    }

    public void ReturnTransformToPool(Transform t)
    {
        _coinSpawnPositions.Add(t);
    }

    private void Start()
    {
        for(int i = 0; i < _startingCoins; ++i)
        {
            SpawnCoin();
        }
        for(int i = 0; i < _parallelSpawns; i++)
        {
            StartCoroutine(SpawnThread());
        }
    }

    private IEnumerator SpawnThread()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenSpawns);
            SpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        Transform t = GetSpawnPosition();
        if (t == null) return;
        GameObject coin = _coinsPool.SpawnCoin(t.position);
        if (coin == null)
        {
            ReturnTransformToPool(t);
            return;
        }

        var coinBehaviour = coin.GetComponent<CoinBehaviour>();
        coinBehaviour.SetSpawnReference(t);
        coinBehaviour.OnCoinGot += OnCoinGot;
    }

    private void OnCoinGot(CoinBehaviour coinBehaviour)
    {
        coinBehaviour.OnCoinGot -= OnCoinGot;
        ReturnTransformToPool(coinBehaviour.SpawnReference);
        _coinsPool.ReturnToPool(coinBehaviour.gameObject);
    }
}
