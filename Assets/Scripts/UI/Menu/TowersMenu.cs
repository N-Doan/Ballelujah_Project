using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowersMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject currTower;
    [SerializeField]
    private GameObject tower1;
    [SerializeField]
    private GameObject tower2;
    [SerializeField]
    private GameObject tower3;
    [SerializeField]
    private GridManager grid;
    // Start is called before the first frame update
    void Start()
    {
       
        grid.currTower = tower1;
        currTower = grid.currTower;
    }

    // Update is called once per frame
    void Update()
    {
        currTower = grid.currTower;
    }

    public void changeToBumper() {
        grid.currTower = tower1;
    }

    public void changeToPortal() {
        grid.currTower = tower2;
    }

    public void changeToTower() {
        grid.currTower = tower3;
    }
}
