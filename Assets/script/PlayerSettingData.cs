using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingData
{
    //public InitialStatus initialStatus;

    //前方向の速度
    public float velocityZ;
    //横方向の速度
    public float velocityX;
    //上方向の速度
    public float velocityY;
    //横方向の移動量
    public float setpositionX;
    //左右の移動できる範囲
    public float movableRange;
    //横方向の現在位置
    public float nowpositionX;
    //横方向の入力による速度
    public float inputVelocityX;
    //横移動許可
    public bool movableX;
    //スライディングのパラメータ
    public float slidespan;
    public float slidedelta;
    //攻撃のパラメータ
    public float atkspan;
    public float atkdelta;

    //動きを減速させる係数
    public float coefficient;

    //プレイヤーのHP
    public float PlayerHP;
    public int PlayerAtk;
    public float PlayerSpd;

    public PlayerSettingData(InitialStatus initialStatus)
    {
        //前方向の速度
        velocityZ = initialStatus.velocityZ;
        //横方向の速度
        velocityX = initialStatus.velocityX;
        //上方向の速度
        velocityY = initialStatus.velocityY;
        //横方向の移動量
        setpositionX = initialStatus.setpositionX;
        //左右の移動できる範囲
        movableRange = initialStatus.movableRange;
        //横方向の現在位置
        nowpositionX = initialStatus.nowpositionX;
        inputVelocityX = initialStatus.inputVelocityX;
        //横移動許可
        movableX = initialStatus.movableX;
        //スライディングのパラメータ
        slidespan = initialStatus.slidespan;
        slidedelta = initialStatus.slidedelta;
        //攻撃のパラメータ
        atkspan = initialStatus.atkspan;
        atkdelta = initialStatus.atkdelta;

        //動きを減速させる係数
        coefficient = initialStatus.coefficient;

        //プレイヤーのHP
        PlayerHP = initialStatus.HP;
        PlayerAtk = initialStatus.Atk;
        PlayerSpd = initialStatus.Spd;
    }

    /*
    //前方向の速度
    velocityZ = initialStatus.velocityZ;
    //横方向の速度
    velocityX = initialStatus.velocityX;
    //上方向の速度
    velocityY = initialStatus.velocityY;
    //横方向の移動量
    setpositionX = initialStatus.setpositionX;
    //左右の移動できる範囲
    movableRange = initialStatus.movableRange;
    //横方向の現在位置
    nowpositionX = initialStatus.nowpositionX;
    inputVelocityX = initialStatus.inputVelocityX;
    //横移動許可
    movableX = initialStatus.movableX;
    //スライディングのパラメータ
    slidespan = initialStatus.slidespan;
    slidedelta = initialStatus.slidedelta;
    //攻撃のパラメータ
    atkspan = initialStatus.atkspan;
    atkdelta = initialStatus.atkdelta;

    //動きを減速させる係数
    coefficient = initialStatus.coefficient;

    //プレイヤーのHP
    HP = initialStatus.HP;
    Atk = initialStatus.Atk;
    Spd = initialStatus.Spd;
    */

 }


