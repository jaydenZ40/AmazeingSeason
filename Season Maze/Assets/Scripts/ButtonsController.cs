using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public void OnSpring()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-12.5f, 10f, 0);
        HideSeasonPanel();
    }

    public void OnSummer()
    {
        PlayerController.instance.rb.transform.position = new Vector3(14.5f, 11.5f, 0);
        HideSeasonPanel();
    }

    public void OnFall()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-12.5f, -9.5f, 0);
        HideSeasonPanel();
    }

    public void OnWinter()
    {
        PlayerController.instance.rb.transform.position = new Vector3(12.5f, -7.5f, 0);
        HideSeasonPanel();
    }

    void HideSeasonPanel()
    {
        GameController.instance.SeasonPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("ChooseModes");
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnResume()
    {
        Pause.instance.TogglePause();
    }

    public void OnRestart()
    {
        GameController.instance.Restart();
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene("Start");
    }
    public void OnTutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }
    public void OnNextScene1()
    {
        SceneManager.LoadScene("StoryScene2");
    }
    public void OnNextScene2()
    {
        SceneManager.LoadScene("StoryScene3");
    }
    public void OnNextScene3()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void OnHard()
    {
        SceneManager.LoadScene("StoryScene1");
        GameController.instance.Challenge = GameController.ChallengeLevels.Hard;
//        NoDestroyController.instance.isHard = true;
//        NoDestroyController.instance.isCrazy = false;
    }

    public void OnEasy()
    {
        SceneManager.LoadScene("StoryScene1");
        GameController.instance.Challenge = GameController.ChallengeLevels.Easy;
        //        NoDestroyController.instance.isHard = false;
        //        NoDestroyController.instance.isCrazy = false;
    }
    public void OnCrazy()
    {
        SceneManager.LoadScene("StoryScene1");
        GameController.instance.Challenge = GameController.ChallengeLevels.Crazy;
        //        NoDestroyController.instance.isHard = false;
        //        NoDestroyController.instance.isCrazy = true;
    }
}
