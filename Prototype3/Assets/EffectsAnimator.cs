﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StopEffects()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Animator>().enabled = false;
    }
}
