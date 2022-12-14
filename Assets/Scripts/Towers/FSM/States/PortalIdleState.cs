using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIdleState : FSMState
{
    private IdleStatePortalAIProperties aiProperties;
    private PortalTowerAI towerController;
    private bool ballEntered;

    public PortalIdleState(PortalTowerAI controller,IdleStatePortalAIProperties idleStateAIProperties, Transform trans)
    {
        stateID = fSMStateID.IdleState;
        aiProperties = idleStateAIProperties;
        towerController = controller;
    }


    public override void enterStateInit(Transform tower)
    {
        ballEntered = false;
    }

    public override void reason(Transform tower)
    {
        if(ballEntered)
        {
            towerController.performTransition(transition.BallInRange);
            return;
        }
    }

    public override void act(Transform tower)
    {

    }

    public void setBallEntered(bool ballEnter)
    {
        ballEntered = ballEnter;
    }
}
