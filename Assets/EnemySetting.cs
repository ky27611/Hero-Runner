using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Setting")]
public class EnemySetting : ScriptableObject {
    
    public enum EnemyType {
        Slime,
        Turtle,
        Purple
    }

    public EnemyType Type;
    public string Name;
    public int Hp;
    public int Atk;
    public float Spd;
    public int Exp;

    //移動させるコンポーネントを入れる
    public Rigidbody myRigidbody; // ここはそのままだとダメです！（アセットはシーンの情報を持って来れないので・・・！）

    /*
    public Enemy(string name, int hp, int atk, int exp)
    {
        this.Name = name;
        this.Hp = hp;
        this.Atk = atk;
        this.Exp = exp;
    }
    */
}

// public class SlimeSetting : EnemySetting
// {
//     public override void Initialize()
//     {
//         Name = "Slime";
//         Hp = 1;
//         Atk = 1;
//         Spd = 1;
//         Exp = 100;
//         Debug.Log("Slime");
//     }
//
//     public override void OnUpdate()
//     {
//     
//     }
//
// }
//
// public class TurtleSetting : EnemySetting
// {
//     public override void Initialize()
//     {
//         Name = "Turtle";
//         Hp = 2;
//         Atk = 2;
//         Spd = 1;
//         Exp = 150;
//     }
//
//     public override void OnUpdate()
//     {
//
//     }
//
// }
//
// public class PurpleSetting : EnemySetting
// {
//     public override void Initialize()
//     {
//         Name = "Purple";
//         Hp = 1;
//         Atk = 1;
//         Spd = 1;
//         Exp = 200;
//     }
//
//     public override void OnUpdate()
//     {
//
//     }
//
// }