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

    }

    
    public void AudioChange(int BGMNo)
    {
        switch (BGMNo)
        {
            case 1:     //戦闘１
                audios.Stop();
                audios.loop = true;
                audios.clip = clips[1];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 2:     //戦闘２
                audios.Stop();
                audios.loop = true;
                audios.clip = clips[2];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 3:     //戦闘３
                audios.Stop();
                audios.loop = true;
                audios.clip = clips[3];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 9:     //ボス
                audios.Stop();
                audios.loop = true;
                audios.clip = clips[9];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 10:     //ラスボス
                audios.Stop();
                audios.loop = true;
                audios.clip = clips[10];
                if (audios.isPlaying == false)
                {
                    audios.Play();
                }
                break;
            case 11:    //ゲームオーバー
                audios.Stop();
                audios.loop = false;
                audios.clip = clips[11];
                audios.PlayOneShot(audios.clip);
                break;
            case 12:    //ステージクリア
                audios.Stop();
                audios.loop = false;
                audios.clip = clips[12];
                audios.PlayOneShot(audios.clip);
                break;
            default:
                audios.Stop();
                break;
        }
    }
}
