using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    GameObject scoreText;
    GameObject defeatText;
    private float time = 0;
    private int timescore = 0;
    private int timescoremagnification = 10;
    private int defeatscore = 0;
    private int score = 0;
    private int defeatCount = 0;
    public bool isTimeScore;
    private GameObject gamedirector;

    // Start is called before the first frame update
    void Start()
    {
        this.scoreText = GameObject.Find("Score");
        this.defeatText = GameObject.Find("Defeat");
        this.gamedirector = GameObject.Find("GameDirector");
        this.isTimeScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        //経過時間スコア
        if (this.isTimeScore == true && this.gamedirector.GetComponent<GameDirector>().isGameStart == false)
        {
            this.time += Time.deltaTime;
            this.timescore = 0;// timescoremagnification * (int)(time * 10);
        }

        //経過時間スコアと敵倒したポイントをスコアとして表示
        this.score = defeatscore + timescore;
        this.scoreText.GetComponent<Text>().text = "Score：" + this.score.ToString();
    }

    //敵を倒すとスコア加算
    public void DefeatEnemy()
    {
        //this.defeatscore += 1000;
        this.defeatCount++;
        this.defeatText.GetComponent<Text>().text = "Defeat：" + this.defeatCount.ToString();
    }
    //ボスを倒すとスコア加算
    public void DefeatBoss()
    {
        //this.defeatscore += 5000;
    }

}
