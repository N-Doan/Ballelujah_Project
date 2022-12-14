using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private GridManager gridManager;
    private bool noBuildZone = false;
    private GameObject tower;
    private int tileRow;
    private int tileCol;
    private bool permNoBuild = false;
    private Vector3 initialPos;

    private void Awake()
    {
        initialPos = this.transform.position;
    }

    public void OnMouseOver()
    {
        gridManager.placeCheck(tileRow, tileCol);
        if (Input.GetButtonDown("Select"))
        {
            if (noBuildZone == false)
            {
                gridManager.placeTower(this.transform);
            }
        }

        //test for portal tower
        if (Input.GetButtonDown("Test"))
        {
            if (noBuildZone == false)
            {
                gridManager.placeTestTower(this.transform);
            }
        }
    }
    public void setGridManager (GridManager gridInput)
    {
        gridManager = gridInput;
    }
    public void setNoBuildZone (bool noBuild)
    {
        noBuildZone = noBuild;
    }

    public bool getNoBuildZone()
    {
        return noBuildZone;
    }
    public void setTileRow(int rowNum)
    {
        tileRow = rowNum;
    }

    public void setTileCol(int colNum)
    {
        tileCol = colNum;
    }

    public int getTileRow()
    {
        return tileRow;
    }

    public int getTileCol()
    {
        return tileCol;
    }
    public void setPermNoBuild(bool noBuild)
    {
        permNoBuild = noBuild;
    }
    public bool getPermNoBuild()
    {
        return permNoBuild;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            setNoBuildZone(true);
            setPermNoBuild(true);
        }
    }
    public void resetPos()
    {
        this.transform.position = initialPos;
    }
}
