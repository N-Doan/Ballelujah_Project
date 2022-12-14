using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTowerEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    int instanceID;

    void Start()
    {
        instanceID = gameObject.GetInstanceID();
        EventManager.instance.EOnTowerCreated += portalTowerBuilt;
        EventManager.instance.EOnTowerTriggered += portalTowerActivated;
    }

    //build event
    private void portalTowerBuilt(int id)
    {
        if (id == instanceID)
        {
            
        }
    }

    private void portalTowerActivated(int id)
    {
        if (id == instanceID)
        {
          
        }
    }
}