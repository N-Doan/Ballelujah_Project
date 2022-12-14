using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Reflection;
using System;


public class AOSceneLoad : MonoBehaviour
{
    public float radius;
    public float directStrength;

    // Start is called before the first frame update
    void Start()
    {
        SSAOConfigurator ssaoConfigurator = new SSAOConfigurator();
        ssaoConfigurator.radius = radius;
        ssaoConfigurator.directStrength = directStrength;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
