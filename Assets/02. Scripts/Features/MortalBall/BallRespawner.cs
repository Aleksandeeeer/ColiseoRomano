using System.Collections;
using UnityEditor;
using UnityEngine;

public class BallRespawner : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How much time will pass before the ball respawns")]
    private float _respawnTime = 3f;

    [SerializeField]
    [Tooltip("To which position will the ball return after respawning")]
    private Vector3 _returnToPosition;

    [SerializeField]
    [Tooltip("Variation in position in which the ball can appear")]
    [Range(0.01f, 0.5f)]
    private float _positionVariation = 0.2f;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
        transform.position = new Vector3(0f, -1000f, 0f);
    }

    private void OnEnable()
    {
        GameTimer.OnPreparationEnded += OnPreparationEnded;
        GameTimer.OnTimerEnded += OnGameFinished;
    }

    private void OnDisable()
    {
        GameTimer.OnPreparationEnded -= OnPreparationEnded;
        GameTimer.OnTimerEnded -= OnGameFinished;
    }

    private void OnGameFinished()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }

    private void OnPreparationEnded()
    {
        Spawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            Debug.Log("Ball fell");
            StartCoroutine(RespawnCoroutine());
        }
    }

    private void Update()
    {
        if (_rb.IsSleeping() && !_rb.isKinematic)
        {
            _rb.WakeUp();
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        _rb.isKinematic = true;
        transform.position = transform.position - Vector3.up * 100f;
        yield return new WaitForSeconds(_respawnTime);
        Spawn();
    }

    private void Spawn()
    {
        Vector3 targetPosition = _returnToPosition;
        targetPosition += Vector3.right * Random.Range(-_positionVariation, _positionVariation);
        targetPosition += Vector3.forward * Random.Range(-_positionVariation, _positionVariation);
        transform.position = targetPosition;

        _rb.isKinematic = false;
    }
}
