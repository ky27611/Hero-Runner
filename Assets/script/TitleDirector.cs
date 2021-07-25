using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    private GameObject Story1;
    private GameObject Story2;
    private GameObject Operation1;
    private GameObject OperationText;
    private GameObject Cursor1;
    private GameObject Cursor2;
    private GameObject Cursor3;
    private GameObject Cursor4;
    private GameObject Crystal;
    private GameObject Flower;
    private GameObject imageBack;
    private GameObject FadePanel;
    private GameObject HighScoreText;
    private float waittime;
    private bool isGameStart;
    private bool isMethod;
    private bool isSe;
    public bool isFadeIn;
    public bool isFadeOut;
    public AudioClip[] clips;
    AudioSource audios;
    public int BGMNo;
    public int ModeNo;
    public float alfa;

    //private GameDirector gameDirector;


    // Start is called before the first frame update
    void Start()
    {
        this.Story1 = GameObject.Find("Story1");
        this.Story2 = GameObject.Find("Story2");
        this.Operation1 = GameObject.Find("Operation1");
        this.OperationText = GameObject.Find("OperationText");
        this.Cursor1 = GameObject.Find("Cursor1");
        this.Cursor2 = GameObject.Find("Cursor2");
        this.Cursor3 = GameObject.Find("Cursor3");
        this.Cursor4 = GameObject.Find("Cursor4");
        this.Crystal = GameObject.Find("Crystal");
        this.Flower = GameObject.Find("Flower");
        this.imageBack = GameObject.Find("imageBack");
        this.FadePanel = GameObject.Find("FadePanel");
        this.HighScoreText = GameObject.Find("HighScore");
        audios = GetComponent<AudioSource>();
        this.BGMNo = 0;
        this.waittime = 0;
        this.ModeNo = 0;
        this.isGameStart = false;
        this.isMethod = false;
        this.isSe = true;

        audios.loop = true;
        audios.clip = clips[1];
        audios.Play();
        this.alfa = 1f;
        this.isFadeIn = true;
        this.isFadeOut = false;

        this.Story1.gameObject.SetActive(false);
        this.Story2.gameObject.SetActive(false);
        this.Operation1.gameObject.SetActive(false);
        this.Cursor1.gameObject.SetActive(false);
        this.Cursor2.gameObject.SetActive(false);
        this.Cursor3.gameObject.SetActive(false);
        this.Cursor4.gameObject.SetActive(false);
        this.Crystal.gameObject.SetActive(false);
        this.Flower.gameObject.SetActive(false);
        this.imageBack.gameObject.SetActive(false);

        this.HighScoreText.GetComponent<Text>().text = "ハイスコア：" + GameDirector.highscore.ToString();

    }

    // Update is called once per frame
    void Update()
    {

        switch (ModeNo)
        {
            case 0:
                FadeIn();
                if (isFadeIn == false)
                {
                    ModeNo = 1;
                    isFadeOut = true;
                }
                break;
            case 1:
                this.waittime += Time.deltaTime;
                this.OperationText.GetComponent<Text>().text = "";
                if (isGameStart)
                {
                    
                    if (this.waittime >= 2)
                    {
                        //SceneManager.LoadScene("Stage0Scene");
                        isGameStart = false;
                        this.waittime = 0;
                        audios.Stop();
                        audios.clip = clips[3];
                        audios.Play();
                        ModeNo = 2;
                    }
                }

                if (isMethod)
                {
                    
                    if (this.waittime >= 1)
                    {
                        isMethod = false;
                        this.Operation1.gameObject.SetActive(true);
                        ModeNo = 11;
                    }
                }
                break;
            case 2:
                this.waittime += Time.deltaTime;
                this.Story1.gameObject.SetActive(true);

                if (audios.isPlaying == false)
                {
                    audios.clip = clips[3];
                    audios.Play();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    this.waittime = 5;
                }

                if (this.waittime >= 5 )
                {
                    //this.Story1.gameObject.SetActive(false);
                    ModeNo = 3;
                    this.waittime = 0;
                }

                break;
            case 3:
                this.waittime += Time.deltaTime;
                this.Story2.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    this.waittime = 5;
                }

                if (this.waittime >= 5 && this.waittime < 7)
                {
                    if (isSe)
                    {
                        this.isSe = false;
                        audios.PlayOneShot(clips[2]);
                        this.FadePanel.GetComponent<Image>().enabled = true;
                    }
                }
                else if (this.waittime >= 7)
                {
                    FadeOut();
                    if (isFadeOut == false)
                    {
                        SceneManager.LoadScene("Stage0Scene");
                        this.waittime = 0;
                        this.isSe = false;
                    }
                }
                break;
            case 11:
                this.OperationText.GetComponent<Text>().text = "敵を倒し障害物を避けながらスコアを稼いで走り続けよう！\nキーボードとマウスで操作するよ！";
                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo ++;
                    audios.PlayOneShot(clips[4]);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo = 1;
                    audios.PlayOneShot(clips[5]);
                    this.Story1.gameObject.SetActive(false);
                    this.Story2.gameObject.SetActive(false);
                    this.Operation1.gameObject.SetActive(false);
                    this.Cursor1.gameObject.SetActive(false);
                    this.Cursor2.gameObject.SetActive(false);
                    this.Cursor3.gameObject.SetActive(false);
                    this.Cursor4.gameObject.SetActive(false);
                    this.Crystal.gameObject.SetActive(false);
                    this.Flower.gameObject.SetActive(false);
                    this.imageBack.gameObject.SetActive(false);
                }
                break;
            case 12:
                this.OperationText.GetComponent<Text>().text = "      W：ジャンプ\nA・D：左右移動\n                 S：スライディング";
                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo = 14;
                    audios.PlayOneShot(clips[4]);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    audios.PlayOneShot(clips[5]);
                }
                break;
            case 13:
                this.OperationText.GetComponent<Text>().text = "      W：ジャンプ\nA・D：左右移動\n                 S：スライディング";
                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    audios.PlayOneShot(clips[5]);
                }
                break;
            case 14:
                this.OperationText.GetComponent<Text>().text = "攻撃して敵を倒してスコアアップ！\n       クリック：通常攻撃\n右クリック：スキル";
                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo = 12;
                    audios.PlayOneShot(clips[5]);
                }
                break;
            case 15:
                this.OperationText.GetComponent<Text>().text = "スキルは勇者ごとに異なるよ！\n画面右下のスキルゲージが満タンで使用可能！\nスキルゲージは時間経過で溜まっていく！";
                this.Cursor1.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                    this.Cursor1.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    this.Cursor1.gameObject.SetActive(false);
                    audios.PlayOneShot(clips[5]);
                }
                break;
            case 16:
                this.OperationText.GetComponent<Text>().text = "左上のハートはHP！\n攻撃を受けたり、敵や障害物にぶつかると下がる！\nなくなったら、ゲームオーバー...";
                this.Cursor2.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                    this.Cursor2.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    this.Cursor2.gameObject.SetActive(false);
                    audios.PlayOneShot(clips[5]);
                }
                break;
            case 17:
                this.OperationText.GetComponent<Text>().text = "でも、勇者ゲージが溜まっていれば大丈夫！！\nなんとゲームオーバーにならない！！\n次の勇者にバトンタッチ！！";
                this.Cursor3.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                    this.Cursor3.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    this.Cursor3.gameObject.SetActive(false);
                    audios.PlayOneShot(clips[5]);
                }
                break;
            case 18:
                this.OperationText.GetComponent<Text>().text = "勇者ゲージは赤いクリスタルを取ると溜まる！\n満タン状態で取るとスコアが上がるよ！";
                this.Crystal.gameObject.SetActive(true);
                this.imageBack.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                    this.Crystal.gameObject.SetActive(false);
                    this.imageBack.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    audios.PlayOneShot(clips[5]);
                    this.Crystal.gameObject.SetActive(false);
                    this.imageBack.gameObject.SetActive(false);
                }
                break;
            case 19:
                this.OperationText.GetComponent<Text>().text = "お花は踏むと勇者ゲージが下がっちゃう…\nゲージが空の時に踏むとスコアが下がる…\n勇者たるもの自然は大切に…";
                this.Flower.gameObject.SetActive(true);
                this.imageBack.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                    this.Flower.gameObject.SetActive(false);
                    this.imageBack.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    ModeNo--;
                    audios.PlayOneShot(clips[5]);
                    this.Flower.gameObject.SetActive(false);
                    this.imageBack.gameObject.SetActive(false);
                }
                break;
            case 20:
                this.OperationText.GetComponent<Text>().text = "スコアは右上に表示されてるよ！\n敵やボスを倒す、クリスタルを集めることで上がっていく！\n倒れずにステージクリアでボーナスもあるよ！";
                this.Cursor4.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    this.Cursor4.gameObject.SetActive(false);
                    ModeNo++;
                    audios.PlayOneShot(clips[4]);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    audios.PlayOneShot(clips[5]);
                    this.Cursor4.gameObject.SetActive(false);
                    ModeNo--;
                }
                break;
            case 21:
                this.OperationText.GetComponent<Text>().text = "説明は以上だ！\n頑張って高スコアを目指そう！";
                

                if (Input.GetMouseButtonDown(0))
                {
                    audios.PlayOneShot(clips[4]);
                    ModeNo = 1;
                    this.Story1.gameObject.SetActive(false);
                    this.Story2.gameObject.SetActive(false);
                    this.Operation1.gameObject.SetActive(false);
                    this.Cursor1.gameObject.SetActive(false);
                    this.Cursor2.gameObject.SetActive(false);
                    this.Cursor3.gameObject.SetActive(false);
                    this.Cursor4.gameObject.SetActive(false);
                    this.Crystal.gameObject.SetActive(false);
                    this.Flower.gameObject.SetActive(false);
                    this.imageBack.gameObject.SetActive(false);
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    audios.PlayOneShot(clips[5]);
                    ModeNo--;
                }
                break;

        }

    }

    public void GameStart()
    {
        this.isGameStart = true;
        audios.PlayOneShot(clips[2]);
    }
    public void Method()
    {
        this.isMethod = true;
        audios.PlayOneShot(clips[2]);
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
                this.FadePanel.GetComponent<Image>().enabled = false;
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
}
