using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetting
{
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Atk { get; set; }
    public float Spd { get; set; }
    public int Exp { get; set; }

    //移動させるコンポーネントを入れる
    public Rigidbody myRigidbody;

    /*
    public Enemy(string name, int hp, int atk, int exp)
    {
        this.Name = name;
        this.Hp = hp;
        this.Atk = atk;
        this.Exp = exp;
    }
    */

    public virtual void Initialize()
    {
        
    }

    public virtual void OnUpdate()
    {
        
    }
}

public class SlimeSetting : EnemySetting
{
    public override void Initialize()
    {
        Name = "Slime";
        Hp = 1;
        Atk = 1;
        Spd = 1;
        Exp = 100;
        Debug.Log("Slime");
    }

    public override void OnUpdate()
    {
    
    }

}

public class TurtleSetting : EnemySetting
{
    public override void Initialize()
    {
        Name = "Turtle";
        Hp = 2;
        Atk = 2;
        Spd = 1;
        Exp = 150;
    }

    public override void OnUpdate()
    {

    }

}

public class PurpleSetting : EnemySetting
{
    public override void Initialize()
    {
        Name = "Purple";
        Hp = 1;
        Atk = 1;
        Spd = 1;
        Exp = 200;
    }

    public override void OnUpdate()
    {

    }

}