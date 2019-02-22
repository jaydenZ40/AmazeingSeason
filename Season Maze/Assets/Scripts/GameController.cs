using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int completedSeason = 0;

    public static GameController instance;
    public GameObject SeasonPanel;
    public GameObject Element;

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
        print(c);
        int season = 0;
        switch (c)
        {
            case 'p':
                season = 0;
                break;
            case 'u':
                season = 1;
                break;
            case 'a':
                season = 2;
                break;
            case 'i':
                season = 3;
                break;
        }
        if (Element.transform.GetChild(season).childCount == 0)
        {
            completedSeason++;
            print("Season " + (season + 1) + " completed!"); // edit here: something happens for this season.
        }
        if (completedSeason == 4)
        {
            print("All seasons completed!"); // edit here: something happens, load another scene
        }
    }
}
