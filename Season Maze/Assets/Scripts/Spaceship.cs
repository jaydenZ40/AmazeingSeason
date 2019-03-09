using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    public static Spaceship instance { get; private set; }
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
        if (0 < damageLevel)
            damageLevel--;
        m_Sprite.sprite = m_Sprites[damageLevel];
    }

    public void Fly()
    {
        var vec = new Vector3(0.0f, 0.25f, 0);
        this.transform.Translate(vec);
    }

    public void Restart()
    {
        instance.transform.position = new Vector3(0.4f, 0.6f);
        damageLevel = 4;
        m_Sprite.sprite = m_Sprites[damageLevel];
    }
}
