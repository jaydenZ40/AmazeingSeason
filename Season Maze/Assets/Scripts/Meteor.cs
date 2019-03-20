using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Sprite[] sprites;
    private SpriteRenderer m_SpriteRender;
    private int step = 1;
    private Vector3 movement;
    private int currentSprite = 0;
    void Start()
    {
        movement = new Vector3(-0.11f, -0.11f, 0);
        m_SpriteRender = GetComponent<SpriteRenderer>();
        m_SpriteRender.sprite = sprites[currentSprite];
        transform.Rotate(new Vector3(0,0,-135));
        transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
        m_SpriteRender.sortingOrder = 1;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += movement;
        if (step % 25 == 0)
        {
            if(4 > currentSprite)
                currentSprite++;
            m_SpriteRender.sprite = sprites[currentSprite];
        }
        var pos = transform.position;
        if (-9 > pos.x || -7 > pos.y)
        {
//            Debug.Log("call destroy");
            Destroy(this.gameObject);
        }
        step++;
    }
}
