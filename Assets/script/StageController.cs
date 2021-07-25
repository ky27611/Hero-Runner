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
    public GameObject BossTurtlePrefab;
    public GameObject BossPurplePrefab;
    public GameObject BossDragonPrefab;
    public GameObject Road1Prefab;
    public GameObject Road2Prefab;
    public GameObject Road3Prefab;

    public GameObject Player;
    public GameObject Director;

    private float PlayerDistance;
    private float CreatePos;
    private float CreateRange;
    private float CreateDistance;
    public bool isCreate;
    public bool isRandom;
    public bool isRoadDebug;
    private bool isGoal;
    public bool isStageCreate;
    public bool isCreateNewArea;
    public float StartPos;
    public float GoalPos;
    public float PosRange;
    public float StageStartPos;
    public float StageCreatePos;
    public int AreaNo;
    public int DummyStageNo;
    public int PlacementNo;
    public int DummyPlacementNo;
    private int Ocount;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
        this.Director = GameObject.Find("GameDirector");
        this.isCreate = false;
        this.isRandom = false;
        this.isRoadDebug = false;
        this.isStageCreate = false;
        this.isCreateNewArea = false;
        this.isGoal = false;
        this.StartPos = 80f;
        this.GoalPos = 400f;
        this.CreateDistance = 55f;
        this.CreateRange = 15f;
        this.CreatePos = this.StartPos;
        this.PosRange = 2f;
        this.StageStartPos = 0f;
        this.StageCreatePos = 200f;
        this.AreaNo = 1;
        this.PlacementNo = 1;
        this.DummyStageNo = 1;
        this.DummyPlacementNo = 1;
        this.Ocount = 0;

}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            this.Ocount++;

            if (this.Ocount >= 3)
            {
                if (this.isRoadDebug)
                {
                    isRoadDebug = false;
                    this.Ocount = 0;
                }
                else
                {
                    isRoadDebug = true;
                    this.Ocount = 0;
                }
            }
        }

        this.DummyStageNo = this.Director.GetComponent<GameDirector>().StageNo % 10;

        if (this.DummyStageNo >= 1 && this.DummyStageNo < 4)
        {
            this.AreaNo = 1;
        }
        else if (this.DummyStageNo >= 4 && this.DummyStageNo < 7)
        {
            this.AreaNo = 2;
        }
        else if (this.DummyStageNo >= 7 || this.DummyStageNo == 0 )
        {
            this.AreaNo = 3;
        }

        if (isRandom)
        {
            this.isRandom = false;
            this.DummyPlacementNo = Random.Range(1, 11);
            if (this.DummyPlacementNo >= 1 && this.DummyPlacementNo < 5)
            {
                this.PlacementNo = 1;
            }
            else if (this.DummyPlacementNo >= 5 && this.DummyPlacementNo < 8)
            {
                this.PlacementNo = 2;
            }
            else if (this.DummyPlacementNo == 8)
            {
                this.PlacementNo = 3;
            }
            else if (this.DummyPlacementNo == 9)
            {
                this.PlacementNo = 4;
            }
            else if (this.DummyPlacementNo == 10)
            {
                this.PlacementNo = 5;
            }
        }


        if (this.isCreate == false && this.isStageCreate == true)
        {
            StageCreate();
        }

        if (this.isCreateNewArea)
        {
            CreateNewArea();
        }
        
        if (this.StageStartPos - Player.transform.position.z <= 0)
        {
            /*
            GameObject Road = Instantiate(RoadPrefab);
            Road.transform.position = new Vector3(0, Road.transform.position.y, StageCreatePos);
            this.StageStartPos += 1000f;
            this.StageCreatePos += 1000f;
            */

            RoadCreate();

        }


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

        if (Player.transform.position.z >= this.GoalPos && isGoal == false)//this.GoalPos - Player.transform.position.z <= 0)
        {
            this.isGoal = true;
            
            if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 0)
            {
                LastBossCreate();
            }
            else if ((this.Director.GetComponent<GameDirector>().StageNo % 10) % 3 == 0)
            {
                BossCreate();
            }
            else
            {
                this.Director.GetComponent<GameDirector>().isStageClear = true;
                //this.Director.GetComponent<GameDirector>().index = GameDirector.Index.StageClear;
                //this.Director.GetComponent<GameDirector>().isChangeIndex = true;
            }
        }

    }

    public void CreateNewArea()
    {
        /*
        this.StageStartPos = 0f;
        this.StageCreatePos = 200f;
        */

        this.isCreateNewArea = false;

        if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 0)
        {
            GameObject Road1 = Instantiate(Road1Prefab);
            Road1.transform.position = new Vector3(0, Road1.transform.position.y, 0);
            this.Director.GetComponent<GameDirector>().isPlayerOrigin = true;
        }
        else if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 3)
        {
            GameObject Road2 = Instantiate(Road2Prefab);
            Road2.transform.position = new Vector3(0, Road2.transform.position.y, 0);
            this.Director.GetComponent<GameDirector>().isPlayerOrigin = true;
        }
        else if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 6)
        {
            GameObject Road3 = Instantiate(Road3Prefab);
            Road3.transform.position = new Vector3(0, Road3.transform.position.y, 0);
            this.Director.GetComponent<GameDirector>().isPlayerOrigin = true;
        }

    }

    void StageCreate()
    {
        this.StartPos = Player.transform.position.z + 50f;
        this.GoalPos = this.StartPos + 400f;
        this.CreatePos = this.StartPos + this.CreateRange;
        this.isCreate = true;
        this.isRandom = true;
        this.isStageCreate = false;
        this.isGoal = false;
        this.Director.GetComponent<GameDirector>().isNormalMode = true;
        //this.Director.GetComponent<GameDirector>().index = GameDirector.Index.NormalMode;
        //this.Director.GetComponent<GameDirector>().isChangeIndex = true;
    }

    void RoadCreate()
    {
        /*
        GameObject Road1 = Instantiate(Road1Prefab);
        Road1.transform.position = new Vector3(0, Road1.transform.position.y, StageCreatePos);
        this.StageStartPos += 1000f;
        this.StageCreatePos += 1000f;
        */

        if (this.Director.GetComponent<GameDirector>().StageNo % 10 >= 1 && this.Director.GetComponent<GameDirector>().StageNo % 10 <= 3)
        {
            GameObject Road1 = Instantiate(Road1Prefab);
            Road1.transform.position = new Vector3(0, Road1.transform.position.y, StageCreatePos);
        }
        else if (this.Director.GetComponent<GameDirector>().StageNo % 10 >= 4 && this.Director.GetComponent<GameDirector>().StageNo % 10 <= 6)
        {
            GameObject Road2 = Instantiate(Road2Prefab);
            Road2.transform.position = new Vector3(0, Road2.transform.position.y, StageCreatePos);
        }
        else if (this.Director.GetComponent<GameDirector>().StageNo % 10 >= 7 && this.Director.GetComponent<GameDirector>().StageNo % 10 <= 9)
        {
            GameObject Road3 = Instantiate(Road3Prefab);
            Road3.transform.position = new Vector3(0, Road3.transform.position.y, StageCreatePos);
        }
        else
        {
            GameObject Road3 = Instantiate(Road3Prefab);
            Road3.transform.position = new Vector3(0, Road3.transform.position.y, StageCreatePos);
        }
        this.StageStartPos += 200f;
        this.StageCreatePos += 200f;

    }

    void CreateItem()
    {
        if (isRoadDebug == false)
        {
            switch (AreaNo)
            {
                case 1:
                    Area1();
                    break;
                case 2:
                    Area2();
                    break;
                case 3:
                    Area3();
                    break;
            }
        }
        
        /*
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
            //アイテムを置く座標のオフセットをランダムに設定
            int offsetZ = Random.Range(-5, 6);
            int offsetX = Random.Range(-1, 2);

            if (num2 <= 5)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if(6 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 10)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        */
    }

    void BossCreate()
    {
        //this.Director.GetComponent<GameDirector>().index = GameDirector.Index.BossMode;

        this.Director.GetComponent<GameDirector>().isBossMode = true;

        if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 3)
        {
            GameObject BossSlime = Instantiate(BossSlimePrefab);
            BossSlime.transform.position = new Vector3(0, BossSlime.transform.position.y, Player.transform.position.z + 12);
        }
        else if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 6)
        {
            GameObject BossTurtle = Instantiate(BossTurtlePrefab);
            BossTurtle.transform.position = new Vector3(0, BossTurtle.transform.position.y, Player.transform.position.z + 12);
        }
        else if (this.Director.GetComponent<GameDirector>().StageNo % 10 == 9)
        {
            GameObject BossPurple = Instantiate(BossPurplePrefab);
            BossPurple.transform.position = new Vector3(0, BossPurple.transform.position.y, Player.transform.position.z + 12);
        }
    }

    void LastBossCreate()
    {
        this.Director.GetComponent<GameDirector>().isBossMode = true;
        GameObject BossDragon = Instantiate(BossDragonPrefab);
        BossDragon.transform.position = new Vector3(0, BossDragon.transform.position.y, Player.transform.position.z + 12);
    }

    void Area1()
    {
        switch (PlacementNo)
        {
            case 1:
                StageA1();
                break;
            case 2:
                StageA2();
                break;
            case 3:
                StageA3();
                break;
            case 4:
                StageA4();
                break;
            case 5:
                StageA5();
                break;

        }
    }

    void Area2()
    {
        switch (PlacementNo)
        {
            case 1:
                StageB1();
                break;
            case 2:
                StageB2();
                break;
            case 3:
                StageB3();
                break;
            case 4:
                StageB4();
                break;
            case 5:
                StageB5();
                break;

        }
    }

    void Area3()
    {
        switch (PlacementNo)
        {
            case 1:
                StageC1();
                break;
            case 2:
                StageC2();
                break;
            case 3:
                StageC3();
                break;
            case 4:
                StageC4();
                break;
            case 5:
                StageC5();
                break;

        }
    }

    void StageA1()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);


        if (num1 <= 2)
        {
            if (num2 <= 8)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }

        }
        else if (num1 >= 3 && num1 <= 4)
        {
            if (num2 <= 4)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos + offsetZ);
            }
            //else if(num2 <= 7)
            else if(num2 >= 5 && num2 <= 9) 
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >=5  && num1 <= 7)
        {
            if (num2 <= 5)
            {
                //を生成
                GameObject Rock = Instantiate(RockPrefab);
                Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 6 && num2 <= 9)
            {
                //を生成
                GameObject Tree = Instantiate(TreePrefab);
                Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 3)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 4 && num2 <= 5)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }
    }

    void StageA2()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 1)
        {
            if (num2 <= 8)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 2 && num1 <= 4)
        {
            if (num2 <= 4)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos + offsetZ);
            }
            //else if(num2 <= 7)
            else if (num2 >= 5 && num2 <= 9)
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 5 && num1 <= 7)
        {
            if (num2 <= 5)
            {
                //を生成
                GameObject Rock = Instantiate(RockPrefab);
                Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 6 && num2 <= 9)
            {
                //を生成
                GameObject Tree = Instantiate(TreePrefab);
                Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 3)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 4 && num2 <= 5)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }
    }

    void StageA3()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 8)
        {
            if (num2 <= 8)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
        }

    }
    void StageA4()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 5)
        {
            if (num2 <= 8)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 3)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }

        }
    }
    void StageA5()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 5)
        {
            if (num2 <= 8)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 3)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }

    }

    void StageB1()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if(num1 <= 2)
        {
            if (num2 <= 4)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (5 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 3 && num1 <= 4)
        {
            if (num2 <= 4)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos + offsetZ);
            }
            //else if(num2 <= 7)
            else if (num2 >= 5 && num2 <= 9)
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 5 && num1 <= 7)
        {
            if (num2 <= 5)
            {
                //を生成
                GameObject Rock = Instantiate(RockPrefab);
                Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 6 && num2 <= 9)
            {
                //を生成
                GameObject Tree = Instantiate(TreePrefab);
                Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 2)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 4 && num2 <= 5)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }

    }

    void StageB2()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 1)
        {
            if (num2 <= 4)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (5 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 2 && num1 <= 4)
        {
            if (num2 <= 4)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos + offsetZ);
            }
            //else if(num2 <= 7)
            else if (num2 >= 5 && num2 <= 9)
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 5 && num1 <= 7)
        {
            if (num2 <= 5)
            {
                //を生成
                GameObject Rock = Instantiate(RockPrefab);
                Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 6 && num2 <= 9)
            {
                //を生成
                GameObject Tree = Instantiate(TreePrefab);
                Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 2)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 4 && num2 <= 5)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }

    }

    void StageB3()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 8)
        {
            if (num2 <= 4)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (5 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }

    }
    void StageB4()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 5)
        {
            if (num2 <= 4)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (5 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 2)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }

        }

    }
    void StageB5()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 5)
        {
            if (num2 <= 4)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (5 <= num2 && num2 <= 8)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (9 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 3)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }

    }

    void StageC1()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 2)
        {
            if (num2 <= 3)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (4 <= num2 && num2 <= 6)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (7 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 3 && num1 <= 4)
        {
            if (num2 <= 4)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos + offsetZ);
            }
            //else if(num2 <= 7)
            else if (num2 >= 5 && num2 <= 9)
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 5 && num1 <= 7)
        {
            if (num2 <= 5)
            {
                //を生成
                GameObject Rock = Instantiate(RockPrefab);
                Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 6 && num2 <= 9)
            {
                //を生成
                GameObject Tree = Instantiate(TreePrefab);
                Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 1)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 4 && num2 <= 5)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }


    }

    void StageC2()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 1)
        {
            if (num2 <= 3)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (4 <= num2 && num2 <= 6)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (7 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 2 && num1 <= 4)
        {
            if (num2 <= 4)
            {
                GameObject Barricade = Instantiate(BarricadePrefab);
                Barricade.transform.position = new Vector3(0, Barricade.transform.position.y, CreatePos + offsetZ);
            }
            //else if(num2 <= 7)
            else if (num2 >= 5 && num2 <= 9)
            {
                GameObject Fence = Instantiate(FencePrefab);
                Fence.transform.position = new Vector3(0, Fence.transform.position.y, CreatePos + offsetZ);
            }
        }
        else if (num1 >= 5 && num1 <= 7)
        {
            if (num2 <= 5)
            {
                //を生成
                GameObject Rock = Instantiate(RockPrefab);
                Rock.transform.position = new Vector3(PosRange * offsetX, Rock.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 6 && num2 <= 9)
            {
                //を生成
                GameObject Tree = Instantiate(TreePrefab);
                Tree.transform.position = new Vector3(PosRange * offsetX, Tree.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 1)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }
            else if (num2 >= 4 && num2 <= 5)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }

    }

    void StageC3()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 8)
        {
            if (num2 <= 3)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (4 <= num2 && num2 <= 6)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (7 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }

    }
    void StageC4()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 5)
        {
            if (num2 <= 3)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (4 <= num2 && num2 <= 6)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (7 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 1)
            {
                //を生成
                GameObject Crystal = Instantiate(CrystalPrefab);
                Crystal.transform.position = new Vector3(PosRange * offsetX, Crystal.transform.position.y, CreatePos + offsetZ);
            }

        }

    }
    void StageC5()
    {
        int num1 = Random.Range(1, 11);
        int num2 = Random.Range(1, 11);
        int offsetZ = Random.Range(-5, 6);
        int offsetX = Random.Range(-1, 2);

        if (num1 <= 5)
        {
            if (num2 <= 3)
            {
                GameObject Slime = Instantiate(SlimePrefab);
                Slime.transform.position = new Vector3(PosRange * offsetX, Slime.transform.position.y, CreatePos + offsetZ);
            }
            else if (4 <= num2 && num2 <= 6)
            {
                GameObject Turtle = Instantiate(TurtlePrefab);
                Turtle.transform.position = new Vector3(PosRange * offsetX, Turtle.transform.position.y, CreatePos + offsetZ);
            }
            else if (7 <= num2 && num2 <= 9)
            {
                GameObject Purple = Instantiate(PurplePrefab);
                Purple.transform.position = new Vector3(PosRange * offsetX, Purple.transform.position.y, CreatePos + offsetZ);
            }
        }
        else
        {
            if (num2 <= 3)
            {
                //を生成
                GameObject Flower = Instantiate(FlowerPrefab);
                Flower.transform.position = new Vector3(PosRange * offsetX, Flower.transform.position.y, CreatePos + offsetZ);
            }
        }

    }

}
