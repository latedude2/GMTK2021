using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MusicConductor : MonoBehaviour
{
    public float songBPM;
    
    //The offset to the first beat of the song in seconds
    public float firstBeatOffset;

    public BlockBeater blockBeater;

    public float distanceBetweenBlocks;

    [Header("Song trackers. DO NOT SET THESE")]
    //Current song position, in seconds
    public static float songPosition;

    //Current song position, in beats
    public static float songPositionInBeats;

    //The number of seconds for each song beat
    public static float secPerBeat;

    //How many seconds have passed since the song started
    public static float dspSongTime;

    private AudioSource[] musicSource = new AudioSource[10];
    public float volumeWhenLayerActive = 0.26f;

    private PostProcessingControl post;

    private int oldBeatVal;

    public GameObject postProcessing;

    private void Awake()
    {
        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBPM;
    }

    void Start()
    {
        postProcessing = GameObject.Find("PostProcessing");
        post = postProcessing.GetComponent<PostProcessingControl>();

        //Record the time when the music starts
        dspSongTime = (float) AudioSettings.dspTime;
        int i = 0;
        foreach (Transform child in transform)
        {
            musicSource[i] = child.GetComponent<AudioSource>();
            musicSource[i].Play();
            i++;
        }

        InvokeRepeating(nameof(CreateBlock), firstBeatOffset, secPerBeat);

        AddMusicLayer();

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

    public void AddMusicLayer()
    {
        List<AudioSource> filtered = new List<AudioSource>(musicSource).Where(x => x.volume == 0).ToList();
        if (filtered.Count > 0)
        {
            int i = Random.Range(0, filtered.Count);
            if (filtered[i].volume == 0)
            {
                StartCoroutine(IncreaseMusicLayerVolume(i));
            } 
        }
    }

    private IEnumerator IncreaseMusicLayerVolume(int i)
    {
        while (musicSource[i].volume < volumeWhenLayerActive)
        {
            musicSource[i].volume += 0.0003f;
            yield return null;
        }
    }

    public void RemoveMusicLayer()
    {
        List<AudioSource> filtered = new List<AudioSource>(musicSource).Where(x => x.volume > 0).ToList();
        if (filtered.Count > 0)
        {
            int i = Random.Range(0, filtered.Count);
            if (filtered[i].volume > 0)
            {
                StartCoroutine(DecreaseMusicLayerVolume(i));
            }
        }
    }

    private IEnumerator DecreaseMusicLayerVolume(int i)
    {
        while (musicSource[i].volume !<= 0)
        {
            musicSource[i].volume += 0.0003f;
            yield return null;
        }
    }
}
