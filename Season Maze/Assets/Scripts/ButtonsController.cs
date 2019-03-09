using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public void OnSpring()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-12.5f, 10.5f, 0);
        HideSeasonPanel();
    }

    public void OnSummer()
    {
        PlayerController.instance.rb.transform.position = new Vector3(13.5f, 10.5f, 0);
        HideSeasonPanel();
    }

    public void OnFall()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-13f, -11f, 0);
        HideSeasonPanel();
    }

    public void OnWinter()
    {
        PlayerController.instance.rb.transform.position = new Vector3(12f, -9f, 0);
        HideSeasonPanel();
    }

    void HideSeasonPanel()
    {
        GameController.instance.SeasonPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
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
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void OnTutorial()
    {
        SceneManager.LoadScene(4);
    }
}
