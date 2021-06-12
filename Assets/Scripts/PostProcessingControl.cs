using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using ScoreSystem;


public class PostProcessingControl : MonoBehaviour
{
    float maxScore = 5000;
    private ChromaticAberration chromaticAberration;
    private Bloom bloom;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = transform.GetComponent<PostProcessVolume>();
        //Get variables for adjusting post processing effects
        volume.profile.TryGetSettings(out chromaticAberration);
        volume.profile.TryGetSettings(out bloom);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Map(0, 15, 0, maxScore, ScoreManager.score));
        bloom.intensity.value = Map(0, 15, 0, maxScore, ScoreManager.score);
        chromaticAberration.intensity.value = Map(0, 1, 0, maxScore, ScoreManager.score); ;
    }

    public float Map(float from, float to, float from2, float to2, float value)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }
}
