using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField]
    [Tooltip("To which position will the player return when falling?")]
    private Vector3 _returnToPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            Debug.Log("Player fell");
            transform.position = _returnToPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fall"))
        {
            Debug.Log("Player got hit");
            transform.position = _returnToPosition;
        }
    }
}
