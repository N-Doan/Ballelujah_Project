using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //left player == 0, right player == 1
    [SerializeField]
    private int ID;
    [SerializeField]
    private flipperScript flipperLeft;
    [SerializeField]
    private flipperScript flipperRight;
    [SerializeField]
    private GameObject towerSelectUI;
    [SerializeField]
    private Button[] selectBtn;
    [SerializeField]
    private GameObject towerUpgradeUI;
    [SerializeField]
    private GameObject fullUpgradeUI;
    [SerializeField]
    private ResourceManager resource;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private GameObject tipUI;
    
    [SerializeField]
    GameObject bumperTower;
    [SerializeField]
    GameObject windTower;
    [SerializeField]
    GameObject portalTower;
    [SerializeField]
    GameObject cannonTower;

    [SerializeField]
    public BaseCharAbility ability;

    public Material baseMat;
    public Material happyMat;

    public Animator nomadModelAnimator;

    int instanceID;

    private bool isLeftUp = false;

    public GameObject[] towerPosition;
    [SerializeField]
    private GameObject cube;

    private int index = 0;
    private int btnIndex = 0;
    private int uiIndex = 0;

    private bool isShow = false;
    private bool canUpgrade = false;
    private bool isJustBuild = false;
    private bool isEnough = false;

    private float rate = 0.18f;
    public bool isChange = false;
    private bool isUp = false;
    public int GetPlayerIndex()
    {
        return ID;
    }

    public int GetTowerIndex()
    {
        return index;
    }

    public GameObject GetTowerPosition()
    {
        return towerPosition[index];
    }

    private void Start()
    {
        towerSelectUI.SetActive(false);
        towerUpgradeUI.SetActive(false);
        fullUpgradeUI.SetActive(false);
        tipUI.SetActive(false);
     
        instanceID = transform.gameObject.GetInstanceID();

        EventManager.instance.EOnFlipperPressed += eventFlipperUp;
        EventManager.instance.EOnFlipperReleased += eventFlipperDown;

        var colors = selectBtn[0].GetComponent<Button>().colors;
        colors.normalColor = Color.red;
        selectBtn[0].GetComponent<Button>().colors = colors;

        cube.transform.position = towerPosition[0].transform.position;
    }
    public void LeftUp()
    {
        //key down
        flipperLeft.FlipperUp();
        EventManager.instance.OnFlipperUp(instanceID);
        FMODUnity.RuntimeManager.PlayOneShot("event:/flipper");
    }
    public void LeftDown()
    {
        //key down
        flipperLeft.FlipperDown();
        EventManager.instance.OnFlipperDown(instanceID);
    }

    public void RightUp()
    {
        flipperRight.FlipperUp();
        EventManager.instance.OnFlipperUp(instanceID);
        FMODUnity.RuntimeManager.PlayOneShot("event:/flipper");
    }

    public void RightDown()
    {
        flipperRight.FlipperDown();
        EventManager.instance.OnFlipperDown(instanceID);
    }

    private bool checkUpgrade()
    {
        GameObject towerHolder = towerPosition[index].GetComponent<TowerBuildSpot>().PlacedTower();
        if(towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 2)
        {
            cube.GetComponent<CursorIndicatorHandler>().setExtraEssence(true);
        }
        else
        {
            cube.GetComponent<CursorIndicatorHandler>().setExtraEssence(false);
        }
        if (towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 2 && resource.getPlayerEssence() >= 3)
        {
            return true;
        }
        else if (towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 0 || towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 1)
        {
            if (resource.getPlayerEssence() >= 2)
            {
                return true;
            }
        }
        return false;
    }


    public void CheckUi() 
    {
        if (!isShow)
        {
            isEnough = false;
            GameObject towerHolder = towerPosition[index].GetComponent<TowerBuildSpot>().PlacedTower();

            if (towerHolder.GetComponent<BaseTowerUpgradePath>().isFullyUpgraded())
            {
                UIHide();
                return;
            }

            //level 2 ---> level 3
            if (towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 2)
            {
                if (resource.getPlayerEssence() >= 3)
                {
                    isEnough = true;
                    resource.subtractPlayerEssence(3);
                }
            }

            if (towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 0 || towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 1)
            {
                if (resource.getPlayerEssence() >= 2)
                {
                    isEnough = true;
                    resource.subtractPlayerEssence(2);
                }
            }

        }
    }
    //A button
    public void select()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
        //when A button use for UI
        if (!isJustBuild)
        {
            if (!isShow)
            {
                if (towerPosition[index].GetComponent<TowerBuildSpot>().isTower())
                {
                    canUpgrade = true;
                    if (checkIsFull())
                    {
                        fullUpgradeUI.SetActive(true);
                        towerUpgradeUI.SetActive(false);
                        tipUI.SetActive(false);
                    }
                    else
                    {
                        //show upgrade UI and hide others
                        towerSelectUI.SetActive(false);
                        if (checkUpgrade()) towerUpgradeUI.SetActive(true);
                        else tipUI.SetActive(true);

                        //Checking if we're in the tutorial scene
                        PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
                        if (tutorialCheck != null)
                        {
                            tutorialCheck.onUpgradeMenuOpened();
                        }
                    }
                }
                else
                {
                    //Checking if we're in the tutorial scene
                    towerSelectUI.SetActive(true);
                    PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
                    if (tutorialCheck != null)
                    {
                        tutorialCheck.onTowerMenuOpened();
                    }

                    towerUpgradeUI.SetActive(false);
                }
                isShow = true;
            }
            else
            {
                fullUpgradeUI.SetActive(false);
                towerSelectUI.SetActive(false);
                towerUpgradeUI.SetActive(false);
                tipUI.SetActive(false);
                isShow = false;
            }
        }
        //when A button use for submit
        else
        {
            isJustBuild = false;
        }
    }

    private void Update()
    {
        if(isChange)
        {
            rate -= Time.deltaTime;
            if (rate <= 0 && isChange)
            {
                //Debug.Log("-----------");
                rate = 0.18f;
                //add cursor position
                moveCursor();
            }
        }

       // if()
    }

    private void moveCursor()
    {
        if(isUp && index < towerPosition.Length-1)
        {
            index++;
        }
        else if(!isUp && index > 0)
        {
            index--;
        }
        cube.transform.position = towerPosition[index].transform.position;
    }
    public void placeTowers(Vector2 dir)
    {
        UIHide();
        isJustBuild = false;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                isUp = true;
            }
            else if (dir.x < 0)
            {
                isUp = false;
            }
        }
        isChange = true;
    }
    public void placeOnRight(Vector2 dir)
    {
        //Debug.Log(towerPosition.Length);
        UIHide();
        isJustBuild = false;
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if (dir.x > 0)
            {
                while (true && index < towerPosition.Length-1)
                {
                    index++;
                    if (towerPosition[index] != null) break;
                }
            }
            else if (dir.x < 0)
            {
                while (true && index > 0)
                {
                    index--;
                    if (towerPosition[index] != null) break;
                }
            }
            cube.transform.position = towerPosition[index].transform.position;
        }
        else
        {
            if(dir.y<0)
            {
                if (index == 0) index += 2;
                else if (index == 1 || index == 2) index += 1;
            }
            else if(dir.y>0)
            {
                if (index == 4||index==2) index -= 2;
                else if (index == 3) index -= 1;
            }
            cube.transform.position = towerPosition[index].transform.position;
        }
    }

    public void towerRotationLeft()
    {
        if(isJustBuild) towerPosition[index].GetComponent<TowerBuildSpot>().towerRotation(45);
    }

    public void towerRotationRight()
    {
        if (isJustBuild) towerPosition[index].GetComponent<TowerBuildSpot>().towerRotation(-45);
    }
    
    public void towerDeletion()
    {
        if(towerPosition[index].GetComponent<TowerBuildSpot>().isTower() && isShow)
        {
            towerPosition[index].GetComponent<TowerBuildSpot>().towerDeletion();
            UIHide();
            FMODUnity.RuntimeManager.PlayOneShot("event:/TowerDelete");
        }
    }

    public void placeBumperTower()
    {
        if (isShow && !towerPosition[index].GetComponent<TowerBuildSpot>().isTower())
        {
            //p1
            if (ID == 0)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(bumperTower);
                bumperTower.GetComponent<VFX>().showPlaceTower();
            }
            //p2
            else if (ID == 1)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(bumperTower);
                bumperTower.GetComponent<VFX>().showPlaceTower();
            }
            UIHide();
            isJustBuild = true;

            //Checking if we're in the tutorial scene
            PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
            if (tutorialCheck != null)
            {
                tutorialCheck.onTowerBuilt();
            }
        }
    }

    public void placePortalTower()
    {
        if (isShow)
        {
            //p1
            if (ID == 0)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(portalTower);
                portalTower.GetComponent<VFX>().showPlaceTower();
            }
            //p2
            else if (ID == 1)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(portalTower);
                portalTower.GetComponent<VFX>().showPlaceTower();
            }
            UIHide();
            isJustBuild = true;

            //Checking if we're in the tutorial scene
            PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
            if (tutorialCheck != null)
            {
                tutorialCheck.onTowerBuilt();
            }
        }
    }

    public void placeWindTower()
    {
        if (isShow)
        {
            //p1
            if (ID == 0)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(windTower);
                windTower.GetComponent<VFX>().showPlaceTower();
            }
            //p2
            else if (ID == 1)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(windTower);
                windTower.GetComponent<VFX>().showPlaceTower();
            }
            UIHide();
            isJustBuild = true;

            //Checking if we're in the tutorial scene
            PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
            if (tutorialCheck != null)
            {
                tutorialCheck.onTowerBuilt();
            }
        }
    }

    public void placeCannonTower()
    {
        if (isShow)
        {
            //p1
            if (ID == 0)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(cannonTower);
                cannonTower.GetComponent<VFX>().showPlaceTower();

            }
            //p2
            else if (ID == 1)
            {
                towerPosition[index].GetComponent<TowerBuildSpot>().setupTower(cannonTower);
                cannonTower.GetComponent<VFX>().showPlaceTower();
            }
            UIHide();
            isJustBuild = true;

            //Checking if we're in the tutorial scene
            PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
            if (tutorialCheck != null)
            {
                tutorialCheck.onTowerBuilt();
            }
        }
    }

    private void UIHide()
    {
        towerSelectUI.SetActive(false);
        towerUpgradeUI.SetActive(false);
        fullUpgradeUI.SetActive(false);
        tipUI.SetActive(false);

        isShow = false;
    }

    private bool checkIsFull()
    {
        //if there is a tower on the spot
        if(towerPosition[index].GetComponent<TowerBuildSpot>().isTower())
        {
            //get the tower obj
            GameObject towerHolder = towerPosition[index].GetComponent<TowerBuildSpot>().PlacedTower();

            return towerHolder.GetComponent<BaseTowerUpgradePath>().isFullyUpgraded();
        }
        else
        {
            return false;
        }
    }
    public void towerUpgrade()
    {
        if (!isShow) return;
        if (towerPosition[index].GetComponent<TowerBuildSpot>().isTower()) canUpgrade = true;
        else return;

        if (canUpgrade && resource.getPlayerEssence() >=2)
        {
            isEnough = false;
            GameObject towerHolder = towerPosition[index].GetComponent<TowerBuildSpot>().PlacedTower();

            if (towerHolder.GetComponent<BaseTowerUpgradePath>().isFullyUpgraded())
            {
                UIHide();
                return;
            }

            //level 2 ---> level 3
            if (towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 2)
            {
                if(resource.getPlayerEssence() >= 3)
                {
                    isEnough = true;
                    resource.subtractPlayerEssence(3);
                }
            }

            if (towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade == 0 || towerHolder.GetComponent<BaseTowerUpgradePath>().currentUpgrade==1)
            {
                if(resource.getPlayerEssence() >= 2)
                {
                    isEnough = true;
                    resource.subtractPlayerEssence(2);
                }
            }

            //to do: find a easier way to do this BRUHHHHHHHHH
            //add the rest tower upgrade
            if(isEnough)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/upgradeTower");
                if (towerHolder.GetComponent<BumperTowerAI>())
                {
                    towerHolder.GetComponent<BumperTowerUpgrader>().upgrade();
                    towerHolder.GetComponent<VFX>().showTowerUpgrade();
                }
                if (towerHolder.GetComponent<PortalTowerUpgrader>())
                {
                    towerHolder.GetComponent<PortalTowerUpgrader>().upgrade();
                    towerHolder.GetComponent<VFX>().showTowerUpgrade();
                }
                if (towerHolder.GetComponent<CannonTowerAi>())
                {
                    towerHolder.GetComponent<CannonTowerUpgrader>().upgrade();
                    towerHolder.GetComponent<VFX>().showTowerUpgrade();
                }
                if (towerHolder.GetComponent<WindTowerUpgrader>())
                {
                    //upgrade wind tower
                    towerHolder.GetComponent<WindTowerUpgrader>().upgrade();
                    towerHolder.GetComponent<VFX>().showTowerUpgrade();
                }
                UIHide();

                //Checking if we're in the tutorial scene
                PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
                if (tutorialCheck != null)
                {
                    tutorialCheck.onTowerUpgraded();
                }
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/NotEnoughEssense");
            }
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/NotEnoughEssense");
        }
    }
    private void eventFlipperUp(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("FLIPPER TRIGGERED");
        }
    }

    private void eventFlipperDown(int id)
    {
        if (id == instanceID)
        {
            //Debug.Log("FLIPPER RELEASED");
        }
    }

    public void PasueOrPlayGame() 
    {
        levelManager.CheckForPause();
        
    }

    public void activateCharAbility()
    {
        //Checking to see if we're in the tutorial scene
        PlayerInputTutorial tutorialCheck = gameObject.GetComponentInChildren<PlayerInputTutorial>();
        if (tutorialCheck != null)
        {
            tutorialCheck.onNomadAbilityUsed();
        }

        if (ID == 0 && ability.p1AbilityActivated == false)
        {
            GameObject baseModel = nomadModelAnimator.gameObject.transform.Find("Base").gameObject;
            nomadModelAnimator.SetTrigger("Celebration");
            baseModel.GetComponent<SkinnedMeshRenderer>().material = happyMat;
            StartCoroutine(swapBackMat());
            ability.activateAbility(true);
        }
        else if (ID == 1 && ability.p2AbilityActivated == false)
        {
            GameObject baseModel = nomadModelAnimator.gameObject.transform.Find("Base").gameObject;
            nomadModelAnimator.SetTrigger("Celebration");
            baseModel.GetComponent<SkinnedMeshRenderer>().material = happyMat;
            StartCoroutine(swapBackMat());
            ability.activateAbility(false);
        }
        
    }

    public void playDissapointAnim()
    {
        nomadModelAnimator.SetTrigger("Disappointment");
    }

    public void playVictoryAnim()
    {
        nomadModelAnimator.SetTrigger("Victory");
        GameObject baseModel = nomadModelAnimator.gameObject.transform.Find("Base").gameObject;
        baseModel.GetComponent<SkinnedMeshRenderer>().material = happyMat;
    }

    private IEnumerator swapBackMat()
    {
        yield return new WaitForSeconds(2.0f);
        GameObject baseModel = nomadModelAnimator.gameObject.transform.Find("Base").gameObject;
        baseModel.GetComponent<SkinnedMeshRenderer>().material = baseMat;
    }
}
