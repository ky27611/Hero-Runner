using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BoxCollider myCollider;
    public bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        this.myCollider = GetComponent<BoxCollider>();
        this.isAttack = false;
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
            other.gameObject.GetComponent<Enemy>().EnemyHP -= 1;//Setting.PlayerAtk;
        }
    }
}
