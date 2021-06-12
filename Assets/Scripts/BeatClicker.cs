using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatClicker : MonoBehaviour
{

    public void OnBeatButtonClicked()
    {
        ScoreManager.ProcessClick(Timer.timeLeftToBeat);
    }
}
