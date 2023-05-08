using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnPool : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Where will the coins be moved when retrieving them?")]
    private Transform _coinsParent;

    private List<GameObject> _coinsInPool = new List<GameObject>();


    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            _coinsInPool.Add(transform.GetChild(i).gameObject);
        }
    }

    public void ReturnToPool(GameObject coinObject)
    {
        coinObject.transform.SetParent(transform);
        coinObject.SetActive(false);
        _coinsInPool.Add(coinObject);
    }

    public GameObject SpawnCoin(Vector3 position)
    {
        if (_coinsInPool.Count == 0) return null;
        GameObject coin = _coinsInPool[0];
        _coinsInPool.RemoveAt(0);
        coin.transform.SetParent(_coinsParent);
        coin.transform.position = position;
        coin.SetActive(true);
        return coin;
    }
}
