using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public GameObject RoadPrefab;
    public GameObject BarricadePrefab;
    public GameObject FencePrefab;
    public GameObject RockPrefab;
    public GameObject SlimePrefab;
    public GameObject TurtlePrefab;
    public GameObject PurplePrefab;
    public GameObject FlowerPrefab;
    public GameObject TreePrefab;
    public GameObject CrystalPrefab;
    public GameObject BossSlimePrefab;

    public GameObject Player;

    private float PlayerDistance;
    private float CreatePos;
    private float CreateRange;
    private float CreateDistance;
    private bool isCreate;
    public float StartPos;
    public float GoalPos;
    public float PosRange;


    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.isCreate = true;
        this.StartPos = 80f;
        this.GoalPos = 400f;
        this.CreateDistance = 55f;
        this.CreateRange = 15f;
        this.CreatePos = this.StartPos;
        this.PosRange = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //生成点とPlayerとの距離が一定以下になったらアイテム生成して次の生成点を更新
        this.PlayerDistance = this.CreatePos - Player.transform.position.z;

        if (this.PlayerDistance <= this.CreateDistance && this.isCreate == true)
        {
            CreateItem();

            this.CreatePos += this.CreateRange;

            //ゴール地点から先には生成しない
            if (this.CreatePos > this.GoalPos)
            {
                this.isCreate = false;
            }

        }



    }

    void CreateItem()
    {
        //どのアイテムを出すのかをランダムに設定
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        if (num1 <= 2)
        {
            //障害物
            if (num2 <= 3)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos);
            }
            //else if(num2 <= 7)
            else
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos);
            }
        }
        else if(3 <= num1 && num1 <= 7)
        {
            
            //アイテムを置く座標のオフセットをランダムに設定
            int offsetZ = Random.Range(-5, 6);
            int offsetX = Random.Range(-1, 2);

            if (num2 == 1)
                {
                    //を生成
                    GameObject Crystal = Instantiate(CrystalPrefab);
                    Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
                }
                else if (7 <= num2 && num2 <= 8)
                {
                    //を生成
                    GameObject Rock = Instantiate(RockPrefab);
                    Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
                }
                else if (num2 == 9)
                {
                    //を生成
                    GameObject Tree = Instantiate(TreePrefab);
                    Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
                }
                else if (num2 == 10)
                {
                    //を生成
                    GameObject Flower = Instantiate(FlowerPrefab);
                    Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
                }
            
        }
        else
        {
            if (num2 <= 5)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(0, Slime.transform.position.y, CreatePos);
            }
            else if(6 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(0, Turtle.transform.position.y, CreatePos);
            }
            else if (9 <= num2 && num2 <= 10)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(0, Purple.transform.position.y, CreatePos);
            }
        }
    }

}
