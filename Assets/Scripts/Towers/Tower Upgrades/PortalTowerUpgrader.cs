using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTowerUpgrader : BaseTowerUpgradePath
{
    [SerializeField]
    private float scaleBy;

    [SerializeField]
    private GameObject pullArea;

    [System.NonSerialized]
    public Transform[] activeAdjacentSpot;
    public Material finalupgradeMat;
    private void Start()
    {
        if(gameObject.GetComponentInParent<TowerBuildSpot>().getSide() == true)
        {
            activeAdjacentSpot = GlobalVariableStorage.Instance.p1AdjacentBuildSpots;
        }
        else
        {
            activeAdjacentSpot = GlobalVariableStorage.Instance.p2AdjacentBuildSpots;
        }
    }

    //scale up portal tower by scaleBy
    public override void applyUpgrade1()
    {
        gameObject.transform.localScale = new Vector3 (gameObject.transform.localScale.x + (gameObject.transform.localScale.x * scaleBy), gameObject.transform.localScale.y, gameObject.transform.localScale.z + (gameObject.transform.localScale.z * scaleBy));
        gameObject.transform.localScale = gameObject.transform.localScale + (gameObject.transform.localScale * scaleBy);
        GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.red);
    }

    public override void applyUpgrade2()
    {
        PortalTowerAI ai = GetComponent<PortalTowerAI>();
        ai.setCooldownTime(ai.getCooldownTime() - 0.75f);
        GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.blue);
    }

    public override void applyUpgrade3()
    {
        applyChange();
        pullArea.SetActive(true);
        GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.yellow);
        finalTowerVFX.SetActive(true);
    }

    private void applyChange()
    {
        GetComponentInChildren<MaterialChange>().applyChange(finalupgradeMat);
    }

}
