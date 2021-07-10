using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack2Controller : MonoBehaviour
{

    private GameObject Player;
    private Transform myTransform;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.myTransform = this.transform;
        this.transform.position = new Vector3(this.transform.position.x, 1, Player.transform.position.z + 11);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPos = myTransform.localPosition;
        localPos.z -= 0.1f;
        myTransform.localPosition = localPos;

        if (Player.transform.position.z - this.transform.position.z >= 10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.gameObject.GetComponent<PlayerController>().Setting.PlayerHP -= 1;
        }
    }
}
