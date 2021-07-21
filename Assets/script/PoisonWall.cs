using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonWall : MonoBehaviour
{
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.gameObject.GetComponent<PlayerController>().Setting.PlayerHP -= 1;
        }
    }
    */
}
