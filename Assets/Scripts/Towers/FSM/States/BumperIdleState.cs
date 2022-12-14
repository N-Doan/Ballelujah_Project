using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperIdleState : FSMState
{
    //Initializes local variables
    private IdleStateAIProperties aIProperties;
    private BumperTowerAI towerController;
    private bool ballEntered;
    public BumperIdleState (BumperTowerAI controller, IdleStateAIProperties idleStateAIProperties, Transform trans)
    {
        //Sets local variable
        stateID = fSMStateID.IdleState;
        aIProperties = idleStateAIProperties;
        towerController = controller;
    }

    public override void enterStateInit (Transform tower)
    {
        //Defaults ball entered to false
        ballEntered = false;
    }

    public override void reason (Transform tower)
    {
        //Switches to active state when ball entered is equal to true
        if (ballEntered == true)
        {
            //Debug.Log("BALL ENTERED");
            towerController.performTransition(transition.BallInRange);
            return;
        }
    }

    public override void act (Transform tower)
    {

    }

    //Method used to switch ball entered value from within the BumperTowerAI class
    public void setBallEntered (bool ballEnter)
    {
        ballEntered = ballEnter;
    }
}
