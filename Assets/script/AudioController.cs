using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


    public AudioClip[] clips;
    AudioSource audios;
    public int BGMNo;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
        this.BGMNo = 0;
    }

    // Update is called once per frame
    void Update()
    {

        switch (BGMNo)
        {
            case 1:     //戦闘１
                audios.loop = true;
                audios.clip = clips[1];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 2:     //戦闘２
                audios.loop = true;
                audios.clip = clips[2];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 3:     //戦闘３
                audios.loop = true;
                audios.clip = clips[3];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 9:     //ボス
                audios.loop = true;
                audios.clip = clips[9];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 10:     //ラスボス
                audios.loop = true;
                audios.clip = clips[10];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 11:    //ゲームオーバー
                audios.loop = false;
                audios.clip = clips[11];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 12:    //ステージクリア
                audios.loop = false;
                audios.clip = clips[12];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            default:
                audios.Stop();
                break;

        }
    }

    /*
    public void AudioChange(int BGMNo)
    {
        switch (BGMNo)
        {
            case 1:     //戦闘１
                audios.loop = true;
                audios.clip = clips[1];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 2:     //戦闘２
                audios.loop = true;
                audios.clip = clips[2];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 3:     //戦闘３
                audios.loop = true;
                audios.clip = clips[3];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 9:     //ボス
                audios.loop = true;
                audios.clip = clips[9];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 10:     //ラスボス
                audios.loop = true;
                audios.clip = clips[10];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 11:    //ゲームオーバー
                audios.loop = false;
                audios.clip = clips[11];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 12:    //ステージクリア
                audios.loop = false;
                audios.clip = clips[11];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            default:
                audios.Stop();
                break;
        }
    }
    */

}
