using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed at which the object will rotate")]
    private float _rotationSpeed = 5f;
    [SerializeField]
    [Tooltip("The vector which the object will rotate around")]
    private Vector3 _axis = Vector3.up;

    // Starts with a random rotation
    private void Start()
    {
        // Be sure no weird numbers are on the axis
        _axis.x = Mathf.Clamp01(_axis.x);
        _axis.y = Mathf.Clamp01(_axis.y);
        _axis.z = Mathf.Clamp01(_axis.z);

        var euler = transform.eulerAngles;
        euler += _axis * Random.value * 180f;
        transform.eulerAngles = euler;
    }

    // Each frame, rotate a little
    void Update()
    {
        var euler = transform.eulerAngles;
        euler += _axis * _rotationSpeed * Time.deltaTime;
        transform.eulerAngles = euler;
    }
}
