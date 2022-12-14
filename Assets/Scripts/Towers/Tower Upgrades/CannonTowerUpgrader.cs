using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerUpgrader : BaseTowerUpgradePath
{
    private CannonTowerAi tower;
 
    
    public Material finalupgradeMat;
    // Start is called before the first frame update
    void Start()
    {
        tower = gameObject.GetComponent<CannonTowerAi>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P)) applyChange();
    }

    public override void applyUpgrade1()
    {
        tower.power = tower.power * 2f;
        //Debug.Log(tower.power);
        GetComponent<Renderer>().material.SetColor("_Color", Color.red);
    }

    public override void applyUpgrade2()
    {
        tower.cooldownTime = tower.cooldownTime / 2;
        GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
    }

    public override void applyUpgrade3()
    {
        
        applyChange();
        tower.fullyUpgraded = true;
        finalTowerVFX.SetActive(true);

    }

    private void applyChange() 
    {
        GetComponentInChildren<MaterialChange>().applyChange(finalupgradeMat);
    }

}
