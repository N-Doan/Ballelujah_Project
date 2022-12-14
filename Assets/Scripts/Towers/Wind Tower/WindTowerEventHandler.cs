using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTowerEventHandler : MonoBehaviour
{
    // Start is called before the first frame update
    int instanceID;

    void Start()
    {
        instanceID = gameObject.GetInstanceID();
        EventManager.instance.EOnTowerCreated += windTowerBuilt;
        EventManager.instance.EOnTowerTriggered += ballInWindBox;
    }

    //build event
    private void windTowerBuilt(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("WIND TOWER BUILT");
        }
    }

    //Triggers every frame a pinball is inside the active zone of the windbox (put particles and sounds in here)
    private void ballInWindBox(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("BALL HAS ENTERED WIND ZONE");
            //GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
