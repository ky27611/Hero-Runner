using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanelController : MonoBehaviour
{
    public GameObject[] icons;
    private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (i < Player.GetComponent<PlayerController>().Setting.PlayerHP)
            {
                icons[i].SetActive(true);
            }
            else
            {
                icons[i].SetActive(false);
            }
        }
    }

    /*
    public void UpdateLife(int life)
    {
        for (int i = 0; i < icons.Length; i++)
        {
            if (i < life)
            {
                icons[i].SetActive(true);
            }
            else
            {
                icons[i].SetActive(false);
            }
        }
    }
    */

}
