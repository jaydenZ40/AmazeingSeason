using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    private bool isFollowingPlayer = false;
    private GameObject tempParentHolder;
    private bool islocked = false;

    void Start()
    {
        PlayerController.instance.onOperateElement.AddListener(OnOperateElement);
        CheckElementHolder.instance.onLocked.AddListener(OnLocked);
    }

    void OnOperateElement(string str)
    {
        if (this.name == str && !islocked) // avoid operating all listeners by matching object's name
        {
            tempParentHolder = this.transform.parent.gameObject;
            this.transform.position = PlayerController.instance.rb.transform.position + new Vector3(0, 0.5f, 0);  // hold the Element above the player?
            this.transform.SetParent(PlayerController.instance.transform);
            isFollowingPlayer = !isFollowingPlayer;
        }
        else if (str == "empty" && isFollowingPlayer)
        {
            this.transform.SetParent(tempParentHolder.transform); // put it back to the parent gameObject
            isFollowingPlayer = !isFollowingPlayer;
        }
    }

    void OnLocked(string lockedElementName)
    {
        if (lockedElementName == this.name)
        {
            islocked = true;
        }
        else return;
    }
}
