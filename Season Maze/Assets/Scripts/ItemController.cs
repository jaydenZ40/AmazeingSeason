using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private bool isFollowingPlayer = false;


    void Awake()
    {
        PlayerController.instance.onOperateItem.AddListener(OperateItem);
    }
    void OperateItem()
    {
        if (!isFollowingPlayer)
        {
            this.transform.position = PlayerController.instance.rb.transform.position + new Vector3(0, 0.5f, 0);
            this.transform.SetParent(PlayerController.instance.transform);
            PlayerController.instance.itemName = this.name;
            // hold the item above the player?
        }
        else
        {
            this.transform.parent = null;
        }
        isFollowingPlayer = !isFollowingPlayer;
    }
}
