using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioController : MonoBehaviour
{

    [SerializeField] private AudioSource _musicSource, _effectsSource;

    private static AudioController audioController;
    public static AudioController Instance
    {
        get
        {
            if (!audioController)
            {
                audioController = FindObjectOfType(typeof(AudioController)) as AudioController;

                if (!audioController)
                {
                    Debug.LogError("There needs to be one active AudioController script on a GameObject in your scene.");
                }

                else
                {
                    audioController.Init();
                }
            }

            return audioController;
        }
    }

    void Init()
    {

    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            audioController = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
