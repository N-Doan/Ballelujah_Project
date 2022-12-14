using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows;
    [SerializeField]
    private int cols;
    [SerializeField]
    private float tileSize = 1;
    [SerializeField]
    private GameObject referenceTile;
    [SerializeField]
    public GameObject currTower;
    [SerializeField]
    private float towerRot = 0;
    private int selectedTowerWidth;
    private int selectedTowerLength;
    private GameObject[,] gridTiles;
    private List<int> currTileRows = new List<int>();
    private List<int> currTileCols = new List<int>();
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite redSprite;
    [SerializeField]
    private Sprite greenSprite;
    [SerializeField]
    private float gridRotation;
    private bool moveX = false;
    private bool moveZ = false;
    [SerializeField]
    private int noBuildLengthMax = 0;
    [SerializeField]
    private int noBuildLengthMin = 0;
    [SerializeField]
    private int noBuildWidthMin = 0;
    [SerializeField]
    private int noBuildWidthMax = 0;

    [SerializeField]
    private ResourceManager P1resourceManager;
    [SerializeField]
    private ResourceManager P2resourceManager;

    //just for the test
    [SerializeField]
    private GameObject testTower;
    private int portalTowerCount = 0;
    private int portalNum = 0;
    // Start is called before the first frame update
    void Awake()
    {
        gridTiles = new GameObject[rows,cols];
        generateGrid();
    }

    void Update()
    {
        if (Input.GetButtonDown("RotateTower"))
        {
            towerRot += 90;
            if (towerRot >= 360)
            {
                towerRot = 0;
            }
        }
    }
    private void generateGrid()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                gridTiles[row, col] = (GameObject)Instantiate(referenceTile, transform);

                if (row >= noBuildLengthMax || row <= noBuildLengthMin)
                {
                    gridTiles[row, col].GetComponent<Tile>().setNoBuildZone(true);
                    gridTiles[row, col].GetComponent<Tile>().setPermNoBuild(true);
                }
                else if (col <= noBuildWidthMin || col >= noBuildWidthMax)
                {
                    gridTiles[row, col].GetComponent<Tile>().setNoBuildZone(true);
                    gridTiles[row, col].GetComponent<Tile>().setPermNoBuild(true);
                }

                float posX = col * tileSize + this.transform.position.x;
                float posY = this.transform.position.y;
                float posZ = row * -tileSize + this.transform.position.z;

                gridTiles[row, col].transform.position = new Vector3(posX, posY, posZ);
                Tile tempTile = gridTiles[row, col].GetComponent<Tile>();
                gridTiles[row, col].GetComponent<Tile>().setGridManager(this);
                gridTiles[row, col].GetComponent<Tile>().setTileCol(col);
                gridTiles[row, col].GetComponent<Tile>().setTileRow(row);
            }
        }
        this.transform.eulerAngles = new Vector3(0, 0, gridRotation);
    }

    public bool placeCheck (int row, int col)
    {
        int rowStart;
        int rowEnd;
        int colStart;
        int colEnd;
        int numTileWidth = gridTiles.GetLength(1);
        int numTileLength = gridTiles.GetLength(0);
        bool ableToBuild = true;

        row++;
        col++;

        if (currTileRows != null)
        {
            changeTiles("Default");
        }

        currTileRows = new List<int>();
        currTileCols = new List<int>();

        if (towerRot == 0)
        {
            rowStart = row - selectedTowerLength;
            rowEnd = row;
            colStart = col - selectedTowerWidth;
            colEnd = col;
            if (selectedTowerLength%2 == 0)
            {
                moveZ = true;
            }
            else
            {
                moveZ = false;
            }
            if (selectedTowerWidth%2 == 0)
            {
                moveX = true;
            }
            else
            {
                moveX = false;
            }
        }
        else if (towerRot == 90)
        {
            col--;
            rowStart = row - selectedTowerWidth;
            rowEnd = row;
            colStart = col;
            colEnd = col + selectedTowerLength;
            if (selectedTowerLength % 2 == 0)
            {
                moveX = true;
            }
            else
            {
                moveX = false;
            }
            if (selectedTowerWidth % 2 == 0)
            {
                moveZ = true;
            }
            else
            {
                moveZ = false;
            }
        }
        else if (towerRot == 180)
        {
            row--;
            rowStart = row;
            rowEnd = rowStart + selectedTowerLength;
            colStart = col - selectedTowerWidth;
            colEnd = col;
            if (selectedTowerLength % 2 == 0)
            {
                moveZ = true;
            }
            else
            {
                moveZ = false;
            }
            if (selectedTowerWidth % 2 == 0)
            {
                moveX = true;
            }
            else
            {
                moveX = false;
            }
        }
        else
        {
            rowStart = row - selectedTowerWidth;
            rowEnd = row;
            colStart = col - selectedTowerLength;
            colEnd = col;
            if (selectedTowerLength % 2 == 0)
            {
                moveX = true;
            }
            else
            {
                moveX = false;
            }
            if (selectedTowerWidth % 2 == 0)
            {
                moveZ = true;
            }
            else
            {
                moveZ = false;
            }
        }


        for (int a = rowStart; a < rowEnd; a++)
        {
            for (int b = colStart; b < colEnd; b++)
            {
                if (a >= numTileLength || a < 0 || b >= numTileWidth || b < 0)
                {
                    ableToBuild = false;
                }
                else
                {
                    Tile tempTile = gridTiles[a, b].GetComponent<Tile>();
                    currTileRows.Add(tempTile.getTileRow());
                    currTileCols.Add(tempTile.getTileCol());
                    bool buildCheck = tempTile.getNoBuildZone();
                    if (buildCheck == true)
                    {
                        ableToBuild = false;
                    }
                }
            }
        }

        if (ableToBuild == true)
        {
            changeTiles("Green");
        }
        else
        {
            changeTiles("Red");
        }
        return ableToBuild;
    }

    private void changeTiles (string colour)
    {

        for (int a = 0; a < currTileRows.Count; a++)
        {
            for (int b = 0; b < currTileCols.Count; b++)
            {
                if (colour == "Default")
                {
                    gridTiles[currTileRows[a],currTileCols[b]].GetComponent<SpriteRenderer>().sprite = defaultSprite;
                }
                else if (colour == "Red")
                {
                    gridTiles[currTileRows[a], currTileCols[b]].GetComponent<SpriteRenderer>().sprite = redSprite;
                }
                else
                {
                    gridTiles[currTileRows[a], currTileCols[b]].GetComponent<SpriteRenderer>().sprite = greenSprite;
                }
            }
        }
    }

    private void createNoBuildZone()
    {

        for (int a = 0; a < currTileRows.Count; a++)
        {
            for (int b = 0; b < currTileCols.Count; b++)
            {
                if (a == 0 || b == 0)
                {
                    if (a == 0)
                    {
                        if (currTileRows[a] - 1 >= 0)
                        {
                            gridTiles[currTileRows[a] - 1, currTileCols[b]].GetComponent<Tile>().setNoBuildZone(true);
                        }
                    }
                    else if (b == 0)
                    {
                        if (currTileCols[b] - 1 >= 0)
                        {
                            gridTiles[currTileRows[a], currTileCols[b] - 1].GetComponent<Tile>().setNoBuildZone(true);
                        }
                    }
                }

                gridTiles[currTileRows[a], currTileCols[b]].GetComponent<Tile>().setNoBuildZone(false);

                if (a == selectedTowerWidth || b == selectedTowerLength)
                {
                    if (a == selectedTowerWidth)
                    {
                        if (currTileRows[a] + 2 < rows)
                        {
                            gridTiles[currTileRows[a] + 2, currTileCols[b]].GetComponent<Tile>().setNoBuildZone(true);
                        }
                    }
                    else if (b == selectedTowerLength)
                    {
                        if (currTileCols[b] + 1 < cols)
                        {
                            gridTiles[currTileRows[a], currTileCols[b] + 1].GetComponent<Tile>().setNoBuildZone(true);
                        }
                    }
                }
            }
        }
    }

    public void removeNoBuildZone(List<int> removeRowsList, List<int> removeColsList)
    {
        for (int a = 0; a < removeRowsList.Count; a++)
        {
            for (int b = 0; b < removeColsList.Count; b++)
            {
                if (a == 0 || b == 0)
                {
                    if (a == 0)
                    {
                        if (removeRowsList[a] - 1 >= 0)
                        {
                            if (gridTiles[removeRowsList[a] - 1, removeColsList[b]].GetComponent<Tile>().getPermNoBuild() == false)
                            {
                                gridTiles[removeRowsList[a] - 1, removeColsList[b]].GetComponent<Tile>().setNoBuildZone(false);
                            }
                        }
                    }
                    else if (b == 0)
                    {
                        if (currTileCols[b] - 1 >= 0)
                        {
                            if (gridTiles[removeRowsList[a], removeColsList[b] - 1].GetComponent<Tile>().getPermNoBuild() == false)
                            {
                                gridTiles[removeRowsList[a], removeColsList[b] - 1].GetComponent<Tile>().setNoBuildZone(false);
                            }
                        }
                    }
                }


                if (gridTiles[removeRowsList[a], removeColsList[b]].GetComponent<Tile>().GetComponent<Tile>().getPermNoBuild() == false)
                {
                    gridTiles[removeRowsList[a], removeColsList[b]].GetComponent<Tile>().setNoBuildZone(false);
                }

                if (a == selectedTowerWidth || b == selectedTowerLength)
                {
                    if (a == selectedTowerWidth)
                    {
                        if (removeRowsList[a] + 2 < rows)
                        {
                            if (gridTiles[removeRowsList[a] + 2, removeColsList[b]].GetComponent<Tile>().GetComponent<Tile>().getPermNoBuild() == false)
                            {
                                gridTiles[removeRowsList[a] + 2, removeColsList[b]].GetComponent<Tile>().setNoBuildZone(false);
                            }
                        }
                    }
                    else if (b == selectedTowerLength)
                    {
                        if (removeColsList[b] + 1 < cols)
                        {
                            if (gridTiles[removeRowsList[a], removeColsList[b] + 1].GetComponent<Tile>().GetComponent<Tile>().getPermNoBuild() == false)
                            {
                                gridTiles[removeRowsList[a], removeColsList[b] + 1].GetComponent<Tile>().setNoBuildZone(false);
                            }
                        }
                    }
                }
            }
        }
    }
    public void placeTower(Transform tilePos)
    {
        Vector3 towerPos;
        double centCalcRow = currTileRows.Count / 2;
        double centCalcCol = currTileCols.Count / 2;
        int centrePosRow = (int)Math.Ceiling(centCalcRow);
        int centrePosCol = (int)Math.Ceiling(centCalcCol);

        TowerBuildManager m = currTower.GetComponent<TowerBuildManager>();
        GivePointsOnCollision p = currTower.GetComponent<GivePointsOnCollision>();
        //Check that player has enough essence
        bool essenceCheck = false;
        if (tilePos.position.z >= -0.26 && P1resourceManager.getPlayerEssence() >= m.getCost())
        {
            p.playerResourceManager = P1resourceManager;
            P1resourceManager.subtractPlayerEssence(currTower.GetComponent<TowerBuildManager>().getCost());
            essenceCheck = true;
        }
        else if (tilePos.position.z < -0.26 && P2resourceManager.getPlayerEssence() >= m.getCost())
        {
            p.playerResourceManager = P2resourceManager;
            P2resourceManager.subtractPlayerEssence(currTower.GetComponent<TowerBuildManager>().getCost());
            essenceCheck = true;
        }

        if (essenceCheck)
        {
            GameObject tower = (GameObject)Instantiate(currTower, transform);
            tower.transform.eulerAngles = new Vector3(0, towerRot - 90, 0);

            if (towerRot == 90 || towerRot == 270)
            {
                towerPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol] + 1].transform.position;
            }
            else
            {
                towerPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol]].transform.position;
            }
            if (moveX == true)
            {
                Transform tempPos;
                if (towerRot == 90 || towerRot == 270)
                {
                    tempPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol] + 2].transform;
                }
                else
                {
                    tempPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol] - 1].transform;
                }
                float posX = towerPos.x + (towerPos.x - tempPos.position.x) / 2;
                towerPos = new Vector3(posX, towerPos.y, towerPos.z);
            }

            if (moveZ == true)
            {
                Transform tempPos = gridTiles[currTileRows[centrePosRow] + 1, currTileCols[centrePosCol] + 1].transform;
                float posZ = towerPos.z + (towerPos.z - tempPos.position.z) / 2;
                towerPos = new Vector3(towerPos.x, towerPos.y, posZ);
            }

            tower.transform.position = new Vector3(towerPos.x, towerPos.y, towerPos.z);
            createNoBuildZone();
        }
    }

    public void placeTestTower(Transform tilePos)
    {
        Debug.Log("place tower");
        Vector3 towerPos;
        double centCalcRow = currTileRows.Count / 2;
        double centCalcCol = currTileCols.Count / 2;
        int centrePosRow = (int)Math.Ceiling(centCalcRow);
        int centrePosCol = (int)Math.Ceiling(centCalcCol);

        TowerBuildManager m = testTower.GetComponent<TowerBuildManager>();
        GivePointsOnCollision p = testTower.GetComponent<GivePointsOnCollision>();
        
        //Check that player has enough essence
        bool essenceCheck = false;
        if (tilePos.position.z >= -0.26 && P1resourceManager.getPlayerEssence() >= m.getCost())
        {
            p.playerResourceManager = P1resourceManager;
            P1resourceManager.subtractPlayerEssence(m.getCost());
            essenceCheck = true;
        }
        else if (tilePos.position.z < -0.26 && P2resourceManager.getPlayerEssence() >= m.getCost())
        {
            p.playerResourceManager = P2resourceManager;
            P2resourceManager.subtractPlayerEssence(m.getCost());
            essenceCheck = true;
        }

        if (essenceCheck)
        {
            GameObject tower = (GameObject)Instantiate(testTower, transform);
            tower.transform.eulerAngles = new Vector3(0, towerRot - 90, 0);
            PortalTowerAI ai = tower.GetComponent<PortalTowerAI>();
            //if its odd tower = base
            portalTowerCount += 1;

            if (towerRot == 90 || towerRot == 270)
            {
                towerPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol] + 1].transform.position;
            }
            else
            {
                towerPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol]].transform.position;
            }
            if (moveX == true)
            {
                Transform tempPos = gridTiles[currTileRows[centrePosRow], currTileCols[centrePosCol] + 1].transform;
                float posX = towerPos.x + (towerPos.x - tempPos.position.x) / 2;
                towerPos = new Vector3(posX, towerPos.y, towerPos.z);
            }

            if (moveZ == true)
            {
                Transform tempPos = gridTiles[currTileRows[centrePosRow] + 1, currTileCols[centrePosCol] + 1].transform;
                float posZ = towerPos.z + (towerPos.z - tempPos.position.z) / 2;
                towerPos = new Vector3(towerPos.x, towerPos.y, posZ);
            }

            tower.transform.position = new Vector3(towerPos.x, towerPos.y, towerPos.z);
            createNoBuildZone();
        }
    }
}
