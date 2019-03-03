using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int completedSeason = 0;

    public static GameController instance;
    public GameObject SeasonPanel;
    public GameObject Element;
    public Timer timer;
    public GameObject wizard;

    void Awake()
    {
        instance = this;
        PlayerController.instance.onWrapGate.AddListener(ShowSeasonPanel);
        ElementController.checkProcess.AddListener(CheckSeason);
        AudioManager.instance.zortonComplete.AddListener(timer.StartTimer);
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    private void Start()
    {
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
            print("Season " + (season + 1) + " completed!"); // edit here: something happens for this season.
        }
        if (completedSeason == 4)
        {
            print("All seasons completed!"); // edit here: something happens, load another scene
            StartCoroutine(winner());
        }
    }

    IEnumerator winner()
    {
        AudioManager.instance.BGM_Play(false);
        yield return new WaitForSeconds(0.5f);
        PlayerController.instance.GetComponent<SpriteRenderer>().sprite = null;
        AudioManager.instance.BGM_Play(false);
        AudioManager.instance.spaceship(true);
        for (int i = 1; i < 100; i++)
        {
            AudioManager.instance.spaceshipVolume();
            Spaceship.instance.Fly();
            yield return new WaitForSeconds(0.05f);
        }
        AudioManager.instance.spaceship(false);
        AudioManager.instance.BGM_Play(false);
        timer.StopTimer();
        SceneManager.LoadScene(2);
    }
}
