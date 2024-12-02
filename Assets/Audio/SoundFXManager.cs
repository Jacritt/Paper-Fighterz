using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.Dependencies;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager soundFXManager;
    public AudioSource soundFXObject;

    public float minPitch = 1f;
    public float maxPitch = 1f;

    private void Awake()
    {
        // If there is no instance, set the current instance
        if (soundFXManager == null)
        {
            soundFXManager = this;
            // Don't destroy this GameObject when loading a new scene
            DontDestroyOnLoad(gameObject);
        }
        else if (soundFXManager != this)
        {
            // Destroy duplicate instances
            Destroy(gameObject);
        }

        // Ensure we have an AudioSource attached
        
    }

    public void PlaySoundEffect(AudioClip audioClip){
        if (audioClip == null){return;}
        AudioSource audioSource = Instantiate(soundFXObject, new Vector3(0,0,0), quaternion.identity);
        audioSource.clip = audioClip ?? null;
        audioSource.pitch = Random.Range(0.9f, 1);
        audioSource.Play();
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
