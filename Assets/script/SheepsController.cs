using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepsController : MonoBehaviour
{
    private GameObject Player;
    private GameObject gameDirector;
    //public float Duration;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.gameDirector = GameObject.Find("GameDirector");
        //this.Duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameDirector.GetComponent<GameDirector>().index != GameDirector.Index.BossMode)
        {
            //this.Duration += Time.deltaTime;
            if (this.Player.GetComponent<PlayerController>().Setting.isSkillActivation == false)
            {
                Destroy(this.gameObject);
            }

            this.transform.position = new Vector3(this.Player.transform.position.x, this.transform.position.y, this.Player.transform.position.z);
        }
        if (this.gameDirector.GetComponent<GameDirector>().index == GameDirector.Index.GameOver)
        {
            Destroy(this.gameObject);
        }
    }
}
