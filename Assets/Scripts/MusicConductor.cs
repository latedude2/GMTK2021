using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicConductor : MonoBehaviour
{
    public float songBPM;

    [Header("Song trackers. DO NOT SET THESE")]
    //Current song position, in seconds
    public static float songPosition;

    //Current song position, in beats
    public static float songPositionInBeats;

    //The number of seconds for each song beat
    public static float secPerBeat;

    //How many seconds have passed since the song started
    public static float dspSongTime;

    private AudioSource musicSource;

    private void Awake()
    {
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBPM;
    }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();

        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        musicSource.Play();
    }

    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - dspSongTime);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;
    }
}
