using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharUnityEvent : UnityEvent<char> { }
public class CheckElementHolder : MonoBehaviour
{
    private int requiredElementNum = 2;
    private char c;

    public static CheckElementHolder instance;
    //public CharUnityEvent onCompleteSeason = new CharUnityEvent();
    public StringUnityEvent onLocked = new StringUnityEvent();
    void Awake()
    {
        instance = this;
        c = this.transform.name[1]; // distinguish by letter: spring = p, summer = u, fall = a, winter = i
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name[1] == c)
        {
            onLocked.Invoke(other.transform.name);
            PlayerController.instance.onOperateElement.Invoke("empty");
            PlayerController.instance.elementName = "empty";
            requiredElementNum--;
            if (requiredElementNum == 0)
            {
                print("Seasons completed!");
            }
            //if (requiredElementNum == 0)
            //{
            //    onCompleteSeason.Invoke(c);
            //}
        }
    }
}
