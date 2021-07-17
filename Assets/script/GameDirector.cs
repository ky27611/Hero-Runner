﻿using System.Collections;
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
    public Index preindex;

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
    private GameObject PlayerDummy;
    private GameObject Stage;
    private GameObject Sword;
    private GameObject SwordDummy;
    private Transform Geometry;
    private Transform GeometryDummy;
    private GameObject bossSlime;
    private GameObject BGM;
    public bool isHeroDecision;
    public bool isGameStart;
    public bool isBossMode;
    public bool isStageClear;
    public bool isGameOver;
    public bool isNormalMode;
    private float waittime = 0;
    private int waitcount = 0;
    private float countdowntime = 4;
    private int countdowntimetext;
    //private bool isCountDown;
    private int generationCount = 1;
    //private static bool isHeroSelect;

    public float HeroPoint;
    public float HeroPointMAX;
    public float HeroPointRatio;
    public int indexNo;
    public int PlayerNo;
    public int StageNo;
    //public int ProcessNo;

    public float RestartPos;

    public bool isChangeIndex;

    //public LifePanelController lifePanel;

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
        this.PlayerDummy = GameObject.Find("PlayerDummy");
        this.Geometry = Player.transform.Find("Geometry");
        this.GeometryDummy = PlayerDummy.transform.Find("GeometryDummy");
        this.Sword = GameObject.Find("Sword");
        this.SwordDummy = GameObject.Find("SwordDummy");
        this.BGM = GameObject.Find("BGM");
        this.Stage = GameObject.Find("StageDirector");
        this.BGM.GetComponent<AudioController>();

        this.HeroPoint = 0;
        this.HeroPointMAX = 2;
        this.indexNo = 0;
        this.index = Index.PlayerSelect;
        this.PlayerNo = 0;
        this.StageNo = 1;
        //this.ProcessNo = 1;

        this.RestartPos = 0;

        this.isChangeIndex = true;
        this.isHeroDecision = false;
        this.isChangeIndex = true;

        /*
        //this.isCountDown = true;
        this.isGameStart = true;
        this.isBossBattle = false;
        this.isClear = false;
        this.isGameOver = false;
        */

        this.generationText.GetComponent<Text>().text = "Generation:" + generationCount.ToString();
        //this.heroPointText.GetComponent<Text>().text = "HeroPoint:" + HeroPoint.ToString();
        //this.HPText.GetComponent<Text>().text = "HP:" + Player.GetComponent<PlayerController>().PlayerHP.ToString();
        
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
        
        this.HeroPointRatio = this.HeroPoint / this.HeroPointMAX;

        if (isNormalMode)
        {
            this.isNormalMode = false;
            this.index = Index.NormalMode;
            this.isChangeIndex = true;
        }
        if (isBossMode)
        {
            this.isBossMode = false;
            this.index = Index.BossMode;
            this.isChangeIndex = true;
        }
        if (isStageClear)
        {
            this.isStageClear = false;
            this.index = Index.StageClear;
            this.isChangeIndex = true;
        }
        if (isGameOver)
        {
            this.isGameOver = false;
            this.index = Index.GameOver;
            this.isChangeIndex = true;
        }

        if (isChangeIndex)
        {
            ChangeIndex();
        }

        SwitchIndex(index, 2);

        //this.heroPointText.GetComponent<Text>().text = "HeroPoint:" + HeroPoint.ToString();
        //this.HPText.GetComponent<Text>().text = "HP:" + Player.GetComponent<PlayerController>().PlayerHP.ToString();
        //lifePanel.UpdateLife((int)Player.GetComponent<PlayerController>().Setting.PlayerHP);
        //m_CurrentIndex.OnUpdate();

    }

    public void ChangeIndex()
    {
        this.isChangeIndex = false;
        SwitchIndex(preindex, 3);
        SwitchIndex(index, 1);
    }

    public void SwitchIndex(Index selectindex, int ProcessNo)
    {
        switch (selectindex)
        {
            case Index.PlayerSelect:     //キャラクター選択
                PlayerSelect(ProcessNo);
                break;

            case Index.GameStart:     //スタート処理
                GameStart(ProcessNo);
                break;

            case Index.NormalMode:     //ラン
                NormalMode(ProcessNo);
                break;

            case Index.BossMode:     //ボス
                BossMode(ProcessNo);
                break;

            case Index.StageClear:     //ステージクリア
                StageClear(ProcessNo);
                break;

            case Index.GameOver:     //ゲームオーバー
                GameOver(ProcessNo);
                break;

            case Index.NextStage:
                NextStage(ProcessNo);
                break;

            case Index.StageCreate:
                StageCreate(ProcessNo);
                break;
            case Index.CountDown:
                CountDown(ProcessNo);
                break;
        }
    }

    public void PlayerSelect(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.Player.GetComponent<PlayerController>().isRunning = false;
                PlayerDummy.gameObject.SetActive(true);
                Geometry.GetChild(PlayerNo).gameObject.SetActive(false);
                this.Sword.GetComponent<Renderer>().enabled = false;
                this.SwordDummy.GetComponent<Renderer>().enabled = false;
                Player.transform.position = new Vector3(0, 0.1f, RestartPos);
                this.centerText.GetComponent<Text>().text = "press space";
                break;
            case 2:
                this.waittime += Time.deltaTime;
                
                if (isHeroDecision == false)
                {
                    if (this.waittime >= 0.08f)
                    {
                        waittime = 0;
                        GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(false);
                        this.PlayerNo++;
                        if (PlayerNo >= 6)
                        {
                            PlayerNo = 0;
                        }
                        GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        this.isHeroDecision = true;
                        this.waittime = 0;
                    }
                }
                else
                {
                    this.centerText.GetComponent<Text>().text = "";
                    this.SwordDummy.GetComponent<Renderer>().enabled = true;

                    if (this.waittime >= 3)
                    {
                        this.preindex = this.index;
                        this.index = Index.GameStart;
                        this.isChangeIndex = true;
                    }
                }
                break;
            case 3:
                this.waittime = 0;
                this.isHeroDecision = false;
                GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(false);
                PlayerDummy.gameObject.SetActive(false);
                Geometry.GetChild(PlayerNo).gameObject.SetActive(true);
                this.Sword.GetComponent<Renderer>().enabled = true;
                GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void GameStart(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.Player.GetComponent<PlayerController>().isRunning = false;
                Player.transform.position = new Vector3(0, 0.1f, RestartPos);
                this.Player.GetComponent<PlayerController>().Setting.velocityZ = 16;
                this.Player.GetComponent<PlayerController>().Setting.velocityX = 12;
                this.Player.GetComponent<PlayerController>().Setting.velocityY = 4;
                this.centerText.GetComponent<Text>().text = "Stage" + StageNo.ToString();
                break;
            case 2:
                this.waittime += Time.deltaTime;
                if (this.waittime >= 3)
                {
                    this.preindex = this.index;
                    this.index = Index.CountDown;
                    this.isChangeIndex = true;
                }
                break;
            case 3:
                this.waittime = 0;
                this.centerText.GetComponent<Text>().text = "";
                break;
            default:
                break;
        }        
    }

    public void NormalMode(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.Player.GetComponent<PlayerController>().isRunning = true;

                if (this.StageNo % 10 <= 3)
                {
                    BGM.GetComponent<AudioController>().AudioChange(1);
                    //this.BGM.GetComponent<AudioController>().BGMNo = 1;
                }
                else if (this.StageNo % 10 >= 4 && this.StageNo % 10 <= 6)
                {
                    BGM.GetComponent<AudioController>().AudioChange(2);
                    //this.BGM.GetComponent<AudioController>().BGMNo = 2;
                }
                else if (this.StageNo % 10 >= 7 && this.StageNo % 10 <= 9)
                {
                    BGM.GetComponent<AudioController>().AudioChange(3);
                    //.BGM.GetComponent<AudioController>().BGMNo = 3;
                }

                break;
            case 2:

                break;
            case 3:

                break;
            default:
                break;
        }
    }

    public void BossMode(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                if (this.StageNo % 10 == 0)
                {
                    BGM.GetComponent<AudioController>().AudioChange(10);
                }
                else if (this.StageNo % 3 == 0)
                {
                    BGM.GetComponent<AudioController>().AudioChange(9);
                }
                break;
            case 2:

                break;
            case 3:

                break;
            default:
                break;
        }
    }

    public void StageClear(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.centerText.GetComponent<Text>().text = "Clear";
                BGM.GetComponent<AudioController>().AudioChange(12);
                break;
            case 2:
                this.waittime += Time.deltaTime;
                if (this.waittime >= 3)
                {
                    this.preindex = this.index;
                    this.index = Index.NextStage;
                    this.isChangeIndex = true;
                }
                break;
            case 3:
                //this.Stage.GetComponent<StageController>().isNextStage = true;
                this.centerText.GetComponent<Text>().text = "";
                this.waittime = 0;
                this.StageNo++;
                break;
            default:
                break;
        }
    }

    public void GameOver(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.Player.GetComponent<PlayerController>().isRunning = false;
                this.Sword.GetComponent<Renderer>().enabled = false;
                this.Stage.GetComponent<StageController>().isCreate = false;
                BGM.GetComponent<AudioController>().AudioChange(11);
                break;
            case 2:
                this.waittime += Time.deltaTime;
                if (this.HeroPoint >= 1)
                {

                }
                else
                {
                    this.centerText.GetComponent<Text>().text = "GameOver";
                }

                if (this.waittime >= 5)
                {
                    this.waittime = 0;
                    generationCount++;
                    this.generationText.GetComponent<Text>().text = "Generation:" + generationCount.ToString();
                    this.centerText.GetComponent<Text>().text = "";

                    if (this.HeroPoint >= 1)
                    {
                        this.preindex = this.index;
                        this.index = Index.PlayerSelect;
                        this.isChangeIndex = true;
                    }
                    else
                    {
                        SceneManager.LoadScene("TitleScene");
                    }

                }
                break;
            case 3:
                this.HeroPoint = 0;
                this.RestartPos = Player.transform.position.z;
                this.Player.GetComponent<PlayerController>().ChangeState(PlayerController.StateType.Idle);
                break;
            default:
                break;
        }
    }

    public void PoseMenu(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                
                break;
            case 2:

                break;
            case 3:

                break;
            default:
                break;
        }
    }

    public void StageCreate(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.Stage.GetComponent<StageController>().isStageCreate = true;
                break;
            case 2:
               
                break;
            case 3:
                
                break;
            default:
                break;
        }
    }

    public void NextStage(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:
                this.centerText.GetComponent<Text>().text = "Stage" + StageNo.ToString();
                break;
            case 2:
                this.waittime += Time.deltaTime;
                if (this.waittime >= 3)
                {
                    this.preindex = this.index;
                    this.index = Index.StageCreate;
                    this.isChangeIndex = true;
                }
                break;
            case 3:
                this.waittime = 0;
                this.centerText.GetComponent<Text>().text = "";
                break;
            default:
                break;
        }
    }

    public void CountDown(int ProcessNo)
    {
        switch (ProcessNo)
        {
            case 1:

                break;
            case 2:
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
                        this.preindex = this.index;
                        this.index = Index.StageCreate;
                        this.isChangeIndex = true;
                    }
                }
                break;
            case 3:
                this.waittime = 0;
                this.centerText.GetComponent<Text>().text = "";
                this.countdowntime = 4;
                break;
            default:
                break;
        }
    }
}
