using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int completedSeason = 0;

    public static GameController instance;
    public GameObject SeasonPanel;
    void Start() // a bug when uses Awake instead of Start...?!
    {
        instance = this;
        PlayerController.instance.onWrapGate.AddListener(ShowSeasonPanel);
        //CheckElementHolder.instance.onCompleteSeason.AddListener(CheckProcess);
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    void ShowSeasonPanel()
    {
        SeasonPanel.SetActive(true);
        Time.timeScale = 0;
    }

    //void CheckProcess(char c)
    //{
    //    string season = "";
    //    switch (c)
    //    {
    //        case 'p':
    //            season = "Spring";
    //            break;
    //        case 'u':
    //            season = "Summer";
    //            break;
    //        case 'a':
    //            season = "Fall";
    //            break;
    //        case 'i':
    //            season = "Winter";
    //            break;
    //    }
    //    completedSeason++;
    //    print("Seasons " + season + " completed!"); // edit here: something happens for this season.
    //    if (completedSeason == 4)
    //    {
    //        print("All seasons completed!"); // edit here: something happens, load another scene
    //    }
    //}
}
