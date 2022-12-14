using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperTowerEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    int instanceID;

    void Start()
    {
        instanceID = gameObject.GetInstanceID();
        EventManager.instance.EOnTowerCreated += bumperTowerBuilt;
        EventManager.instance.EOnTowerTriggered += bumperTowerActivated;
    }

    //build event
    private void bumperTowerBuilt(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("BUMPER TOWER BUILT");
        }
    }

    private void bumperTowerActivated(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("BUMPER TOWER ACTIVATED");
        }
    }
}
