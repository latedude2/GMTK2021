using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using ScoreSystem;


public class PostProcessingControl : MonoBehaviour
{
    float maxScore = 1000;
    private ChromaticAberration chromaticAberration;
    private Bloom bloom;
    private ColorGrading colorGrading;

    // Start is called before the first frame update
    void Start()
    {
        PostProcessVolume volume = transform.GetComponent<PostProcessVolume>();
        //Get variables for adjusting post processing effects
        volume.profile.TryGetSettings(out chromaticAberration);
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out colorGrading);
    }

    // Update is called once per frame
    void Update()
    {
        bloom.intensity.value = Map(0, 15, 0, maxScore, Score.score);
        chromaticAberration.intensity.value = Map(0, 1, 0, maxScore, Score.score); ;
    }

    public void AdjustColorGrading()
    {
        //Debug.Log("Adjusting color hue");
        colorGrading.hueShift.value = Random.Range(-180, 180);
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
