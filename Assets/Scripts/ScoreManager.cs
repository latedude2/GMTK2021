using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSystem
{
    public class ScoreManager : MonoBehaviour
    {

        private Text scoreText;

        void Start()
        {
            scoreText = GetComponent<Text>();
            scoreText.text = "Score: " + Score.score;
        }

        private void Update()
        {
            scoreText.text = "Score: " + (Score.score -60).ToString();
            if (Score.score < 60)
                Score.score = 60;
        }

        public static void MissedTarget()
        {
            Score.score -= 5;
        }

        public static ScoreType CalculateScore(float yPosition, float yTarget, float lowTopMargin, float lowBottomMargin, float highTopMargin, float highBottomMargin)
        {
            if ((int) yPosition == (int) yTarget)
            {
                Score.score += 5;
                return ScoreType.PerfectHit;
            }
            else if (yPosition >= lowBottomMargin && yPosition <= lowTopMargin)
            {
                Score.score += 3;
                return ScoreType.Hit;
            }
            else if (yPosition >= highBottomMargin && yPosition <= highTopMargin)
            {
                Score.score += 1;
                return ScoreType.AverageHit;
            }
            else
            {
                Score.score -= 5;
                return ScoreType.Miss;
            }
        }
    }

    public enum ScoreType
    {
        PerfectHit, Hit, AverageHit, Miss
    }
}
