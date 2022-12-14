using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PortalActivateState : FSMState
{
    private ActivateStatePortalAIProperties aiProperties;
    private PortalTowerAI towerController;
    private bool activateComp;
    private float force;
    private GameObject pinball;

    private Transform trans;
    public PortalActivateState(PortalTowerAI controller, ActivateStatePortalAIProperties activateStateAIProperties, Transform trans)
    {
        stateID = fSMStateID.ActivateState;
        aiProperties = activateStateAIProperties;
        towerController = controller;
    }

    public override void enterStateInit(Transform tower)
    {
        activateComp = false;
        //firing activated event
        EventManager.instance.OnTowerTriggerEnter(towerController.gameObject.GetInstanceID());
    }

    public override void reason(Transform tower)
    {
        if (activateComp == true)
        {
            towerController.performTransition(transition.ActivateComplete);
            return;
        }
    }

    public override void act(Transform tower)
    {
        if (!activateComp)
        {
                Vector3 dir = pinball.transform.position;
                dir = dir.normalized;
                if (tower.GetComponentInParent<TowerBuildSpot>().getSide() == true)
                {
                    pinball.transform.position = new Vector3(0.896f, -0.029f, 0.121f + Random.Range(-0.1449f, 0.1149f));
                }
                else
                {
                    pinball.transform.position = new Vector3(0.896f, -0.029f, -0.54f + Random.Range(-0.1449f, 0.1149f));
                }

                Rigidbody rb = pinball.GetComponent<Rigidbody>();
                rb.velocity = new Vector3(0, 0, 0);

            //Transform targetPosition = tower.gameObject.GetComponent<PortalTowerUpgrader>().activeAdjacentSpot[Random.Range(0, 4)];
            Vector3 direction = pinball.transform.position;
                direction.y = 0;
                direction.x = 0;
                rb.AddForce(direction * 5);

            VisualEffect[] VEArr = towerController.GetComponentsInChildren<VisualEffect>();
            foreach (VisualEffect v in VEArr)
            {
                if (v.gameObject.name.Equals("Portal Tower VFX"))
                {
                    v.enabled = false;
                    v.Stop();
                }
            }

            activateComp = true;
        }
    }

    public void setForce(int force)
    {
        this.force = force;
    }

    public void setPinball(GameObject pinballHit)
    {
        pinball = pinballHit;
    }
}
