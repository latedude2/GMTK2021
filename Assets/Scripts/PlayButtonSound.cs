using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        audioSource.Play();
        Debug.Log("Playing sound");
    }

    public void StopSound()
    {
        audioSource.Stop();
        Debug.Log("Stopping sound");

    }
}
