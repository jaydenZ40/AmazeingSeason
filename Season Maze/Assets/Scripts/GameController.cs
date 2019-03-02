using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int completedSeason = 0;
    private bool isFlash = false;

    public static GameController instance;
    public GameObject SeasonPanel;
    public GameObject Element;
    public GameObject MainCamera;
    public GameObject FlashImage;

    void Awake()
    {
        instance = this;
        PlayerController.instance.onWrapGate.AddListener(ShowSeasonPanel);
        ElementController.checkProcess.AddListener(CheckSeason);
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    void ShowSeasonPanel()
    {
        SeasonPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void CheckSeason(char c)
    {
        int season = PlayerController.instance.TranslateLetter(c);
        if (Element.transform.GetChild(season).childCount == 0)
        {
            completedSeason++;
            //print("Season " + (season + 1) + " completed!"); // edit here: something happens for this season.
        }
        if (completedSeason == 1)
        {
            //print("All seasons completed!"); // edit here: something happens, load another scene
            MainCamera.transform.parent = null;
            MainCamera.transform.position = new Vector3(0, 0, -10);
            Invoke("LoadVictory", 3);
            for (float i = 0.125f; i < 3; i += 0.125f)
            {
                Invoke("Flash", i);
            }
        }
    }
    void LoadVictory()
    {
        SceneManager.LoadScene(2);
    }
    void Flash()
    {
        FlashImage.SetActive(!isFlash);
        isFlash = !isFlash;
    }
}
