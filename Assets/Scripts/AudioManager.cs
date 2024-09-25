using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Asegura que el AudioManager persista entre escenas
        }
        else
        {
            Destroy(gameObject); // Destruye instancias adicionales del AudioManager
        }
    }

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on AudioManager game object.");
        }
    }

    public void ReproducirSonido(AudioClip audio)
    {
        if (audioSource != null && audio != null)
        {
            audioSource.PlayOneShot(audio);
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is null.");
        }
    }

    public void StopSonido()
    {
        if (audioSource != null)
        {
            audioSource.Pause();
        }
        else
        {
            Debug.LogError("AudioSource is null.");
        }
    }

    public void ResumeSonido()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource is null.");
        }
    }
}
