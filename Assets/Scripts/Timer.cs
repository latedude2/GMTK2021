using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public static float timeLeftToBeat;

    private Text timerText;

    public void Awake()
    {
        timeLeftToBeat = MusicConductor.secPerBeat;
    }

    public void Start()
    {
        timerText = GetComponent<Text>();
    }

    public void FixedUpdate()
    {
        //Debug.Log("Updating time");
        timeLeftToBeat = Mathf.Max(0, timeLeftToBeat - Time.fixedDeltaTime);
        timerText.text = timeLeftToBeat.ToString("0.00");

        if (timeLeftToBeat <= 0)
        {
            timeLeftToBeat = MusicConductor.secPerBeat;
        }
    }
}
