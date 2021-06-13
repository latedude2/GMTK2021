using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{

    void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
