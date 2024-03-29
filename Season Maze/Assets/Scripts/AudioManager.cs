﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public AudioSource m_BGM_Manager, m_SFX_KeyPickup, m_SFX_ElementPickup, SFX_YouWon, SFX_GameOver, 
        m_SFX_ElementReturn, m_SFX_UnlockDoor, m_DLG_Zorton, m_SFX_Spaceship, m_SFX_Explosion, m_SFX_Crash,
        DLG_Story1, DLG_Story2;
    private float timer = 0;
    public UnityEvent zortonComplete = new UnityEvent();
    private bool BGM_Started = false;
    private bool BGM_Stopped = true;
    private bool greetingStarted = false;
    private bool timerRunning = false;
    void Awake()
    {
        if (null == instance)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
            timer += Time.deltaTime;
        //Debug.Log("timer = " + timer.ToString("0.0000"));
        if (45 < timer && !BGM_Stopped)
        {
            timer = 0;
            StartCoroutine(IncreasePitch());
        }
        if (!BGM_Started && greetingStarted && !m_DLG_Zorton.isPlaying)
        {
            BGM_Started = true;
            BGM_Play();
            zortonComplete.Invoke();
        }

    }

    internal void PlayerStarted()
    {
        PlayerController.instance.onKeyPickup.AddListener(pickupKey);
        PlayerController.instance.onElementPickup.AddListener(pickupElement);
        PlayerController.instance.onElementReturn.AddListener(returnElement);
    }

    internal void GameOver()
    {
        BGM_Play(false);
        SFX_GameOver.Play();
    }

    public void BGM_Play(bool play = true, bool runTimer = true)
    {
        BGM_Stopped = !play;
        if (play)
        {
            m_BGM_Manager.Play();
            timerRunning = runTimer;
            timer = 0;
            m_BGM_Manager.volume = .254f;
            m_BGM_Manager.pitch = 1;
        }
        else
        {
            m_BGM_Manager.Stop();
            StopAllCoroutines();
            timerRunning = runTimer;
            timer = 0;
        }

    }

        IEnumerator IncreasePitch()
    {
        //Increase pitch 10 percent and restart
        for (int i = 0; 0 < m_BGM_Manager.volume; i++)
        {
            if (BGM_Stopped)
                break;
            m_BGM_Manager.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        if (!BGM_Stopped)
        {
            m_BGM_Manager.pitch += 0.1f;
            m_BGM_Manager.Stop();
            m_BGM_Manager.Play();
        }
        for (int i = 0; 0.5 > m_BGM_Manager.volume; i++)
        {
            if (BGM_Stopped)
               break;
            m_BGM_Manager.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void pickupKey()
    {
        m_SFX_KeyPickup.GetComponent<AudioSource>().Play();
    }

    internal void Winner()
    {
        spaceship(false);
        BGM_Play(false);
        SFX_YouWon.Play();
    }

    void pickupElement()
    {
        m_SFX_ElementPickup.GetComponent<AudioSource>().Play();
    }
    void returnElement()
    {
        m_SFX_ElementReturn.GetComponent<AudioSource>().Play();
    }

    public void unlockDoor()
    {
        m_SFX_UnlockDoor.GetComponent<AudioSource>().Play();
    }

    public void spaceship(bool play, float volume = 1.0f)
    {
        m_SFX_Spaceship.GetComponent<AudioSource>().volume = volume;
        if (play)
            m_SFX_Spaceship.GetComponent<AudioSource>().Play();
        else
            m_SFX_Spaceship.GetComponent<AudioSource>().Stop();
    }

    public void spaceshipVolume()
    {
        var v = m_SFX_Spaceship.volume;
        m_SFX_Spaceship.volume = v * 0.95f;
    }

    public void wizardGreeting()
    {
        m_DLG_Zorton.Play();
        greetingStarted = true;
        BGM_Started = false;
    }

    public void explosion(bool play)
    {
        if (play)
            m_SFX_Explosion.Play();
        else
            m_SFX_Explosion.Stop();
    }

    public void crash()
    {
        m_SFX_Crash.Play();
    }
}
