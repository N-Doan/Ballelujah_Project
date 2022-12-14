using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    int instanceID;

    void Start()
    {
        instanceID = gameObject.GetInstanceID();
        EventManager.instance.EOnTowerCreated += cannonTowerBuilt;
        EventManager.instance.EOnTowerTriggered += cannonTowerActivated;
    }

    //build event
    private void cannonTowerBuilt(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("CANNON TOWER BUILT");
        }
    }

    private void cannonTowerActivated(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("CANNON TOWER ACTIVATED");
        }
    }
}
