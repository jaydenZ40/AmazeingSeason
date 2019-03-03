using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokePuff : MonoBehaviour
{
    private int puffStep = -1;
    [SerializeField]
    private Sprite[] mySprites;
    private SpriteRenderer mySprite;
    private void Awake()
    {
        mySprite = this.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void puff(bool reset = false)
    {
        if (reset)
            puffStep = -1;
        puffStep++;
        if (37 > puffStep)
            mySprite.sprite = mySprites[puffStep];
        else
            mySprite.sprite = null;
    }
}
