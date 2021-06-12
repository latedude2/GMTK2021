using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static float lowMisclickMargin = Timer.period * 0.1f;
    public static float highMisclickMargin = Timer.period * 0.15f;

    private static float score = 0;

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
        if (timeLeftToBeat < lowMisclickMargin || timeLeftToBeat > Timer.period - lowMisclickMargin)
        {
            score += 10;
        } 
        else if (timeLeftToBeat < highMisclickMargin || timeLeftToBeat > Timer.period - highMisclickMargin) 
        {
            score++;
        } 
        else
        {
            score -= 5;
        }
    }
}
