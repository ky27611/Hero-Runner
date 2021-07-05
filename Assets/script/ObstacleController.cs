using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    private GameObject Gamedirector;

    // Start is called before the first frame update
    void Start()
    {
        this.Gamedirector = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.Gamedirector.GetComponent<GameDirector>().index == GameDirector.Index.PlayerSelect)
        {
            Destroy(this.gameObject);
        }
    }
}
