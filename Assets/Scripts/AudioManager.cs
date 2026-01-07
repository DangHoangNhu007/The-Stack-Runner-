using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Clips")]
    public AudioClip collectClip;
    public AudioClip crashClip;
    public AudioClip winClip;

    private AudioSource _audioSource;

    void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollect()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(collectClip);
    }

    public void PlayCrash()
    {
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(crashClip);
    }
    
    public void PlayWin()
    {
        _audioSource.pitch = 1f;
        _audioSource.PlayOneShot(winClip);
    }
}