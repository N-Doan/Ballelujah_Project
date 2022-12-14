using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PortalResetState : FSMState
{
    private ResetStatePortalAIProperties aiProperties;
    private PortalTowerAI towerController;
    private float resetTimer;
    private float resetLimit;

    public PortalResetState(PortalTowerAI controller, ResetStatePortalAIProperties resetStateAIProperties, Transform trans)
    {
        stateID = fSMStateID.ResetState;
        aiProperties = resetStateAIProperties;
        towerController = controller;
        resetLimit = resetStateAIProperties.resetTime;
    }

    public override void enterStateInit(Transform tower)
    {
        resetTimer = 0.0f;
        resetLimit = aiProperties.resetTime;
    }

    public override void reason(Transform tower)
    {
        if(resetTimer>=resetLimit)
        {
            VisualEffect[] VEArr = towerController.GetComponentsInChildren<VisualEffect>();
            foreach(VisualEffect v in VEArr)
            {
                if(v.gameObject.name.Equals("Portal Tower VFX"))
                {
                    v.enabled = true;
                    v.Play();
                    v.Reinit();
                }
            }
            if (towerController.upgrader.isFullyUpgraded())
            {
                towerController.portalUpgradedPullZone.SetActive(true);
            }
            towerController.performTransition(transition.ResetComplete);
        }
    }

    public override void act(Transform tower)
    {
        resetTimer += Time.deltaTime;
    }
}
