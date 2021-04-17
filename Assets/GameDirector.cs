using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject centerText;
    public bool isGameStart;
    public bool isClear;
    public bool isGameOver;
    private float waittime = 0;
    private float countdowntime = 4;
    private int countdowntimetext;
    private bool isCountDown;
    //private bool ischangeText = true;

    // Start is called before the first frame update
    void Start()
    {
        this.centerText = GameObject.Find("CenterText");
        this.isGameStart = true;
        this.isClear = false;
        this.isGameOver = false;
        this.isCountDown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isGameStart == true)
        {
            GameStart();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Reload();
        }

        if (this.isGameOver == true)
        {
            GameOver();
        }

        if (this.isClear == true)
        {
            Clear();
        }
    }

    public void GameStart()
    {
        if (isGameStart == true)
        {
            CountDown();
        }
    }
    
    public void Reload()
    {
        SceneManager.LoadScene("Stage0Scene");
        Debug.Log("stage0");
    }

    public void GameOver()
    {
        this.waittime += Time.deltaTime;
        Debug.Log((int)waittime);
        Debug.Log("gameover");
        this.centerText.GetComponent<Text>().text = "GameOver";

        if (this.waittime >= 5)
        {
            SceneManager.LoadScene("Stage0Scene");
            Debug.Log("stage0");
            this.waittime = 0;
        }
    }

    public void Clear()
    {
        this.waittime += Time.deltaTime;
        Debug.Log((int)waittime);
        Debug.Log("clear");
        this.centerText.GetComponent<Text>().text = "Clear";

        if (this.waittime >= 5)
        {
            SceneManager.LoadScene("Stage1Scene");
            Debug.Log("stage1");
            this.waittime = 0;
        }
    }

    public void CountDown()
    {
        this.countdowntimetext = (int)this.countdowntime;

        if (countdowntime <= 4 && countdowntime >= 3)
        {
            this.centerText.GetComponent<Text>().text = this.countdowntimetext.ToString();
            this.countdowntime -= Time.deltaTime;
        }
        else if (countdowntime <= 3 && countdowntime >= 2)
        {

            this.centerText.GetComponent<Text>().text = this.countdowntimetext.ToString();
            this.countdowntime -= Time.deltaTime;

        }
        else if (countdowntime <= 2 && countdowntime >= 1)
        {
            this.centerText.GetComponent<Text>().text = this.countdowntimetext.ToString();
            this.countdowntime -= Time.deltaTime;

        }
        else if (countdowntime <= 1)//&& countdowntime >= 0)
        {
            this.centerText.GetComponent<Text>().text = "Start";
            this.waittime += Time.deltaTime;
            if (this.waittime >= 1)
            {
                this.waittime = 0;
                this.centerText.GetComponent<Text>().text = "";
                this.countdowntime = 4;
                isGameStart = false;
            }
        }
    }
}
