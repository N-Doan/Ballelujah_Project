using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdleStatePortalAIProperties : MonoBehaviour
{
    public bool ballEntered = false;
}
[System.Serializable]
public class ActivateStatePortalAIProperties : MonoBehaviour
{
    public GameObject pinball;
    public float force = 2000;
}

[System.Serializable]
public class ResetStatePortalAIProperties : MonoBehaviour
{
    public float resetTime = 2.5f;
}

public class PortalTowerAI : AdvancedFSM
{
    private IdleStatePortalAIProperties idleStateAIProperties = new IdleStatePortalAIProperties();
    private ActivateStatePortalAIProperties activateStateAIProperties = new ActivateStatePortalAIProperties();
    private ResetStatePortalAIProperties resetStateAIProperties = new ResetStatePortalAIProperties();

    private PortalIdleState portalIdleState;
    private PortalActivateState protalActivateState;
    private PortalResetState portalResetState;

    [SerializeField]
    public GameObject portalUpgradedPullZone;
    public PortalTowerUpgrader upgrader;


    private string getStateString()
    {
        string state = "NONE";
        if (CurrentState.ID == fSMStateID.IdleState)
        {
            state = "IDLESTATE";
        }
        else if (CurrentState.ID == fSMStateID.ActivateState)
        {
            state = "ACTIVATESTATE";
        }
        else if (CurrentState.ID == fSMStateID.ResetState)
        {
            state = "RESETSTATE";
        }
        return state;
    }

    private void constructFSM()
    {
        portalIdleState = new PortalIdleState(this, idleStateAIProperties, transform);
        portalIdleState.addTransition(transition.BallInRange, fSMStateID.ActivateState);

        protalActivateState = new PortalActivateState(this, activateStateAIProperties, transform);
        protalActivateState.addTransition(transition.ActivateComplete, fSMStateID.ResetState);

        portalResetState = new PortalResetState(this, resetStateAIProperties, transform);
        portalResetState.addTransition(transition.ResetComplete, fSMStateID.IdleState);

        upgrader = GetComponent<PortalTowerUpgrader>();


        addFSMState(portalIdleState);
        addFSMState(protalActivateState);
        addFSMState(portalResetState);
    }

    public float getCooldownTime()
    {
        return resetStateAIProperties.resetTime;
    }
    public void setCooldownTime(float f)
    {
        resetStateAIProperties.resetTime = f;
    }

    protected override void initialize()
    {
        constructFSM();
    }

    protected override void fSMUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.reason(transform);
            CurrentState.act(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pinball" && gameObject.GetComponent<PortalTowerAI>().enabled)
        {
            portalIdleState.setBallEntered(true);

            protalActivateState.setForce(10);
            protalActivateState.setPinball(other.gameObject);
            if (upgrader.isFullyUpgraded())
            {
                portalUpgradedPullZone.SetActive(false);
            }
            //pinbal speed
            //protalActivateState.setForce(other.)
        }
    }
}

