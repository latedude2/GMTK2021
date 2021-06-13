using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    public void LoadScene()
    {
        Debug.Log("Scene load called");
        SceneManager.LoadScene("SampleScene");
    }

    public void ResetScore()
    {
        Score.score = 60;
    }
}
