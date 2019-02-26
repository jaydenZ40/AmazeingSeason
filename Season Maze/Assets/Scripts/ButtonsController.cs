using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    public void OnSpring()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-1.5f, 2.5f, 0);
        HideSeasonPanel();
    }

    public void OnSummer()
    {
        PlayerController.instance.rb.transform.position = new Vector3(2.5f, 2.5f, 0);
        HideSeasonPanel();
    }

    public void OnFall()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-1.5f, -1.5f, 0);
        HideSeasonPanel();
    }

    public void OnWinter()
    {
        PlayerController.instance.rb.transform.position = new Vector3(2.5f, -1.5f, 0);
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
        Pause.instance.TogglePause();
        SceneManager.LoadScene(1);
    }
}
