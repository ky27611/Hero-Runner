using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{


    public AudioClip[] clips;
    AudioSource audios;

    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void AudioChange(int BGMNo)
    {
        switch (BGMNo)
        {
            case 1:
                audios.loop = true;
                audios.clip = clips[1];
                audios.Play();
                break;
            case 2:
                audios.loop = true;
                audios.clip = clips[2];
                audios.Play();
                break;
            case 11:
                audios.loop = false;
                audios.clip = clips[11];
                audios.Play();
                break;
            case 12:
                audios.loop = false;
                audios.clip = clips[11];
                audios.Play();
                break;
        }
    }

}
