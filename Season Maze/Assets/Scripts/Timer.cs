using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public static Timer instance { get; private set; }
    private TextMeshProUGUI timeText;
    public float timeLeft = 240f; // 4 mins per round?
    private bool running = false;

    void Awake()
    {
        timeText = this.GetComponent<TextMeshProUGUI>();
//        if (null == instance)
//        {
            instance = this;
            timeText = this.GetComponent<TextMeshProUGUI>();
//        }
//        else
//            Destroy(this.gameObject);
        //DontDestroyOnLoad(this.gameObject);
    }

    public void StartTimer()
    {
        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    public void Restart()
    {
        timeLeft = 240f;
        running = true;
    }

    void Update()
    {
        if (!running)
            return;
        timeLeft -= Time.deltaTime;
        int mins = (int)(timeLeft / 60f);
        int sec = (int)(timeLeft % 60);
        timeText.text = string.Format("Timer: {0:00}", mins + ":" + sec.ToString("00"));
        if (timeLeft <= 0)
        {
            running = false;
            GameController.instance.GameOver();
            //print("game over");
            //SceneManager.LoadScene(3);
            // something happens?
        }
    }
}