using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
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
        this.transform.position = new Vector3(0, 3, Player.transform.position.z - 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Road")
        {
            Destroy(other.gameObject);
        }
    }
}
