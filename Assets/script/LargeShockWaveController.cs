using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeShockWaveController : MonoBehaviour
{
    private GameObject Player;
    public SphereCollider myCollider;
    public Rigidbody myRigidbody;
    public float velocityZ;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.myCollider = GetComponent<SphereCollider>();
        this.myRigidbody = GetComponent<Rigidbody>();
        this.velocityZ = 32;
    }

    // Update is called once per frame
    void Update()
    {
        this.myRigidbody.velocity = new Vector3(0, 0, this.velocityZ);

        if (this.transform.position.z - Player.transform.position.z >= 60)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Boss>().BossHP -= 5;
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "enemy1")
        {
            other.gameObject.GetComponent<Enemy>().EnemyHP -= 1;
            Destroy(this.gameObject);
        }

    }

}
