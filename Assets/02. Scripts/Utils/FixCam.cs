using UnityEngine;
using Cinemachine;
using System.Collections;

public class FixCam : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _vcam;

    private IEnumerator Start()
    {
        var temp1 = _vcam.Follow;
        var temp2 = _vcam.LookAt;
        _vcam.Follow = null;
        _vcam.LookAt = null;
        yield return null;
        _vcam.Follow = temp1;
        _vcam.LookAt = temp2;
    }
}
