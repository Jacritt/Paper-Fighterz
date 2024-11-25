using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();   
    }
    public static MusicManager musicManager;
    void Awake()
    {
        if (musicManager == null)
        {
            musicManager = this;
            DontDestroyOnLoad(this);
        }
        else if (musicManager != (this))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    public void PlayMusic(AudioClip audioClip) { 
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
