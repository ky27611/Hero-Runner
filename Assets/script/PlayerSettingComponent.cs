using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettingComponent
{
    //アニメーションするためのコンポーネントを入れる
    public Animator myAnimator;
    //CapsuleColiderコンポーネントを入れる
    public CapsuleCollider myCollider;
    //移動させるコンポーネントを入れる
    public Rigidbody myRigidbody;

    public BoxCollider atkCollider;

    public AudioSource myAudio;

    //public GameObject ShockWavePrefab;

}