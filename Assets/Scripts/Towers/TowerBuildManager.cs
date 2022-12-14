using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildManager : MonoBehaviour
{
    [SerializeField]
    private int cost;

    private TowerBuildSpot buildZone;
    [SerializeField]
    Material transparentMat;
    [SerializeField]
    Material solidMat;

    //Runs when script is enabled
    public void Awake()
    {
        //Sets the defualt material to transparent on awake
        this.gameObject.GetComponentInChildren<Renderer>().material = transparentMat;
    }

    //Runs when mouse hovers over object
    public void OnMouseOver()
    {
        /*
        if (Input.GetButtonDown("Delete"))
        {
            //Destroys this tower object and resets the build zone its on to its default state
            buildZone.resetTowerPlaced();
            Destroy(this.gameObject);
        }*/
    }

    public int getCost()
    {
        return cost;
    }

    //Called when tower is placed to track the build zone that the tower has been placed on and to change its material to a solid material
    public void setTowerBuildSpot(TowerBuildSpot currBuildZone)
    {
        buildZone = currBuildZone;
        this.gameObject.GetComponentInChildren<Renderer>().material = solidMat;
    }
}
