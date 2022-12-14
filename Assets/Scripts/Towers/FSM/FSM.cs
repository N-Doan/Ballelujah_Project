using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    //Player Transform
    protected Transform playerTransform;

    //Next destination position
    protected Vector3 destPos;

    //List of points for patrolling
    protected GameObject[] pointList;

    protected virtual void initialize() { }
    protected virtual void fSMUpdate() { }
    protected virtual void fSMFixedUpdate() { }

    // Use this for initialization
    void Start()
    {
        initialize();
    }

    // Update is called once per frame
    void Update()
    {
        fSMUpdate();
    }

    void FixedUpdate()
    {
        fSMFixedUpdate();
    }
}
