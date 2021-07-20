using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAttack : MonoBehaviour
{
    private GameObject Player;
    private GameObject root;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.root = transform.root.gameObject;
        this.Player = GameObject.Find("Player");
        this.myTransform = this.transform;
        this.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.root.transform.position.z - Player.transform.position.z <= 15)
        {
            this.GetComponent<Renderer>().enabled = true;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 1.0f);

            if (Player.transform.position.z - this.root.transform.position.z >= 10)
            {
                Destroy(this.gameObject);
            }
        }

    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.gameObject.GetComponent<PlayerController>().Setting.PlayerHP -= 1;
            Destroy(this.gameObject);
        }
    }
    */
}
