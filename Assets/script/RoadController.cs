using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
        this.Player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.z - this.transform.position.z >= 200)
        {
            Destroy(this.gameObject);
        }
    }
}
