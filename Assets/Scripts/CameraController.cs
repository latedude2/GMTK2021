using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public float maxCameraShake = 4.0f;
    public float maxScore = 5000.0f;
    public GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Map(0, maxCameraShake, 0, maxScore, ScoreManager.score);
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Map(0, maxCameraShake, 0, maxScore, ScoreManager.score);
        vcam.m_Lens.OrthographicSize = circle.GetComponent<CircleCollider2D>().radius * 7;
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
