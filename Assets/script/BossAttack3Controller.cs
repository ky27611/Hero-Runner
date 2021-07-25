using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3Controller : MonoBehaviour
{
    private GameObject Player;
    private Transform myTransform;
    private GameObject gameDirector;
    private float Direction;
    private float Move;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.gameDirector = GameObject.Find("GameDirector");
        this.myTransform = this.transform;
        this.transform.position = new Vector3(this.transform.position.x, 1, Player.transform.position.z + 11);
        this.Direction = Random.Range(0,2);
        this.Move = ((1 - this.Direction * 2)) * 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x >= 2 || this.transform.position.x <= -2)
        {
            this.Move *= -1;
        }

        this.transform.position = new Vector3(this.transform.position.x + this.Move, this.transform.position.y, this.transform.position.z - 0.05f);

        if (Player.transform.position.z - this.transform.position.z >= 10)
        {
            Destroy(this.gameObject);
        }

        if (this.gameDirector.GetComponent<GameDirector>().index == GameDirector.Index.GameOver)
        {
            Destroy(this.gameObject);
        }
        if (this.gameDirector.GetComponent<GameDirector>().isBossDefeat)
        {
            Destroy(this.gameObject);
        }

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
