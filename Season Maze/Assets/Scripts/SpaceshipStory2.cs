using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipStory2 : MonoBehaviour
{
    private Transform myTransform;
    private Vector3 myTravel;
    private int step = 350;
    [SerializeField]
    private GameObject image, next;
    public ParticleSystem flames;
    private float myTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myTravel = new Vector3(2.9f, -0.45f, 0) - myTransform.position;
        AudioManager.instance.m_SFX_Spaceship.volume = .0625f;
        AudioManager.instance.m_SFX_Spaceship.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (myTransform.position.x < 2.9)
        {
            myTransform.position += myTravel / step;
        }
        else
        {
            if (0 == myTimer)
            {
                AudioManager.instance.spaceship(false);
                AudioManager.instance.crash();
                GetComponent<SpriteRenderer>().sprite = null;
                //Destroy(gameObject);
                //next.SetActive(true);
            }
            if (2.0f < myTimer)
                SceneManager.LoadScene("Level 1");
            myTimer += Time.deltaTime;
        }
        if (step == 450)
        {
            flames.gameObject.SetActive(true);
            flames.Play(true);
            image.SetActive(true);
            AudioManager.instance.DLG_Story2.Play();
        }
        if (step > 500)
            myTransform.localScale *= 0.993f;
        AudioManager.instance.m_SFX_Spaceship.volume *= 0.9985f;
        step += 1;
    }
}
