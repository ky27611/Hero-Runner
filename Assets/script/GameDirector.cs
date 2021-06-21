using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    public enum Index
    {
        PlayerSelect,
        GameStart,
        NormalMode,
        BossMode,
        StageClear,
        GameOver,
        PoseMenu,
        StageCreate,
        NextStage,
        CountDown,
    }

    public Index index;

    /*
    public GameIndex index;

    private Dictionary<Index, GameIndex> m_IndexMap = new Dictionary<Index, GameIndex>();

    private GameIndex m_CurrentIndex;
    */
    
    private GameObject centerText;
    private GameObject generationText;
    private GameObject heroPointText;
    private GameObject HPText;
    private GameObject Player;
    private GameObject Stage;
    private Transform Geometry;
    private GameObject bossSlime;
    private GameObject BGM;
    public bool isGameStart;
    public bool isBossBattle;
    public bool isClear;
    public bool isGameOver;
    private float waittime = 0;
    private float countdowntime = 4;
    private int countdowntimetext;
    //private bool isCountDown;
    private int generationCount = 1;
    //private static bool isHeroSelect;

    public int HeroPoint;
    public int indexNo;
    public int PlayerNo;
    public int StageNo;

    public bool isChangeIndex;

    // Start is called before the first frame update
    void Start()
    {
        /*
        m_IndexMap.Add(Index.PlayerSelect, new PlayerSelect());
        m_IndexMap.Add(Index.GameStart, new GameStart());
        m_IndexMap.Add(Index.NormalMode, new NormalMode());
        m_IndexMap.Add(Index.BossMode, new BossMode());
        m_IndexMap.Add(Index.StageClear, new StageClear());
        m_IndexMap.Add(Index.GameOver, new GameOver());
        m_IndexMap.Add(Index.PoseMenu, new PoseMenu());

        m_CurrentIndex = m_IndexMap[Index.PlayerSelect];
        */

        this.centerText = GameObject.Find("CenterText");
        this.generationText = GameObject.Find("GenerationText");
        this.heroPointText = GameObject.Find("HeroPoint");
        this.HPText = GameObject.Find("HP");
        this.Player = GameObject.Find("Player");
        this.Geometry = Player.transform.Find("Geometry");
        this.BGM = GameObject.Find("BGM");
        this.Stage = GameObject.Find("StageDirector");
        //this.bossSlime = GameObject.Find("BossSlime");

        this.HeroPoint = 0;
        this.indexNo = 0;
        this.index = Index.PlayerSelect;
        this.PlayerNo = 0;
        this.StageNo = 1;

        this.isChangeIndex = true;

        /*
        //this.isCountDown = true;
        this.isGameStart = true;
        this.isBossBattle = false;
        this.isClear = false;
        this.isGameOver = false;
        */

        this.generationText.GetComponent<Text>().text = "Generation:" + generationCount.ToString();
        this.heroPointText.GetComponent<Text>().text = "HeroPoint:" + HeroPoint.ToString();
        this.HPText.GetComponent<Text>().text = "HP:" + Player.GetComponent<PlayerController>().PlayerHP.ToString();

    }

    /*
    private void ChangeIndex(Index index)
    {
        m_CurrentIndex.Release();
        m_CurrentIndex = m_IndexMap[index];
        m_CurrentIndex.Initialize();
    }
    */

    // Update is called once per frame
    void Update()
    {
        this.heroPointText.GetComponent<Text>().text = "HeroPoint:" + HeroPoint.ToString();
        this.HPText.GetComponent<Text>().text = "HP:" + Player.GetComponent<PlayerController>().PlayerHP.ToString();

        ChangeIndex();

        //m_CurrentIndex.OnUpdate();

    }

    public void ChangeIndex()
    {
        switch (index)
        {
            case Index.PlayerSelect:     //キャラクター選択
                
                PlayerChange();
                break;

            case Index.GameStart:     //スタート処理

                GameStart();
                break;

            case Index.NormalMode:     //ラン
                this.Player.GetComponent<PlayerController>().isRunning = true;
                break;

            case Index.BossMode:     //ボス
                this.Player.GetComponent<PlayerController>().isRunning = true;
                break;

            case Index.StageClear:     //ステージクリア

                StageClear();
                break;

            case Index.GameOver:     //ゲームオーバー

                GameOver();
                break;

            case Index.NextStage:

                NextStage();
                break;

            case Index.StageCreate:

                StageCreate();
                break;
            case Index.CountDown:

                CountDown();
                break;
        }

        this.isChangeIndex = false;
    }

    public void PlayerChange()
    {
        //this.isGameStart = false;
        this.Player.GetComponent<PlayerController>().isRunning = false;
        this.centerText.GetComponent<Text>().text = "press a , b or c";
        Debug.Log("press a , b or c");
        Geometry.GetChild(PlayerNo).gameObject.SetActive(false);

        if (Input.GetKeyDown(KeyCode.A))
        {
            /*
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            */
            this.PlayerNo = 0;
            this.centerText.GetComponent<Text>().text = "";
            for (int i = 0; i < 50; i++)
            {
                Geometry.GetChild(i).gameObject.SetActive(false);
            }
            Geometry.GetChild(PlayerNo).gameObject.SetActive(true);
            this.index = Index.GameStart;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            this.PlayerNo = 1;
            this.centerText.GetComponent<Text>().text = "";
            for (int i = 0; i < 50; i++)
            {
                Geometry.GetChild(i).gameObject.SetActive(false);
            }
            Geometry.GetChild(PlayerNo).gameObject.SetActive(true);
            this.index = Index.GameStart;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            this.PlayerNo = 2;
            this.centerText.GetComponent<Text>().text = "";
            for (int i = 0; i < 50; i++)
            {
                Geometry.GetChild(i).gameObject.SetActive(false);
            }
            Geometry.GetChild(PlayerNo).gameObject.SetActive(true);
            this.index = Index.GameStart;
        }
    }

    public void GameStart()
    {
        this.waittime += Time.deltaTime;
        Player.transform.position = new Vector3(0, 0.2f, 0);
        this.centerText.GetComponent<Text>().text = "Stage" + StageNo.ToString();
        if (this.waittime >= 3)
        {
            this.waittime = 0;
            this.centerText.GetComponent<Text>().text = "";
            this.index = Index.CountDown;
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
                this.index = Index.StageCreate;
            }
        }
    }

    public void GameOver()
    {
        this.waittime += Time.deltaTime;
        //Debug.Log((int)waittime);
        Debug.Log("gameover");

        if (this.HeroPoint >= 1)
        {
            
        }
        else
        {
            this.centerText.GetComponent<Text>().text = "GameOver";
        }
        
        if (this.waittime >= 5)
        {
            //BGM.GetComponent<AudioController>().AudioChange(11);
            this.waittime = 0;
            generationCount++;
            this.generationText.GetComponent<Text>().text = "Generation:" + generationCount.ToString();
            this.centerText.GetComponent<Text>().text = "";

            if (this.HeroPoint >= 1)
            {
                this.HeroPoint = 0;
                this.index = Index.PlayerSelect;
            }
            else
            {
                SceneManager.LoadScene("TitleScene");
            }

        }
    }

    public void StageClear()
    {
        this.waittime += Time.deltaTime;
        Debug.Log((int)waittime);
        Debug.Log("clear");
        this.centerText.GetComponent<Text>().text = "Clear";
        //BGM.GetComponent<AudioController>().AudioChange(12);

        if (this.waittime >= 3)
        {
            //this.Stage.GetComponent<StageController>().isNextStage = true;
            this.centerText.GetComponent<Text>().text = "";
            this.index = Index.NextStage;
            this.waittime = 0;
            this.StageNo++;
        }
    }

    public void NextStage()
    {
        this.waittime += Time.deltaTime;
        
        this.centerText.GetComponent<Text>().text = "Stage" + StageNo.ToString();

        if (this.waittime >= 3)
        {
            this.waittime = 0;
            this.centerText.GetComponent<Text>().text = "";
            this.index = Index.StageCreate;
        }
    }

    public void StageCreate()
    {
        this.Stage.GetComponent<StageController>().isStageCreate = true;
    }

}
