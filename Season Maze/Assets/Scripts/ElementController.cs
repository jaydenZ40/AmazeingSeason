using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    private bool isFollowingPlayer = false;
    private GameObject tempParentHolder;

    void Start()
    {
        PlayerController.instance.onOperateElement.AddListener(OnOperateElement);
    }
    void OnOperateElement(string str)
    {
        if (this.name == str) // avoid operating all listeners by matching object's name
        {
            tempParentHolder = this.transform.parent.gameObject;
            this.transform.position = PlayerController.instance.rb.transform.position + new Vector3(0, 0.5f, 0);  // hold the Element above the player?
            this.transform.SetParent(PlayerController.instance.transform);
            isFollowingPlayer = !isFollowingPlayer;
        }
        else if (str == "empty" && isFollowingPlayer)
        {
            this.transform.SetParent(tempParentHolder.transform);
            isFollowingPlayer = !isFollowingPlayer;
        }
    }
}
