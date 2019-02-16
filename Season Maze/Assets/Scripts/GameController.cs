using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject SeasonPanel;
    void Start() // a bug when uses Awake instead of Start...?!
    {
        instance = this;
        PlayerController.instance.onWrapGate.AddListener(ShowSeasonPanel);
    }
    void ShowSeasonPanel()
    {
        SeasonPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
