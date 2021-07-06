using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDummyController : MonoBehaviour
{
    private GameObject gamedirector;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        this.gamedirector = GameObject.Find("GameDirector");
        this.myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gamedirector.GetComponent<GameDirector>().isHeroDecision == false)
        {
            this.myAnimator.SetFloat("DrawSword", 0);
        }
        else
        {
            this.myAnimator.SetFloat("DrawSword", 1);
        }
    }
}
