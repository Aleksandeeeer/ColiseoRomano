using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The audio source with the music")]
    private AudioSource _musicSource;

    private void OnEnable()
    {
        GameTimer.OnPreparationEnded += OnPreparationEnded;
    }

    private void OnDisable()
    {
        GameTimer.OnPreparationEnded -= OnPreparationEnded;
    }

    private void OnPreparationEnded()
    {
        _musicSource.Play();
    }
}
