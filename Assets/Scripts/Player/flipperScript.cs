using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipperScript : MonoBehaviour
{
    [SerializeField]
    private float restPos = 0f; // displacement of the resting position (always 0)

    [SerializeField]
    private float activePos = 41.046f; // displacement of the active position

    [SerializeField]
    private float hitStr = 10000f; //amount of force applied to flippers to make them move

    [SerializeField]
    private float damper = 150f; // damper applied to spring joint when mouse is clicked or un-clicked

    [SerializeField]
    private bool side; // which side the flipper is on (left = true, right = false)


    public bool isUp;
    HingeJoint h;
    JointSpring spring;
    void Start()
    {
        h = GetComponent<HingeJoint>();
        h.useSpring = true;

        spring = new JointSpring();
        spring.spring = hitStr;
        spring.damper = damper;

        isUp = false;
    }
    public void FlipperUp()
    {
        spring.targetPosition = activePos;
        h.spring = spring;
        h.useLimits = true;
        isUp = true;
    }
    public void FlipperDown()
    {
        spring.targetPosition = restPos;
        h.spring = spring;
        h.useLimits = true;
        isUp = false;
    }
   
    /*
    // Update is called once per frame
    void Update()
    {
        switch(side){
            case true:
                if (Input.GetMouseButton(0))
                {
                    spring.targetPosition = activePos;
                }
                else
                {
                    spring.targetPosition = restPos;
                }
                h.spring = spring;
                h.useLimits = true;
                break;
            case false:
                if (Input.GetMouseButton(1))
                {
                    spring.targetPosition = activePos;
                }
                else
                {
                    spring.targetPosition = restPos;
                }
                h.spring = spring;
                h.useLimits = true;
                break;
            }
    }*/
}
