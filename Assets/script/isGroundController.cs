using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isGroundController : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Road")
        {
            Player.GetComponent<PlayerController>().Setting.isGround = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Road")
        {
            Player.GetComponent<PlayerController>().Setting.isGround = false;
        }
    }
}
