using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabbedSwordController : MonoBehaviour
{
    private Transform myTransform;
    private GameObject Director;
    private GameObject StabbedSword;
    private GameObject Player;
    private float waittime;

    // Start is called before the first frame update
    void Start()
    {
        this.Director = GameObject.Find("GameDirector");
        this.StabbedSword = GameObject.Find("StabbedSword");
        this.Player = GameObject.Find("Player");
        this.transform.position = new Vector3(Player.transform.position.x, 5, Player.transform.position.z + 5);
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
        this.StabbedSword.GetComponent<Renderer>().enabled = false;
        this.myTransform = this.transform;
        //this.transform.Rotate(0, 0, 0);
        this.waittime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Director.GetComponent<GameDirector>().index == GameDirector.Index.GameOver)
        {
            this.waittime += Time.deltaTime;

            if (this.waittime >= 2)
            {
                this.StabbedSword.GetComponent<Renderer>().enabled = true;
                if (this.transform.position.y >= 0.9)
                {
                    this.transform.Rotate(0, 0, 9);

                    Vector3 pos = myTransform.position;

                    pos.y -= 0.07f;

                    myTransform.position = pos;
                }
                
            }
            
        }
        else
        {
            this.waittime = 0;
            this.StabbedSword.GetComponent<Renderer>().enabled = false;
            this.transform.position = new Vector3(Player.transform.position.x, 5, Player.transform.position.z + 5);
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
}
