using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rb;
    public float moveSpeed = 2.5f;
    public string itemName = "";
    public UnityEvent onWrapGate = new UnityEvent();
    public UnityEvent onOperateItem = new UnityEvent();



    void Awake()
    {
        instance = this;
        rb = this.transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.transform.Translate(new Vector2(-1 * Time.deltaTime * moveSpeed, 0));
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.transform.Translate(new Vector2(1 * Time.deltaTime * moveSpeed, 0));
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.transform.Translate(new Vector2(0, 1 * Time.deltaTime * moveSpeed));
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            rb.transform.Translate(new Vector2(0, -1 * Time.deltaTime * moveSpeed));
        }
        if (Input.GetKeyDown(KeyCode.Space) && itemName != "")
        {
            onOperateItem.Invoke();  // drop down the item, pass event to ItemController maybe

            // clear the item box?

            itemName = "";
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (itemName == "") // cannot pick two or more items at same time?
        {
            if (Input.GetKeyDown(KeyCode.Space) && other.transform.CompareTag("Item"))
            {
                onOperateItem.Invoke(); // pick up the item,  pass event to in ItemController maybe

                // show it on an item box?

                itemName = other.transform.name;
            }
        }
        if (other.transform.CompareTag("WrapGate"))
        {
            onWrapGate.Invoke(); // send to levelController to show options of 4 seasons in a panel
        }
    }
}
