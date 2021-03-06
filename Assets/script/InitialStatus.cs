using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player/InitialStatus")]
public class InitialStatus : ScriptableObject
{
    //前方向の速度
    public float velocityZ = 16f;
    //横方向の速度
    public float velocityX = 12f;
    //上方向の速度
    public float velocityY = 4f;
    //横方向の移動量
    public float setpositionX = 2f;
    //左右の移動できる範囲
    public float movableRange = 2f;
    //横方向の現在位置
    public float nowpositionX = 0f;
    //横方向の入力による速度
    public float inputVelocityX = 0;
    //横移動許可
    public bool movableX = true;
    //スライディングのパラメータ
    public float slidespan = 1.0f;
    public float slidedelta = 0;
    //攻撃のパラメータ
    //public bool isAttack = false;
    public float atkspan = 0.5f;
    public float atkdelta = 0;

    //動きを減速させる係数
    public float coefficient = 0.95f;

    //プレイヤーのHP
    public float HP = 1;
    public int Atk = 1;
    public float Spd = 1;

    public float InvincibleTime = 1.0f;

}
