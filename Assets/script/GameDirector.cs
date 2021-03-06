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
    public Index preindex;

    /*
    public GameIndex index;

    private Dictionary<Index, GameIndex> m_IndexMap = new Dictionary<Index, GameIndex>();

    private GameIndex m_CurrentIndex;
    */
    
    private GameObject centerText;
    private GameObject centerBack;
    private GameObject underText;
    private GameObject underBack;
    private GameObject generationText;
    private GameObject heroPointText;
    private GameObject HPText;
    private GameObject Player;
    private GameObject PlayerDummy;
    private GameObject Stage;
    private GameObject Sword;
    private GameObject SwordDummy;
    private GameObject scoreDirector;
    private GameObject FadePanel;
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
    public bool isPlayerOrigin;
    public bool isDebug;
    public bool isDeathatStage;
    public bool isFadeIn;
    public bool isFadeOut;
    public bool isBossDefeat;
    public bool isHighScore;
    private float waittime = 0;
    private int waitcount = 0;
    private float countdowntime = 4;
    private int countdowntimetext;
    //private bool isCountDown;
    private int generationCount = 1;
    //private static bool isHeroSelect;

    public static int highscore = 0;
    public float HeroPoint;
    public float HeroPointMAX;
    public float HeroPointRatio;
    public int indexNo;
    public int PlayerNo;
    public int StageNo;
    public int ModeNo;
    //public int ProcessNo;

    public float RestartPos;
    public float alfa;

    public bool isChangeIndex;

    public AudioClip[] Seclips;
    AudioSource Seaudios;
    public int SENo;
    private bool isSeTiming; 

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
        this.centerBack = GameObject.Find("CenterBack");
        this.underText = GameObject.Find("UnderText");
        this.underBack = GameObject.Find("UnderBack");
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
        this.scoreDirector = GameObject.Find("ScoreDirector");
        this.FadePanel = GameObject.Find("FadePanel");
        this.BGM.GetComponent<AudioController>();

        Seaudios = GetComponent<AudioSource>();
        this.SENo = 0;

        this.centerBack.GetComponent<Image>().enabled = false;
        this.underBack.GetComponent<Image>().enabled = false;

        this.HeroPoint = 0;
        this.HeroPointMAX = 2;
        this.indexNo = 0;
        
        this.PlayerNo = 0;
        this.StageNo = 1;
        //this.ProcessNo = 1;

        this.RestartPos = 0;

        this.alfa = 1f;

        this.isPlayerOrigin = false;
        this.isHeroDecision = false;
        this.isSeTiming = false;
        this.isDebug = false;
        this.isDeathatStage = false;
        this.isFadeIn = false;
        this.isFadeOut = false;
        this.isHighScore = false;

        this.generationText.GetComponent<Text>().text = "勇者No：" + generationCount.ToString();
        //this.heroPointText.GetComponent<Text>().text = "HeroPoint:" + HeroPoint.ToString();
        //this.HPText.GetComponent<Text>().text = "HP:" + Player.GetComponent<PlayerController>().PlayerHP.ToString();

        this.isChangeIndex = false;
        this.index = Index.PlayerSelect;
        SwitchIndex(index, 1);
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
        this.isDebug = this.Player.GetComponent<PlayerController>().isDebug;

        this.HeroPointMAX = ((this.StageNo - 1) / 10) + 2;
        
        if (this.HeroPoint >= this.HeroPointMAX)
        {
            this.HeroPoint = this.HeroPointMAX;
        }

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

    public void FadeIn()
    {
        if (isFadeIn)
        {
            this.FadePanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, alfa);
            alfa -= 0.02f;

            if (alfa <= 0)
            {
                alfa = 0;
                isFadeIn = false;
            }

        }        
    }

    public void FadeOut()
    {
        if (isFadeOut)
        {
            this.FadePanel.GetComponent<Image>().color = new Color(0f, 0f, 0f, alfa);
            alfa += 0.02f;

            if (alfa >= 1)
            {
                alfa = 1;
                isFadeOut = false;
            }
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
                
                
                this.isSeTiming = true;
                ModeNo = 0;
                this.isFadeIn = true;
                break;
            case 2:

                this.waittime += Time.deltaTime;

                switch (ModeNo)
                {
                    case 0:
                        FadeIn();
                        if (isFadeIn == false)
                        {
                            ModeNo++;
                            waittime = 0;
                        }
                        break;
                    case 1:
                        this.centerText.GetComponent<Text>().text = "次の勇者は…";
                        this.centerBack.GetComponent<Image>().enabled = true;

                        if (this.isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[0]);
                            isSeTiming = false;
                        }
                       

                        if (this.waittime >= 2)
                        {
                            this.waittime = 0;
                            ModeNo++;
                            isSeTiming = true;
                        }

                        break;
                    case 2:
                        this.centerText.GetComponent<Text>().text = "クリックで決定だ！";
                        this.centerBack.GetComponent<Image>().enabled = true;

                        if (this.waittime >=  0.1f)
                        {
                            Seaudios.PlayOneShot(Seclips[2]);
                            waittime = 0;
                            GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(false);
                            this.PlayerNo++;
                            if (PlayerNo >= 5)
                            {
                                PlayerNo = 0;
                            }
                            GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(true);
                        }

                        if (Input.GetMouseButtonDown(0))
                        {
                            this.isHeroDecision = true;
                            ModeNo++;
                            this.waittime = 0;
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha0) && this.isDebug)
                        {
                            PlayerNo = 0;
                            this.isHeroDecision = true;
                            ModeNo++;
                            this.waittime = 0;
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha1) && this.isDebug)
                        {
                            PlayerNo = 1;
                            this.isHeroDecision = true;
                            ModeNo++;
                            this.waittime = 0;
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha2) && this.isDebug)
                        {
                            PlayerNo = 2;
                            this.isHeroDecision = true;
                            ModeNo++;
                            this.waittime = 0;
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha3) && this.isDebug)
                        {
                            PlayerNo = 3;
                            this.isHeroDecision = true;
                            ModeNo++;
                            this.waittime = 0;
                        }
                        else if (Input.GetKeyDown(KeyCode.Alpha4) && this.isDebug)
                        {
                            PlayerNo = 4;
                            this.isHeroDecision = true;
                            ModeNo++;
                            this.waittime = 0;
                        }


                        break;
                    case 3:
                        if (this.isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[1]);
                            this.isSeTiming = false;
                        }
                        this.centerText.GetComponent<Text>().text = "";
                        this.centerBack.GetComponent<Image>().enabled = false;
                        this.SwordDummy.GetComponent<Renderer>().enabled = true;
                        this.underBack.GetComponent<Image>().enabled = true;

                        if (this.PlayerNo == 0)
                        {
                            this.underText.GetComponent<Text>().text = "騎士(スキル：超スラッシュ)";
                        }
                        else if (this.PlayerNo == 1)
                        {
                            this.underText.GetComponent<Text>().text = "宇宙飛行士(スキル：ロケット)";
                        }
                        else if (this.PlayerNo == 2)
                        {
                            this.underText.GetComponent<Text>().text = "医者(スキル：回復)";
                        }
                        else if (this.PlayerNo == 3)
                        {
                            this.underText.GetComponent<Text>().text = "牧場主(スキル：羊呼び)";
                        }
                        else if (this.PlayerNo == 4)
                        {
                            this.underText.GetComponent<Text>().text = "ゾンビ(スキル：ゆっくり走る)";
                        }

                        if (this.waittime >= 5)
                        {
                            this.preindex = this.index;
                            this.index = Index.GameStart;
                            this.isChangeIndex = true;
                        }
                        break;
                    default:
                        break;

                }
                
                break;
            case 3:
                this.waittime = 0;
                ModeNo = 1;
                this.isHeroDecision = false;
                GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(false);
                PlayerDummy.gameObject.SetActive(false);
                Geometry.GetChild(PlayerNo).gameObject.SetActive(true);
                this.Sword.GetComponent<Renderer>().enabled = true;
                GeometryDummy.GetChild(PlayerNo).gameObject.SetActive(false);
                this.isSeTiming = true;
                this.underText.GetComponent<Text>().text = "";
                this.underBack.GetComponent<Image>().enabled = false;
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
                this.Player.GetComponent<PlayerController>().Setting.PlayerHP = 3;
                this.Player.GetComponent<PlayerController>().Setting.nowpositionX = 0;
                this.centerText.GetComponent<Text>().text = "ステージ" + StageNo.ToString();
                this.centerBack.GetComponent<Image>().enabled = true;
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
                this.centerBack.GetComponent<Image>().enabled = false;
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
                this.Player.GetComponent<PlayerController>().Setting.movableX = true;

                if (this.StageNo % 10 >= 1 && this.StageNo % 10 <= 3)
                {
                    BGM.GetComponent<AudioController>().AudioChange(1);
                    //this.BGM.GetComponent<AudioController>().BGMNo = 1;
                }
                else if (this.StageNo % 10 >= 4 && this.StageNo % 10 <= 6)
                {
                    BGM.GetComponent<AudioController>().AudioChange(2);
                    //this.BGM.GetComponent<AudioController>().BGMNo = 2;
                }
                else if (this.StageNo % 10 >= 7 || this.StageNo % 10 == 0)
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
                else if ((this.StageNo % 10) % 3 == 0)
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
                this.centerText.GetComponent<Text>().text = "ステージ" + StageNo.ToString() + " クリア";
                this.centerBack.GetComponent<Image>().enabled = true;
                BGM.GetComponent<AudioController>().AudioChange(12);
                this.Stage.GetComponent<StageController>().isCreateNewArea = true;
                ModeNo = 1;
                this.isBossDefeat = false;
                break;
            case 2:
                this.waittime += Time.deltaTime;

                switch (ModeNo)
                {
                    case 1:
                        if (this.waittime >= 2 && this.waittime < 4)
                        {
                            
                            
                            if (isDeathatStage == false)
                            {
                                this.centerText.GetComponent<Text>().text = "駆け抜けボーナス！";

                                if (this.isSeTiming)
                                {
                                    Seaudios.PlayOneShot(Seclips[0]);
                                    isSeTiming = false;
                                }
                            }
                            /*
                            else
                            {
                                this.centerText.GetComponent<Text>().text = "スコア：" + scoreDirector.GetComponent<ScoreController>().defeatCount.ToString() + "";
                            }
                            */
                            
                        }
                        if (this.waittime >= 4)
                        {
                            isSeTiming = true;
                            ModeNo++;
                            this.waittime = 0;
                            if (isDeathatStage == false)
                            {
                                this.scoreDirector.GetComponent<ScoreController>().defeatCount += 5;
                            }
                        }
                        break;
                    case 2:
                        if (this.StageNo == 10)
                        {
                            if (this.waittime < 3)
                            {
                                this.centerText.GetComponent<Text>().fontSize = 120;
                                this.centerText.GetComponent<Text>().text = "悪のドラゴンは倒した！";
                                this.centerBack.GetComponent<Image>().enabled = true;
                            }
                            else if (this.waittime >=  3 && this.waittime < 6)
                            {
                                this.centerText.GetComponent<Text>().text = "だが、まだ敵は残っている！\n走り続けろ勇者！";
                            }
                            else if (this.waittime >= 6)
                            {
                                ModeNo++;
                                this.waittime = 0;
                            }
                            
                        }
                        else
                        {
                            ModeNo++;
                            this.waittime = 0;
                        }
                        break;
                    case 3:
                        if (this.waittime >= 2)
                        {
                            this.preindex = this.index;
                            this.index = Index.NextStage;
                            this.isChangeIndex = true;
                        }
                        break;
                }
                break;
            case 3:
                //this.Stage.GetComponent<StageController>().isNextStage = true;
                this.centerText.GetComponent<Text>().text = "";
                this.centerText.GetComponent<Text>().fontSize = 180;
                this.centerBack.GetComponent<Image>().enabled = false;
                this.waittime = 0;
                this.StageNo++;
                this.ModeNo = 1;
                this.isSeTiming = true;
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
                this.isDeathatStage = true;
                this.isFadeOut = true;
                this.isSeTiming = true;
                break;
            case 2:
                this.waittime += Time.deltaTime;

                if (this.waittime >= 2 && this.waittime < 6)
                {
                    if (this.HeroPointRatio >= 1)
                    {
                        this.centerText.GetComponent<Text>().text = "バトンタッチ！";
                        this.centerBack.GetComponent<Image>().enabled = true;
                    }
                    else
                    {
                        this.centerText.GetComponent<Text>().text = "GameOver";
                        this.centerBack.GetComponent<Image>().enabled = true;
                    }
                }

                if (this.waittime >= 6 && this.waittime < 10)
                {
                    if (this.HeroPointRatio >= 1)
                    {
                        waittime = 10;
                    }
                    else
                    {
                        if (isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[0]);
                            isSeTiming = false;
                        }
                        this.centerText.GetComponent<Text>().text = "最終スコア：" + this.scoreDirector.GetComponent<ScoreController>().defeatCount.ToString();
                        this.centerBack.GetComponent<Image>().enabled = true;
                    }
                }

                if (this.waittime >= 10 && this.waittime < 14)
                {
                    if (this.HeroPointRatio >= 1)
                    {
                        waittime = 14;
                    }
                    else
                    {
                        if (this.scoreDirector.GetComponent<ScoreController>().defeatCount > highscore)
                        {
                            if (highscore != 0)
                            {
                                Seaudios.PlayOneShot(Seclips[0]);
                                this.centerText.GetComponent<Text>().text = "ハイスコア！！";
                                this.centerBack.GetComponent<Image>().enabled = true;
                                this.isHighScore = true;
                            }

                            highscore = this.scoreDirector.GetComponent<ScoreController>().defeatCount;

                            if (isHighScore)
                            {
                                
                            }
                            else
                            {
                                this.waittime = 14;
                            }
                            
                        }
                        
                    }
                }

                if (this.waittime >= 14)
                {
                    FadeOut();
                    if (this.isFadeOut == false)
                    {
                        this.waittime = 0;
                        generationCount++;
                        this.generationText.GetComponent<Text>().text = "勇者No：" + generationCount.ToString();
                        this.centerText.GetComponent<Text>().text = "";
                        this.centerBack.GetComponent<Image>().enabled = false;

                        if (this.HeroPointRatio >= 1)
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

                if (isPlayerOrigin)
                {
                    isPlayerOrigin = false;
                    this.Player.transform.position = new Vector3(0, 0.1f, -10);
                    this.Stage.GetComponent<StageController>().StageStartPos = 0;
                    this.Stage.GetComponent<StageController>().StageCreatePos = 200;
                }
                
                this.centerText.GetComponent<Text>().text = "ステージ" + StageNo.ToString();
                this.centerBack.GetComponent<Image>().enabled = true;
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
                this.centerBack.GetComponent<Image>().enabled = false;
                this.isDeathatStage = false;
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
                this.isSeTiming = true;
                this.waittime = 0;
                ModeNo = 1;
                break;
            case 2:
                this.waittime += Time.deltaTime;
                switch (ModeNo)
                {
                   case 1:
                        if (isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[2]);
                            this.isSeTiming = false;
                        }
                        this.centerText.GetComponent<Text>().text = "3";
                        this.centerBack.GetComponent<Image>().enabled = true;

                        if (this.waittime >= 1)
                        {
                            this.waittime = 0;
                            isSeTiming = true;
                            ModeNo++;
                        }
                        break;
                    case 2:
                        if (isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[2]);
                            this.isSeTiming = false;
                        }
                        this.centerText.GetComponent<Text>().text = "2";

                        if (this.waittime >= 1)
                        {
                            this.waittime = 0;
                            isSeTiming = true;
                            ModeNo++;
                        }
                        break;
                    case 3:
                        if (isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[2]);
                            this.isSeTiming = false;
                        }
                        this.centerText.GetComponent<Text>().text = "1";

                        if (this.waittime >= 1)
                        {
                            this.waittime = 0;
                            isSeTiming = true;
                            ModeNo++;
                        }
                        break;
                    case 4:
                        if (isSeTiming)
                        {
                            Seaudios.PlayOneShot(Seclips[3]);
                            this.isSeTiming = false;
                        }
                        this.centerText.GetComponent<Text>().text = "スタート!";

                        if (this.waittime >= 1)
                        {
                            this.waittime = 0;
                            isSeTiming = true;
                            ModeNo++;
                            this.preindex = this.index;
                            this.index = Index.StageCreate;
                            this.isChangeIndex = true;
                        }
                        break;
                    case 5:
                        break;
                }
                break;

            case 3:
                this.waittime = 0;
                this.centerText.GetComponent<Text>().text = "";
                this.centerBack.GetComponent<Image>().enabled = false;
                this.countdowntime = 4;
                this.isSeTiming = true;
                ModeNo = 1;
                break;
            default:
                break;
        }
    }
}
