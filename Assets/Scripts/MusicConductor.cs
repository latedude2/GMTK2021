using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicConductor : MonoBehaviour
{
    public float songBPM;
    
    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    public BlockBeater blockBeater;

    public float distanceBetweenBlocks = 800;

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

    private PostProcessingControl post;

    private int oldBeatVal;

    private void Awake()
    {
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBPM;
    }

    void Start()
    {
        post = GameObject.Find("PostProcessing").GetComponent<PostProcessingControl>();
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource = GetComponent<AudioSource>();

        InvokeRepeating(nameof(CreateBlock), firstBeatOffset, secPerBeat);

        musicSource.Play();

        oldBeatVal = 1000;
    }

    void FixedUpdate()
    {
        //determine how many seconds since the song started
        songPosition = (float) (AudioSettings.dspTime - dspSongTime - firstBeatOffset);

        //determine how many beats since the song started
        songPositionInBeats = songPosition / secPerBeat;

        int newBeatVal = (int) (songPositionInBeats + 1000);
        if (newBeatVal != oldBeatVal) 
        {
            OnBeat(); 
            oldBeatVal = newBeatVal; 
        }
    }

    void OnBeat()
    {
        post.AdjustColorGrading();
    }

    void CreateBlock()
    {
        blockBeater.CreateNewDroppingBlock();
    }
}
