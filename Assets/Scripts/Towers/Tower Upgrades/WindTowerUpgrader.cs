using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTowerUpgrader : BaseTowerUpgradePath
{
    private WindBox box;
    public Material finalupgradeMat;
    private void Start()
    {
        box = gameObject.GetComponent<WindBox>();
    }

    public override void applyUpgrade1()
    {
        BoxCollider activeZone = box.GetComponent<BoxCollider>();
        Vector3 newSize = new Vector3(activeZone.size.x + activeZone.size.x * 0.5f, activeZone.size.y, activeZone.size.z);
        activeZone.size = newSize;
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    public override void applyUpgrade2()
    {
        box.setPushForce(box.getPushForce() + box.getPushForce() * 0.15f);
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }

    public override void applyUpgrade3()
    {
        //Handled in windBox script using isFullyUpgraded bool
        applyChange();
        //GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
        finalTowerVFX.SetActive(true);
    }

    private void applyChange()
    {
        GetComponentInChildren<MaterialChange>().applyChange(finalupgradeMat);
    }
}
