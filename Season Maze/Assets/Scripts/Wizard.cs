using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wizard : MonoBehaviour
{
    public static Wizard instance;
    [SerializeField]
    private SpriteRenderer m_Sprite;
    [SerializeField]
    private Sprite[] m_Sprites;
    public SmokePuff m_SmokePuff;
    private void Awake()
    {
        instance = this;
        m_Sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.onElementReturn.AddListener(zap);
        appear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void zap()
    {
        StartCoroutine(zapAnimation());
    }

    public void appear()
    {
        StartCoroutine(appearAnimation());
    }

    IEnumerator zapAnimation()
    {
        for (int i = 1; i < 4; i++)
        {
            m_Sprite.sprite = m_Sprites[i];
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 3; i > 0; i--)
        {
            m_Sprite.sprite = m_Sprites[i];
            yield return new WaitForSeconds(0.05f);
        }
        m_Sprite.sprite = m_Sprites[0];
    }

    IEnumerator appearAnimation()
    {
        AudioManager.instance.explosion(true);
        for (int i = 1; i < 39; i++)
        {
            m_SmokePuff.puff();
            yield return new WaitForSeconds(0.05f);
            if (20 == i)
            {
                m_Sprite.sprite = m_Sprites[0];//pop in wizard halfway thru
                AudioManager.instance.wizardGreeting();//start wizard dialog
                AudioManager.instance.explosion(false);
            }
        }
    }
}
