using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StringUnityEvent : UnityEvent<string> { }
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rb;
    public float moveSpeed = 2.5f;
    public string elementName = "empty";
    public UnityEvent onWrapGate = new UnityEvent();
    public StringUnityEvent onOperateElement = new StringUnityEvent();



    void Awake()
    {
        instance = this;
        rb = this.transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && elementName != "empty")
        {
            DropElement();
        }
    }

    void Move()
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
    }

    public void DropElement()
    {
        onOperateElement.Invoke("empty");  // drop down the Element, player holds nothing(empty)

        // clear the Element box?

        elementName = "empty";
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("WrapGate"))
        {
            onWrapGate.Invoke(); // send to levelController to show options of 4 seasons in a panel
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (elementName == "empty") // cannot pick two or more Elements at same time
        {
            if (Input.GetKeyDown(KeyCode.Space) && other.transform.CompareTag("Element"))
            {
                elementName = other.transform.name;
                onOperateElement.Invoke(elementName); // pick up the Element,  pass event to in ElementController maybe

                // add code here to show it on an Element box?
            }
        }
    }
}
