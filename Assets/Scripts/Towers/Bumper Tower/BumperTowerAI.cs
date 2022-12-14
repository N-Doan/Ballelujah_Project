using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Declares ai state properties to be inherited by the states
[System.Serializable]
public class IdleStateAIProperties : MonoBehaviour
{
    public bool ballEntered = false;
}
[System.Serializable]
public class ActivateStateAIProperties : MonoBehaviour
{
    public GameObject pinball;
    public float force = 7;
}

[System.Serializable]
public class ResetStateAIProperties : MonoBehaviour
{
    public float resetTime = 0.0f;
}

public class BumperTowerAI : AdvancedFSM
{
    //Declares states
    private IdleStateAIProperties idleStateAIProperties = new IdleStateAIProperties();
    private ActivateStateAIProperties activateStateAIProperties = new ActivateStateAIProperties();
    private ResetStateAIProperties resetStateAIProperties = new ResetStateAIProperties();
    //Used for access to state methods that need to be called in this class
    private BumperIdleState bumperIdleState;
    private BumperActivateState bumperActivateState;

    private Animator animator;

    public ActivateStateAIProperties getActivateState()
    {
        return activateStateAIProperties;
    }

    public void setForce(float value)
    {
        activateStateAIProperties.force = value;
    }
    //Returns the current state in string value
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

    //Sets up the fsm for this ai
    private void constructFSM()
    {
        bumperIdleState = new BumperIdleState(this, idleStateAIProperties, transform);
        bumperIdleState.addTransition(transition.BallInRange, fSMStateID.ActivateState);

        bumperActivateState = new BumperActivateState(this, activateStateAIProperties, transform);
        bumperActivateState.addTransition(transition.ActivateComplete, fSMStateID.ResetState);

        BumperResetState bumperResetState = new BumperResetState(this, resetStateAIProperties, transform);
        bumperResetState.addTransition(transition.ResetComplete, fSMStateID.IdleState);

        addFSMState(bumperIdleState);
        addFSMState(bumperActivateState);
        addFSMState(bumperResetState);
    }
    //Called on start to make fsm
    protected override void initialize()
    {
        constructFSM();
        animator = GetComponent<Animator>();
    }

    //Called on update to constantly run through the current state's reason and act states
    protected override void fSMUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.reason(transform);
            CurrentState.act(transform);
        }
    }

    //Checks if there is a pinball collision then updates the idle and activate states so they receive the information about the contacted pinball that they need
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pinball" && GetComponent<BumperTowerAI>().enabled)
        {
            bumperIdleState.setBallEntered(true);
            bumperActivateState.setPinball(collision.gameObject, collision);

            if (collision.gameObject.tag == "Pinball")
            {
                Vector3 norm = collision.contacts[0].normal;
                Vector3 dir = transform.position - norm;

                dir = new Vector3(dir.x + Random.Range(-0.1f, 0.1f), dir.y + Random.Range(-0.1f, 0.1f), dir.z + Random.Range(-0.1f, 0.1f));

                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * activateStateAIProperties.force);

                animator.SetTrigger("Bump");

                FMODUnity.RuntimeManager.PlayOneShot("event:/BumperTower");
            }
        }
    }
}
