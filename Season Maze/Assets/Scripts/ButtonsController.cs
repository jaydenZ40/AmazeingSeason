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
        GameController.instance.Restart();
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
