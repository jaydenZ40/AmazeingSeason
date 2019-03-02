using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    // Start is called before the first frame update
    private int damageLevel = 4;
    [SerializeField]
    private SpriteRenderer m_Sprite;
    [SerializeField]
    private Sprite[] m_Sprites;
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
}
