using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Timer timer;
    void Start()
    {
        GameController.instance.SetTimer(timer);
        PlayerController.instance.Restart();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
