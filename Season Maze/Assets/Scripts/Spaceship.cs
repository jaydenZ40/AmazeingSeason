using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public static Spaceship instance { get; private set; }
    public GameObject mainCamera;
    private int damageLevel = 4;
    [SerializeField]
    private SpriteRenderer m_Sprite;
    [SerializeField]
    private Sprite[] m_Sprites;
    void Awake()
    {
        if (null == instance)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        m_Sprite = this.gameObject.GetComponent<SpriteRenderer>();
        PlayerController.instance.onElementReturn.AddListener(Repair);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Repair()
    {
        mainCamera.transform.parent = null;
        mainCamera.transform.position = new Vector3(0, 0, -10);
        Invoke("DelayRepair", 0.5f);
        Invoke("CameraBack", 1f);
    }

    void DelayRepair()
    {
        if (0 < damageLevel)
            damageLevel--;
        m_Sprite.sprite = m_Sprites[damageLevel];
    }
    void CameraBack()
    {
        GameObject player = GameObject.Find("Player");
        mainCamera.transform.position = player.transform.position - new Vector3(0, 0, 10);
        mainCamera.transform.parent = player.transform;
    }

    public void Fly()
    {
        var vec = new Vector3(0.0f, 0.25f, 0);
        this.transform.Translate(vec);
    }

    public void Restart()
    {
        instance.gameObject.SetActive(true);
        instance.transform.position = new Vector3(0.0f, 0.25f);
        damageLevel = 4;
        m_Sprite.sprite = m_Sprites[damageLevel];
    }
}
