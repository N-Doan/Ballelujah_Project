using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperActivateState : FSMState
{
    //Initializes local variables
    private ActivateStateAIProperties aIProperties;
    private BumperTowerAI towerController;
    private bool activateComp;
    private float force;
    private GameObject pinball;
    private Collision pballCol;

    public BumperActivateState (BumperTowerAI controller, ActivateStateAIProperties activateStateAIProperties, Transform trans)
    {
        //Sets local variable
        stateID = fSMStateID.ActivateState;
        aIProperties = activateStateAIProperties;
        towerController = controller;
    }

    public override void enterStateInit(Transform tower)
    {
        //Sets initial values on start
        activateComp = false;
        force = aIProperties.force;
        //triggering event
        EventManager.instance.OnTowerTriggerEnter(towerController.gameObject.GetInstanceID());
    }

    public override void reason(Transform tower)
    {
        //Switches back to idle state once the force has been applied to the pinball
        if (activateComp == true)
        {
            towerController.performTransition(transition.ActivateComplete);
            return;
        }
    }

    public override void act (Transform tower)
    {
        if (activateComp == false)
        {
            //Sends a force to the pinball in the opposite direction that it hit the tower
            activateComp = true;

            if (pballCol.gameObject.tag == "Pinball")
            {
                Debug.Log("HITHITHIHT");
                Vector3 norm = pballCol.contacts[0].normal;
                Vector3 dir = tower.position - norm;

                dir = new Vector3(dir.x + Random.Range(-0.1f, 0.1f), dir.y + Random.Range(-0.1f, 0.1f), dir.z + Random.Range(-0.1f, 0.1f));

                pballCol.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                pballCol.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                pballCol.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);

                FMODUnity.RuntimeManager.PlayOneShot("event:/BumperTower");
            }
        }
    }

    //Method used to update the pinball gameobject from within the BumperTowerAI class
    public void setPinball (GameObject pinballHit, Collision ballCol)
    {
        pinball = pinballHit;
        pballCol = ballCol;
    }
}
