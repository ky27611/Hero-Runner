using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    private GameObject centerText;
    private GameObject generationText;
    private GameObject Player;
    private Transform Geometry;
    private GameObject bossSlime;
    public bool isGameStart;
    public bool isBossBattle;
    public bool isClear;
    public bool isGameOver;
    private float waittime = 0;
    private float countdowntime = 4;
    private int countdowntimetext;
    //private bool isCountDown;
    private static int generationCount = 0;
    private static bool isHeroSelect;

    // Start is called before the first frame update
    void Start()
    {
        this.centerText = GameObject.Find("CenterText");
        this.generationText = GameObject.Find("GenerationText");
        this.Player = GameObject.Find("Player");
        this.Geometry = Player.transform.Find("Geometry");
        this.bossSlime = GameObject.Find("BossSlime");
        this.isGameStart = true;
        this.isBossBattle = false;
        this.isClear = false;
        this.isGameOver = false;
        //this.isCountDown = true;

        this.generationText.GetComponent<Text>().text = "Generation:" + generationCount.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (isHeroSelect == true)
        {
            this.isGameStart = false;
            Debug.Log("press a , b or c");

            if (Input.GetKeyDown(KeyCode.A))
            {
                /*
                foreach(Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                }
                */
                for (int i = 0; i < 50; i++)
                {
                    Geometry.GetChild(i).gameObject.SetActive(false);
                }
                Geometry.GetChild(0).gameObject.SetActive(true);
                this.isGameStart = true;
                isHeroSelect = false;
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                for (int i = 0; i < 50; i++)
                {
                    Geometry.GetChild(i).gameObject.SetActive(false);
                }
                Geometry.GetChild(1).gameObject.SetActive(true);
                this.isGameStart = true;
                isHeroSelect = false;
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                for (int i = 0; i < 50; i++)
                {
                    Geometry.GetChild(i).gameObject.SetActive(false);
                }
                Geometry.GetChild(2).gameObject.SetActive(true);
                this.isGameStart = true;
                isHeroSelect = false;
            }
        }
        else if (this.isGameStart == true)
        {
            GameStart();
        }
        else if (this.isGameOver == true)
        {
            GameOver();
        }
        else if (this.isClear == true)
        {
            Clear();
        }

        if (this.isBossBattle == false && Player.transform.position.z - bossSlime.transform.position.z >= -5)
        {
            this.isBossBattle = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Reload();
        } 
    }

    public void GameStart()
    {
        if (isGameStart == true)
        {
            Player.transform.position = new Vector3(0, 0.2f, -495);
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
            this.waittime = 0;
            isHeroSelect = true;
            generationCount++;
            this.generationText.GetComponent<Text>().text = "Generation:" + generationCount.ToString();
            //SceneManager.LoadScene("Stage0Scene");
            //Debug.Log("stage0");
            this.centerText.GetComponent<Text>().text = "";
            this.isGameOver = false;

            /*
            this.generationCount++;
            this.generationText.GetComponent<Text>().text = "Generation:" + this.generationCount.ToString();
            this.waittime = 0;
            this.isGameOver = false;
            this.isGameStart = true;
            */
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
            this.centerText.GetComponent<Text>().text = "Start!";
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
