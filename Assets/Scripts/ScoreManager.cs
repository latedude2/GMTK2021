using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScoreSystem
{
    public class ScoreManager : MonoBehaviour
    {

        public static float score = 60;

        private Text scoreText;

        void Start()
        {
            scoreText = GetComponent<Text>();
            scoreText.text = "Score: " + score;
        }

        private void Update()
        {
            scoreText.text = "Score: " + (score-60).ToString();
            if (score < 60)
                score = 60;
        }

        public static void MissedTarget()
        {
            score -= 5;
        }

        public static ScoreType CalculateScore(float yPosition, float lowTopMargin, float lowBottomMargin, float highTopMargin, float highBottomMargin)
        {
            if (yPosition >= lowBottomMargin && yPosition <= lowTopMargin)
            {
                score += 3;
                return ScoreType.Hit;
            }
            else if (yPosition >= highBottomMargin && yPosition <= highTopMargin)
            {
                score += 1;
                return ScoreType.AverageHit;
            }
            else
            {
                score -= 5;
                return ScoreType.Miss;
            }
        }
    }

    public enum ScoreType
    {
        Hit, AverageHit, Miss
    }
}
