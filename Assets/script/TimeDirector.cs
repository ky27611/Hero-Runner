﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDirector : MonoBehaviour
{
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.time += Time.deltaTime;
    }
}
