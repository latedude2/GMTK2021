using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulesButton : MonoBehaviour
{
    public GameObject rulesWindow;

    private void Start()
    {
        rulesWindow.SetActive(false);
    }

    public void OnRulesButtonClicked()
    {
        rulesWindow.SetActive(!rulesWindow.activeSelf);
    }
}
