using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int completedSeason = 0;

    public static GameController instance;

    public GameObject SeasonPanel;

    private Elements Element;
    public GameObject wizard;
    public GameObject mainCamera;
    private Timer timer;
    public bool isTutorial { get; private set; }
    private bool restarted = false;
    public bool Restarted { get { return restarted; } }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            PlayerController.instance.onWrapGate.AddListener(ShowSeasonPanel);
            ElementController.checkProcess.AddListener(CheckSeason);
            AudioManager.instance.zortonComplete.AddListener(StartTimer);
            Physics2D.IgnoreLayerCollision(8, 9);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if ("TutorialLevel" == scene.name)
        {
            PlayerController.instance.Restart();
            isTutorial = true;
            AudioManager.instance.BGM_Play();
            PlayerController.instance.HideSprite(false);
        }
        else
            isTutorial = false;
        if ("Level 1" == scene.name)
        {
            Spaceship.instance.gameObject.SetActive(true);
            PlayerController.instance.HideSprite(false);
            Wizard.instance.gameObject.SetActive(true);
            Spaceship.instance.mainCamera = PlayerController.GetCamera();
        }
        else
        {
            PlayerController.instance.HideSprite(true);
            Wizard.instance.gameObject.SetActive(false);
            Spaceship.instance.gameObject.SetActive(false);
        }

    }

    internal void SetElememts(Elements elements)
    {
        Element = elements;
    }


    internal void SetTimer(Timer timer)
    {
        this.timer = timer;
        if (restarted)
            timer.StartTimer();
    }


    private void StartTimer()
    {
       Timer.instance.StartTimer();
    }

    private void Start()
    {
    }

    void ShowSeasonPanel()
    {
        SeasonPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        AudioManager.instance.GameOver();
        SceneManager.LoadScene(3);
        Wizard.instance.gameObject.SetActive(false);
        PlayerController.instance.gameObject.SetActive(false);
        Spaceship.instance.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        //PlayerController.instance.Restart();
        restarted = true;
        completedSeason = 0;
        Wizard.instance.gameObject.SetActive(true);
        Spaceship.instance.Restart();
        StartTimer();
        AudioManager.instance.BGM_Play(true);
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
        if (isTutorial && completedSeason == 1)
        {
            AudioManager.instance.BGM_Play(false);
            SceneManager.LoadScene("StoryScene1");
        }
    }

    IEnumerator winner()
    {
        Timer.instance.StopTimer();
        AudioManager.instance.BGM_Play(false);
        yield return new WaitForSeconds(0.5f);
        //Move the player to the spaceship
        PlayerController.instance.GetComponent<BoxCollider2D>().enabled = false;//so he won't get stuck
        var pVec = PlayerController.instance.transform.position;
        pVec -= Spaceship.instance.transform.position;
        pVec /= 50;
        for (int i = 0; i < 50; i++)
        {
            PlayerController.instance.transform.position -= pVec;
            yield return new WaitForSeconds(0.05f);
        }
        PlayerController.instance.GetComponent<BoxCollider2D>().enabled = true;
        //Hide the player to simulate entering the spaceship
        PlayerController.instance.GetComponent<SpriteRenderer>().sprite = null;
        AudioManager.instance.BGM_Play(false);
        AudioManager.instance.spaceship(true);
        for (int i = 1; i < 100; i++)
        {
            AudioManager.instance.spaceshipVolume();
            mainCamera.transform.parent = Spaceship.instance.transform;
            PlayerController.instance.gameObject.SetActive(false);
            Spaceship.instance.Fly();
            yield return new WaitForSeconds(0.05f);
        }
        mainCamera.transform.parent = PlayerController.instance.transform;
        mainCamera.transform.position = new Vector3(0, 0, -10);
        AudioManager.instance.Winner();
        Wizard.instance.gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }
}
