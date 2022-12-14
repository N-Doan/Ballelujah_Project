using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClicked : MonoBehaviour
{
    [SerializeField]
    GameObject bumperTower;
    [SerializeField]
    GameObject windTower;
    [SerializeField]
    GameObject portalTower;
    [SerializeField]
    GameObject cannonTower;


    [SerializeField]
    PlayerController p1Control;
    [SerializeField]
    PlayerController p2Control;


    public void bumpTowerp1()
    {
        p1Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(bumperTower);
    }

    public void portalTowerp1()
    {
        p1Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(portalTower);
    }

    public void windTowerp1()
    {
        p1Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(windTower);
    }

    public void cannonTowerp1()
    {
        p1Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(cannonTower);
    }

    public void bumpTowerp2()
    {
        p2Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(bumperTower);
    }

    public void portalTowerp2()
    {
        p2Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(portalTower);
    }

    public void windTowerp2()
    {
        p2Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(windTower);
    }

    public void cannonTowerp2()
    {
        p2Control.GetTowerPosition().GetComponent<TowerBuildSpot>().setupTower(cannonTower);
    }
}
