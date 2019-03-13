using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyController : MonoBehaviour
{

    public static NoDestroyController instance;
    
    public bool isHard = false;
    public bool isCrazy = false;
    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    
}
