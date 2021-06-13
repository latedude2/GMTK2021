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
    private float bloomAddition = 0;
    private float bloomFallSpeed = 10f;

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
        bloom.intensity.value = Map(0, 15, 0, maxScore, Score.score) + bloomAddition;
        chromaticAberration.intensity.value = Map(0, 1, 0, maxScore, Score.score);
        Debug.Log("Bloom addition: " + bloomAddition);
        if(bloomAddition > 0)
        {
            bloomAddition = bloomAddition - bloomFallSpeed * Time.deltaTime;
        }
    }

    public void AdjustColorGrading()
    {
        //Debug.Log("Adjusting color hue");
        colorGrading.hueShift.value = Random.Range(-180, 180);
    }

    public void BounceBloom()
    {
        bloomAddition = 5f;
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
