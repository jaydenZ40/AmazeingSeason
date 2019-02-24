using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioManager instance { get; private set; }
    public AudioSource m_BGM_Manager, m_SFX_Manager;
    private float timer = 0;
    void Awake()
    {
        if (null == instance)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log("timer = " + timer.ToString("0.0000"));
        if (60 < timer)
        {
            timer = 0;
            StartCoroutine(IncreasePitch());
        }
    }

    IEnumerator IncreasePitch()
    {
        //Increase pitch 10 percent by 1 percent increments
        for (int i = 0; 0 < m_BGM_Manager.volume; i++)
        {
            m_BGM_Manager.volume -= 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
        m_BGM_Manager.pitch += 0.1f;
        m_BGM_Manager.Stop();
        m_BGM_Manager.Play();
        for (int i = 0; 0.5 > m_BGM_Manager.volume; i++)
        {
            m_BGM_Manager.volume += 0.01f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
