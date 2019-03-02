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
    public UnityEvent onKeyPickup = new UnityEvent();
    public UnityEvent onElementPickup = new UnityEvent();
    public UnityEvent onElementReturn = new UnityEvent();
    public StringUnityEvent OperateElement = new StringUnityEvent();
    public GameObject elementHolder, keyHolder, lockHolder, keyParent; //keyParent: an empty gameobject to hold four keys

    private GameObject curElementBox;
    private bool[] haveKeys = new bool[4] { false, false, false, false };

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
        OperateElement.Invoke("empty");  // drop down the Element, player holds nothing(empty)
        HideIcon();
        elementName = "empty";
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Lock"))
        {
            int num = TranslateLetter(other.gameObject.name[1]);
            if (haveKeys[num])
            {
                haveKeys[num] = false;
                keyHolder.transform.GetChild(num).gameObject.SetActive(false);  // should I remove used key?
                lockHolder.transform.GetChild(num).gameObject.SetActive(false); // remove lock after opening
                AudioManager.instance.unlockDoor();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("WarpGate"))
        {
            onWrapGate.Invoke(); // send to levelController to show options of 4 seasons in a panel
        }

        if (other.transform.CompareTag("Key"))
        {
            int num = TranslateLetter(other.name[1]);
            haveKeys[num] = true;
            keyHolder.transform.GetChild(num).gameObject.SetActive(true);   // show icon
            keyParent.transform.GetChild(num).gameObject.SetActive(false);  // hide gameobject
            onKeyPickup.Invoke();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (elementName == "empty") // cannot pick two or more Elements at same time
        {
            if (Input.GetKeyDown(KeyCode.Space) && other.transform.CompareTag("Element"))
            {
                elementName = other.transform.name;
                OperateElement.Invoke(elementName); // pick up the Element, pass event to in ElementController
                ShowIcon();
                onElementPickup.Invoke();
            }
        }
    }

    void ShowIcon()
    {
        for (int i = 0; i < 4; i++)
        {
            if (elementHolder.transform.GetChild(i).name[1] == elementName[1])
            {
                curElementBox = elementHolder.transform.GetChild(i).gameObject;
                elementHolder.transform.GetChild(i).gameObject.SetActive(true);
                break;
            }
        }
    }

    public void HideIcon(bool dropping = false)
    {
        curElementBox.SetActive(false);
        if (dropping)
            onElementReturn.Invoke();
    }

    public int TranslateLetter(char c)
    {
        switch (c)
        {
            case 'p':
                return 0;
            case 'u':
                return 1;
            case 'a':
                return 2;
            case 'i':
                return 3;
            default:
                return -1;
        }
    }
}
