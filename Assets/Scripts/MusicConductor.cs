using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicConductor : MonoBehaviour
{
    public float songBPM;
    
    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    public float musicOffset;

    public BlockBeater blockBeater;

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

    private float oldBeatVal;

    private bool isFirstBlockCreated;
    private bool hasMusicStarted;

    private float musicOffsetCounter;

    private void Awake()
    {
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBPM;
        musicOffset = 800 / DroppingBlock.speed;
    }

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!hasMusicStarted)
        {
            if (!isFirstBlockCreated)
            {
                blockBeater.CreateNewDroppingBlock();
                isFirstBlockCreated = true;
            }

            musicOffsetCounter += Time.fixedDeltaTime;

            if (musicOffsetCounter >= musicOffset)
                StartMusic();
        } 
        else
        {
            //determine how many seconds since the song started
            songPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            float newBeatVal = (int) (songPositionInBeats + 1000);
            if (newBeatVal != oldBeatVal)
            {
                blockBeater.CreateNewDroppingBlock();
                oldBeatVal = newBeatVal;
            }
        }
    }

    void StartMusic()
    {
        //Record the time when the music starts
        dspSongTime = (float) AudioSettings.dspTime;

        musicSource.Play();
        hasMusicStarted = true;
    }
}
