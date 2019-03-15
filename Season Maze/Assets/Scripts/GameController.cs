using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int completedSeason = 0;

    public static GameController instance;

    private GameObject SeasonPanel;

    private GameObject Element;
    private Camera mainCamera;
    private GameObject SeasonCompletePanel;

    private Timer timer;
    public bool isTutorial { get; private set; }
    private bool restarted = false;
    public bool Restarted { get { return restarted; } }

    public ChallengeLevels Challenge { get; set; }

    public enum ChallengeLevels
    {
        Crazy = 2,
        Hard = 4,
        Easy = 6
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            Challenge = ChallengeLevels.Easy;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    internal void SeasonPanelActive(bool active)
    {
        SeasonPanel = GameObject.Find("Canvas").transform.Find("SeasonPanel").gameObject;
        SeasonPanel.SetActive(active);
    }

    internal void SeasonCompletePanelActive(bool active)
    {
        SeasonCompletePanel = GameObject.Find("Canvas").transform.Find("TutorialCompleted").gameObject;
        SeasonCompletePanel.SetActive(active);
    }

    private void Start()
    {
        AudioManager.instance.zortonComplete.AddListener(StartTimer);
        Physics2D.IgnoreLayerCollision(8, 9);
        //PlayerController.instance.Hide(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        completedSeason = 0;
        if ("TutorialLevel" == scene.name)
        {
            isTutorial = true;
            Element = GameObject.Find("Elements");
            ElementController.checkProcess.AddListener(CheckSeason);
            AudioManager.instance.BGM_Play();
            PlayerController.instance.StartPlayer();
        }
        else
            isTutorial = false;
        if ("Level 1" == scene.name)
            SetupLevelOne();
        if ("StoryScene1" == scene.name)
        {
            AudioManager.instance.BGM_Play(false, false);
            AudioManager.instance.spaceship(true, 0.125f);
        }
        if ("StoryScene3" == scene.name)
            StartCoroutine(CrashShip());
        if ("Start" == scene.name)
            AudioManager.instance.BGM_Play(true, false);
 
    }

    private void SetupLevelOne()
    {
        Element = GameObject.Find("Elements");
        PlayerController.instance.onWrapGate.AddListener(ShowSeasonPanel);
        ElementController.checkProcess.AddListener(CheckSeason);
        AudioManager.instance.spaceship(false);
        PlayerController.instance.Hide(false);
        Spaceship.instance.mainCamera = PlayerController.GetCamera();

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

    void ShowSeasonPanel()
    {
        SeasonPanelActive(true);
        Time.timeScale = 0;
    }

    public void GameOver()
    {
        AudioManager.instance.GameOver();
        SceneManager.LoadScene(3);
        //Wizard.instance.gameObject.SetActive(false);
        //PlayerController.instance.gameObject.SetActive(false);
        //Spaceship.instance.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        //PlayerController.instance.Restart();
        restarted = true;
        completedSeason = 0;
        //Wizard.instance.gameObject.SetActive(true);
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
            SeasonCompletePanelActive(true);

            /********************************************************************************************
            // Bug here! when finish tutorial and then back to main menu, the player remains on the screen
            // Fixed player, wizard and spaceship moved above camera on all scenes except level 1 and
            // player not moved on tutorial level.
            *********************************************************************************************/
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
        PlayerController.instance.Hide(true);
        AudioManager.instance.BGM_Play(false);
        AudioManager.instance.spaceship(true);
        mainCamera = PlayerController.instance.camera;
        for (int i = 1; i < 100; i++)
        {
            AudioManager.instance.spaceshipVolume();
            mainCamera.transform.parent = Spaceship.instance.transform;
            //PlayerController.instance.gameObject.SetActive(false);
            Spaceship.instance.Fly();
            yield return new WaitForSeconds(0.05f);
        }
        mainCamera.transform.parent = PlayerController.instance.transform;
        mainCamera.transform.position = new Vector3(0, 0, -10);
        AudioManager.instance.Winner();
        Wizard.instance.gameObject.SetActive(false);
        SceneManager.LoadScene(2);
    }

    IEnumerator CrashShip()
    {
        for (int i = 0; i < 100; i++)
        {
            AudioManager.instance.spaceshipVolume();
            yield return new WaitForSeconds(0.01f);
        }
        AudioManager.instance.spaceship(false);
        AudioManager.instance.crash();
    }
}
