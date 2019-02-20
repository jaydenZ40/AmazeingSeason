using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckElementHolder : MonoBehaviour
{
    private int requiredElementNum = 2;
    private char c;
    private bool matchedElement = false;
    void Awake()
    {
        PlayerController.instance.onOperateElement.AddListener(DroppedElement);
        c = this.transform.name[1]; // distinguish by letter: spring = p, summer = u, fall = a, winter = i
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name[1] == c)
        {
            matchedElement = !matchedElement;
            print(requiredElementNum);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        //if (matchedElement && PlayerController.instance.elementName == "empty")
        //{
        //    requiredElementNum--;
        //}
        matchedElement = !matchedElement; 
        if (requiredElementNum == 0)
            print("Season completed");
    }
    void DroppedElement(string str)
    {
        if (str == "empty" && matchedElement) // If the correct element touches the holder, mark the boolean value to true. If player leaves without the element, requiredElementNum--
            requiredElementNum--;
        else return;
    }
}
