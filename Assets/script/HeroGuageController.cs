using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroGuageController : MonoBehaviour
{
    private GameObject director;
    private GameObject GaugeFew;
    private GameObject GaugeMax;
    private GameObject GaugeFrame;
    private float Ratio;
    private float Flash;
    private bool isGaugeMax;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        this.director = GameObject.Find("GameDirector");
        this.GaugeFew = GameObject.Find("HeroGaugeFew");
        this.GaugeMax = GameObject.Find("HeroGaugeMAX");
        this.GaugeFrame = GameObject.Find("HeroGaugeFrame");
        this.Flash = 0;
    }

    // Update is called once per frame
    void Update()
    {
        this.Ratio = this.director.GetComponent<GameDirector>().HeroPointRatio;

        this.GaugeFew.GetComponent<Image>().fillAmount = this.Ratio;
        this.GaugeMax.GetComponent<Image>().fillAmount = this.Flash;

        this.time += Time.deltaTime;

        if (this.Ratio == 1)
        {
            this.time += Time.deltaTime;
            if (this.time <= 0.2)
            {
                this.Flash = 1;
            }
            else
            {
                this.Flash = 0;
                if (this.time >= 0.4)
                {
                   this.time = 0;
                }
            }
        }
        else
        {
            this.Flash = 0;
            this.time = 0;
        }
            
    }
}
