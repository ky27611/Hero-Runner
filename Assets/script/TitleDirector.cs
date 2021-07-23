using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleDirector : MonoBehaviour
{
    private GameObject Story1;
    private GameObject Story2;
    private float waittime;
    private bool isGameStart;
    private bool isMethod;
    private bool isSe;
    public AudioClip[] clips;
    AudioSource audios;
    public int BGMNo;
    public int ModeNo;

    // Start is called before the first frame update
    void Start()
    {
        this.Story1 = GameObject.Find("Story1");
        this.Story2 = GameObject.Find("Story2");
        audios = GetComponent<AudioSource>();
        this.BGMNo = 0;
        this.waittime = 0;
        this.ModeNo = 1;
        this.isGameStart = false;
        this.isMethod = false;
        this.isSe = true;

        audios.loop = true;
        audios.clip = clips[1];
        audios.Play();

        this.Story1.gameObject.SetActive(false);
        this.Story2.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        switch (ModeNo)
        {
            case 1:
                if (isGameStart)
                {
                    this.waittime += Time.deltaTime;
                    if (this.waittime >= 2)
                    {
                        //SceneManager.LoadScene("Stage0Scene");
                        this.waittime = 0;
                        audios.Stop();
                        audios.clip = clips[3];
                        audios.Play();
                        ModeNo = 2;
                    }
                }

                if (isMethod)
                {

                }
                break;
            case 2:
                this.waittime += Time.deltaTime;
                this.Story1.gameObject.SetActive(true);

                if (audios.isPlaying == false)
                {
                    audios.clip = clips[3];
                    audios.Play();
                }

                if (Input.GetMouseButtonDown(0))
                {
                    this.waittime = 5;
                }

                if (this.waittime >= 5 )
                {
                    this.Story1.gameObject.SetActive(false);
                    ModeNo = 3;
                    this.waittime = 0;
                }

                break;
            case 3:
                this.waittime += Time.deltaTime;
                this.Story2.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    this.waittime = 5;
                }

                if (this.waittime >= 5 && this.waittime < 7)
                {
                    if (isSe)
                    {
                        this.isSe = false;
                        audios.PlayOneShot(clips[2]);
                    }
                }
                else if (this.waittime >= 7)
                {
                    SceneManager.LoadScene("Stage0Scene");
                    this.waittime = 0;
                    this.isSe = false;
                }
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;

        }

    }

    public void GameStart()
    {
        this.isGameStart = true;
        audios.PlayOneShot(clips[2]);
    }
}
