using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private GameObject Player;
    private GameObject gameDirector;
    public BoxCollider myCollider;
    public Rigidbody myRigidbody;
    public float velocityZ;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.gameDirector = GameObject.Find("GameDirector");
        this.myCollider = GetComponent<BoxCollider>();
        this.myRigidbody = GetComponent<Rigidbody>();
        this.velocityZ = 32;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameDirector.GetComponent<GameDirector>().index == GameDirector.Index.BossMode)
        {
            this.myRigidbody.velocity = new Vector3(0, 0, this.velocityZ);

            if (this.transform.position.z - Player.transform.position.z >= 60)
            {
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().BossHP -= 3;
            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "enemy1")
        {
            other.gameObject.GetComponent<Enemy>().EnemyHP -= 1;
        }
    }
}
