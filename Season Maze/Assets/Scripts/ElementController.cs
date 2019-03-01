using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharUnityEvent : UnityEvent<char> { }

public class ElementController : MonoBehaviour
{
    private bool isFollowingPlayer = false;
    private GameObject tempParent;
    private bool islocked = false;

    public GameObject restoredElements;
    public static CharUnityEvent checkProcess = new CharUnityEvent();
    void Start()
    {
        PlayerController.instance.onOperateElement.AddListener(OnOperateElement);
    }

    void OnOperateElement(string str)
    {
        if (!islocked)
        {
            if (this.name == str) // avoid operating all listeners by matching object's name
            {
                tempParent = this.transform.parent.gameObject;
                this.transform.position = PlayerController.instance.rb.transform.position + new Vector3(0, 0.5f, 0);  // hold the Element above the player?
                this.transform.SetParent(PlayerController.instance.transform);
                isFollowingPlayer = !isFollowingPlayer;
            }
            else if (str == "empty" && isFollowingPlayer)
            {
                this.transform.SetParent(tempParent.transform); // put it back to the parent gameObject
                isFollowingPlayer = !isFollowingPlayer;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("ElementHolder") && this.name[1] == other.name[1])
        {
            this.transform.position = other.transform.position;
            this.transform.SetParent(restoredElements.transform);
            isFollowingPlayer = !isFollowingPlayer;
            islocked = true;
            checkProcess.Invoke(tempParent.name[1]);
        }
    }
}
