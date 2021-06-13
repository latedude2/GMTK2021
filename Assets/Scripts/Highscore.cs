using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    void Start()
    {
        GetComponent<Text>().text = (Score.score - 60 + (Score.dancers - 3) * 50).ToString();
    }
}
