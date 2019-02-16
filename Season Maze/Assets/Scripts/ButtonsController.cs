using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public void OnSpring()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-1.5f, 1.5f, 0);
        HideSeasonPanel();
    }
    public void OnSummer()
    {
        PlayerController.instance.rb.transform.position = new Vector3(1.5f, 1.5f, 0);
        HideSeasonPanel();
    }
    public void OnFall()
    {
        PlayerController.instance.rb.transform.position = new Vector3(-1.5f, -1.5f, 0);
        HideSeasonPanel();
    }
    public void OnWinter()
    {
        PlayerController.instance.rb.transform.position = new Vector3(1.5f, -1.5f, 0);
        HideSeasonPanel();
    }
    void HideSeasonPanel()
    {
        GameController.instance.SeasonPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
