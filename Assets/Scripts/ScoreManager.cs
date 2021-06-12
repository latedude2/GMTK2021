using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static float lowMisclickMargin = MusicConductor.secPerBeat * 0.1f;
    public static float highMisclickMargin = MusicConductor.secPerBeat * 0.15f;

    public static float score = 60;

    private Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "Score: " + score;
    }

    private void Update()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    public static void ProcessClick(float timeLeftToBeat)
    {
        if (timeLeftToBeat < lowMisclickMargin || timeLeftToBeat > MusicConductor.secPerBeat - lowMisclickMargin)
        {
            score += 10;
        } 
        else if (timeLeftToBeat < highMisclickMargin || timeLeftToBeat > MusicConductor.secPerBeat - highMisclickMargin) 
        {
            score++;
        } 
        else
        {
            score -= 5;
        }
    }
}
