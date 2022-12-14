using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildSpot : MonoBehaviour
{
    [SerializeField]
    private GameObject tower;
    private GameObject tempTower;
    private bool tempTowerPlaced;
    private bool towerPlaced;
    private float towerRot;
    public GameObject towerHolder;
    private GameObject placedTower;


    [SerializeField]
    private ResourceManager playerResourceManager;

    public bool scriptEnabled;

    //Which side of the board the build spot is on
    [SerializeField]
    private bool side;
    public bool getSide()
    {
        return side;
    }
    public void setSide(bool newSide)
    {
        side = newSide;
    }

    public GameObject PlacedTower()
    {
        return placedTower;
    }

    // Awake is called when script is enabled
    void Awake()
    {
        //Tracks information about tower and tower placement as well is if this script is enabled
        scriptEnabled = true;
        tempTowerPlaced = false;
        towerPlaced = false;
        towerRot = 90;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //Used to rotate the tower by incrementing the tower rotation in euler angles by 45 each time the button is pressed
        if (Input.GetButtonDown("RotateTower"))
        {
            towerRot += 45;
            if (towerRot >= 360)
            {
                towerRot = 0;
            }
        }*/
    }

    /*
    //OnMouseOver is called whne mouse hovers over object
    private void OnMouseOver()
    {
        if (towerPlaced == false && scriptEnabled == true)
        {
            if (tempTowerPlaced == false)
            {
                //Places temp tower on top of build spot
                float newTowerHeight;
                tempTower = (GameObject)Instantiate(tower, transform);
                tempTower.transform.position = this.transform.position;
                newTowerHeight = tempTower.transform.position.y + 0.03f;
                tempTower.transform.position = new Vector3(this.transform.position.x, newTowerHeight, tempTower.transform.position.z);

                //disabling collision on temp tower
                Collider[] colliders = tempTower.GetComponents<Collider>();
                foreach(Collider c in colliders)
                {
                    c.enabled = false;
                }
                tempTowerPlaced = true;
            }
            if (Input.GetButtonDown("Select"))
            {
                //Places down tower if player has enough essence and prefent further tower rotation manipulation untill current tower is deleted
                if(playerResourceManager.getPlayerEssence() >= tempTower.GetComponent<TowerBuildManager>().getCost())
                {
                    towerPlaced = true;
                    tempTower.GetComponent<TowerBuildManager>().setTowerBuildSpot(this);
                    tempTower.gameObject.layer = 0;

                    BasePointsScript points = tempTower.gameObject.GetComponent<BasePointsScript>();
                    points.playerResourceManager = playerResourceManager;
                    playerResourceManager.subtractPlayerEssence(tempTower.GetComponent<TowerBuildManager>().getCost());

                    //enabling collision on temp tower
                    Collider[] colliders = tempTower.GetComponents<Collider>();
                    foreach (Collider c in colliders)
                    {
                        c.enabled = true;
                    }
                    //Calling the event
                    EventManager.instance.OnTowerBuilt(tempTower.GetInstanceID());
                }
            }
            if (tempTower != null)
            {
                //Updates tower's rotation
                tempTower.transform.eulerAngles = new Vector3(0, towerRot, 0);
            }
        }
        //Destroys the temporary tower representation if script is no longer enabled
        if (tempTower != null && scriptEnabled == false)
        {
            Destroy(tempTower);
            tempTowerPlaced = false;
        }
    }

    //OnMouseExit is called when mouse hovering over object moves away from object
    private void OnMouseExit()
    {
        //Destroys the temp tower on mouse exit
        if (tempTower != null)
        {
            if (towerPlaced == false)
            {
                Destroy(tempTower);
                tempTowerPlaced = false;
            }
        }
    }*/

    //Used to reset tower values
    public void resetTowerPlaced()
    {
        tempTowerPlaced = false;
        towerPlaced = false;
        tempTower = null;
        towerRot = 90;
        towerHolder = null;
        placedTower = null;
    }


    public void setupTower(GameObject t1)
    {
        towerHolder = t1;
        placeTower();
    }

    public void placeTower()
    {
        if(towerPlaced == false)
        {
            if (playerResourceManager.getPlayerEssence() >= towerHolder.GetComponent<TowerBuildManager>().getCost())
            {
                float newTowerHeight;

                FMODUnity.RuntimeManager.PlayOneShot("event:/placingTowerNew");

                placedTower = (GameObject)Instantiate(towerHolder, transform);
                placedTower.transform.position = this.transform.position;
                //newTowerHeight = placedTower.transform.position.y + 0.03f;
                placedTower.transform.position = new Vector3(this.transform.position.x, placedTower.transform.position.y, placedTower.transform.position.z);

                towerPlaced = true;
                placedTower.GetComponent<TowerBuildManager>().setTowerBuildSpot(this);
                placedTower.gameObject.layer = 0;

                BasePointsScript points = placedTower.gameObject.GetComponent<BasePointsScript>();
                points.playerResourceManager = playerResourceManager;
                playerResourceManager.subtractPlayerEssence(placedTower.GetComponent<TowerBuildManager>().getCost());

                //enabling collision on temp tower
                Collider[] colliders = placedTower.GetComponents<Collider>();
                foreach (Collider c in colliders)
                {
                    c.enabled = true;
                }
                //Calling the event
                EventManager.instance.OnTowerBuilt(placedTower.GetInstanceID());

                //mark this spot as occupied in the global variable storage
                if (side)
                {
                    GlobalVariableStorage.Instance.p1OccupiedSpots.Add(placedTower);
                }
                else
                {
                    GlobalVariableStorage.Instance.p2OccupiedSpots.Add(placedTower);
                }

                towerPlaced = true;
            }
        }
    }

    public void towerRotation(int angle)
    {
        if (placedTower != null)
        {
            towerRot += angle;
            if (towerRot >= 360)
            {
                towerRot = 0;
            }
            placedTower.transform.eulerAngles = new Vector3(0, towerRot, 0);
        }
    }

    public void towerDeletion()
    {
        //remove reference from global variable storage
        if (side)
        {
            GlobalVariableStorage.Instance.p1OccupiedSpots.Remove(placedTower);
        }
        else
        {
            GlobalVariableStorage.Instance.p2OccupiedSpots.Remove(placedTower);
        }

        GameObject.Destroy(placedTower);
        resetTowerPlaced();
    }

    //check if there is any tower on the spots
    //return false if there is no tower
    public bool isTower()
    {
        if (placedTower == null) return false;
        else return true;
    }
}
