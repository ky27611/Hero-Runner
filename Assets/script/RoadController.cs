using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    public GameObject Player;
    public GameObject director;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.director.GetComponent<GameDirector>().index != GameDirector.Index.NextStage)
        {
            if (this.director.GetComponent<GameDirector>().index != GameDirector.Index.StageClear)
            {
                if (Player.transform.position.z - this.transform.position.z >= 200)
                {
                    Destroy(this.gameObject);
                }

                if (Player.transform.position.z - this.transform.position.z <= -210)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
