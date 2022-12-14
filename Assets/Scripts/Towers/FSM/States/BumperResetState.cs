using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperResetState : FSMState
{
    //Declares local variables
    private ResetStateAIProperties aIProperties;
    private BumperTowerAI towerController;
    private float resetTimer;
    private float resetLimit;
    public BumperResetState (BumperTowerAI controller, ResetStateAIProperties resetStateAIProperties, Transform trans)
    {
        //Sets local variables
        stateID = fSMStateID.ResetState;
        aIProperties = resetStateAIProperties;
        towerController = controller;
        resetLimit = resetStateAIProperties.resetTime;
    }

    public override void enterStateInit(Transform tower)
    {
        //Resets timer on start
        resetTimer = 0.0f;
    }

    public override void reason(Transform tower)
    {
        //Switches to idle when the reset timer is hit
        if (resetTimer >= resetLimit)
        {
            towerController.performTransition(transition.ResetComplete);
        }
    }

    public override void act (Transform tower)
    {
        //Counts up the reset timer
        resetTimer += Time.deltaTime;
    }
}
