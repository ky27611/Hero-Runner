using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BoxCollider myCollider;
    public bool isAttack;
    public GameObject Player;
    public AudioSource myAudio;
    public AudioClip DefeatSE;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.myCollider = GetComponent<BoxCollider>();
        this.isAttack = false;
        this.myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack == true)
        {
            this.myCollider.enabled = true;
        }
        else
        {
            this.myCollider.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy1")
        {
            //this.myAudio.PlayOneShot(DefeatSE);
            other.gameObject.GetComponent<Enemy>().EnemyHP -= this.Player.gameObject.GetComponent<PlayerController>().Setting.PlayerAtk;
        }
    }
}
