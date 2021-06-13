using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public int gameTimeInSec = 30;

    private float timeLeft;
    private Text timeText;

    private bool isStopped;

    private void Start()
    {
        timeLeft = gameTimeInSec;
        timeText = GetComponent<Text>();

        int mins = Mathf.RoundToInt(gameTimeInSec / 60);
        int secs = Mathf.RoundToInt(gameTimeInSec % 60);

        timeText.text = mins.ToString("00") + ":" + secs.ToString("00");
    }

    private void FixedUpdate()
    {
        if (!isStopped)
        {
            timeLeft = Mathf.Max(0, timeLeft - Time.fixedDeltaTime);

            int mins = Mathf.RoundToInt(timeLeft / 60);
            int secs = Mathf.RoundToInt(timeLeft % 60);

            timeText.text = mins.ToString("00") + ":" + secs.ToString("00");

            if (mins == 0 && secs == 0)
            {
                Debug.Log("highscoreScene is being loaded");
                SceneManager.LoadScene("HighscoreScene");
                isStopped = true;
            }
        }
    }
}
