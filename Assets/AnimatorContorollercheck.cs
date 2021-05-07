using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorContorollercheck : MonoBehaviour
{
    private Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            myAnimator.SetFloat("Speed", 0);
            myAnimator.SetBool("Death",false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            myAnimator.SetFloat("Speed", 1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            myAnimator.SetTrigger("JumpTrigger");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            myAnimator.SetTrigger("SlidingTrigger");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            myAnimator.SetTrigger("AttackTrigger");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            myAnimator.SetBool("Death", true);
        }
    }
}
