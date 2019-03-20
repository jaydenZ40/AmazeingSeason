using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipStory : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 myTravel;
    private int step = 500;
    [SerializeField]
    private GameObject image, next; 
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTravel = new Vector3(3,-1.5f,0) - myTransform.position;
        AudioManager.instance.m_SFX_Spaceship.volume = .0625f;
    }

    // Update is called once per frame
    void Update()
    {
        if (myTransform.position.x < 4.35f)
        {
            myTransform.position += myTravel / step;
        }
        myTransform.localScale *= 0.99875f;
        AudioManager.instance.m_SFX_Spaceship.volume *= 0.9985f;
        step += 1;
        if (step == 1500)
        {
            image.SetActive(true);
            //next.SetActive(true);
        }
        if (myTransform.position.x > 3.5f)
            SceneManager.LoadScene("StoryScene2");
    }
}
