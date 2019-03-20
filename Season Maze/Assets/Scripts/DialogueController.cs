using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    void Update()
    {
        if (Timer.instance.timeLeft < 240f)
        {
            this.transform.gameObject.SetActive(false);
        }
    }
}
