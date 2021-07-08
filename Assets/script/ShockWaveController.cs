using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoBehaviour
{
    private GameObject Player;
    public SphereCollider myCollider;
    public Rigidbody myRigidbody;
    public float velocityZ;
    //public bool isShockWave;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.myCollider = GetComponent<SphereCollider>();
        this.myRigidbody = GetComponent<Rigidbody>();
        this.velocityZ = 32;
        //this.isShockWave = false;
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
            other.gameObject.GetComponent<Boss>().BossHP -= 1;
            Destroy(this.gameObject);
        }
    }

}
