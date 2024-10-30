using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip _clipWalk;
    [SerializeField] AudioClip _clipHurt;
    [SerializeField] AudioClip _clipBackground;
    [SerializeField] AudioClip _clipHome;

    [SerializeField] AudioClip _clipWin;

    [SerializeField] GameObject _audioSource;

    private void Awake()
    {
        PlaySound(SoundType.Background);
        MainController.OnPlaySound += PlaySound;
    }

    private void OnDestroy()
    {
        MainController.OnPlaySound -= PlaySound;
    }

    private void PlaySound(SoundType type)
    {
        var audioSource = Instantiate(_audioSource).GetComponent<AudioSource>();

        switch (type)
        {
            case SoundType.Walk:
                audioSource.clip = _clipWalk;
                break;
            case SoundType.Hurt:
                audioSource.clip = _clipHurt;
                break;
            case SoundType.Win:
                audioSource.clip = _clipWin;
                break;
            case SoundType.Background:
                audioSource.loop = true;
                audioSource.clip = _clipBackground;
                break;
            case SoundType.Home:
                audioSource.loop = true;
                audioSource.clip = _clipHome;
                break;
        }
        audioSource.Play();
    }
}

public enum SoundType
{
    Walk = 1,
    Background = 2,
    Win = 3,
    Home = 4,
    Hurt = 5,
}
