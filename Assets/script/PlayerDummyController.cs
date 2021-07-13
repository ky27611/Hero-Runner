using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummyController : MonoBehaviour
{
    private GameObject gamedirector;
    private Animator myAnimator;
    private AudioSource myAudio;
    public AudioClip SwordSE;
    private GameObject TimeDirector;
    private bool isSE;
    private float waittime;

    // Start is called before the first frame update
    void Start()
    {
        this.gamedirector = GameObject.Find("GameDirector");
        this.TimeDirector = GameObject.Find("TimeDirector");
        this.myAnimator = GetComponent<Animator>();
        this.myAudio = GetComponent<AudioSource>();
        this.isSE = true;
        this.waittime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gamedirector.GetComponent<GameDirector>().isHeroDecision == false)
        {
            this.myAnimator.SetFloat("DrawSword", 0);
            this.isSE = true;
        }
        else
        {
            this.myAnimator.SetFloat("DrawSword", 1);
            if (this.isSE == true)
            {
                if (this.waittime == 0)
                {
                    this.waittime = this.TimeDirector.GetComponent<TimeDirector>().time;
                }
                
                if (this.TimeDirector.GetComponent<TimeDirector>().time - this.waittime >= 0.5)
                {
                    this.myAudio.PlayOneShot(SwordSE);
                    this.waittime = 0;
                    this.isSE = false;
                }
            }         
        }
    }
}
