using UnityEngine;

public class PlayerStartingPosition : MonoBehaviour
{
    Vector3 _startingPosition;

    void Start()
    {
        transform.position = _startingPosition;
        transform.LookAt(Vector3.zero);
    }


    public void SetStartingPosition(Vector3 position)
    {
        _startingPosition = position;
    }
}
