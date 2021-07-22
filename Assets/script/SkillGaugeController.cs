using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillGaugeController : MonoBehaviour
{
    private GameObject Player;
    private float SkillTime;
    private float SkillActivationTime;
    private float SkillWaitTime;
    private float SkillRecoveryTime;

    // Start is called before the first frame update
    void Start()
    {
        this.Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.SkillTime = this.Player.GetComponent<PlayerController>().Setting.SkillTime;
        this.SkillActivationTime = this.Player.GetComponent<PlayerController>().Setting.SkillActivationTime;
        this.SkillWaitTime = this.Player.GetComponent<PlayerController>().Setting.SkillWaitTime;
        this.SkillRecoveryTime = this.Player.GetComponent<PlayerController>().Setting.SkillRecoveryTime;

        if (this.SkillTime == 0)
        {
            this.SkillTime = 1;
        }

        if (this.SkillWaitTime == 0)
        {
            this.SkillWaitTime = 1;
        }

        if (this.Player.GetComponent<PlayerController>().Setting.isSkillActivation)
        {
            float ratio1 = this.SkillActivationTime / this.SkillTime;
            this.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
            this.GetComponent<Image>().fillAmount = 1 - (ratio1);
        }
        else
        {
            float ratio2 = this.SkillRecoveryTime / this.SkillWaitTime;
            if (this.Player.GetComponent<PlayerController>().Setting.isEnableSkill == false)
            {
                this.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f);
                this.GetComponent<Image>().fillAmount = ratio2;
            }
            else
            {
                this.GetComponent<Image>().color = new Color(1f, 1f, 1f);
                this.GetComponent<Image>().fillAmount = 1;
            }
            
        }
    }
}
