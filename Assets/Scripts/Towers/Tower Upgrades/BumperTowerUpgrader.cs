using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperTowerUpgrader : BaseTowerUpgradePath
{ 
    [SerializeField]
    GivePointsOnCollision givePoints;
    [SerializeField]
    float upForce = 20;

    private bool isFullyUpgraded;
    public Material finalupgradeMat;
    // Start is called before the first frame update
    void Start()
    {
        isFullyUpgraded = false;
    }

    public override void applyUpgrade1()
    {
        transform.localScale = new Vector3(this.transform.localScale.x*1.25f, this.transform.localScale.y * 1.25f, this.transform.localScale.z * 1.25f);
        //gameObject.GetComponent<BumperTowerAI>().getActivateState().force = upForce;
    }

    public override void applyUpgrade2()
    {
        givePoints.pointsGiven = 400;
    }

    public override void applyUpgrade3()
    {
        applyChange();
        isFullyUpgraded = true;
        givePoints.bumperTowerUpgraded();
        finalTowerVFX.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pinball"))
        {
            if (isFullyUpgraded == true && other.gameObject.GetComponent<PinballSpecialEffectsController>().fireTowerEffectActive == false)
            {
                other.gameObject.GetComponent<PinballSpecialEffectsController>().changePinballState(PinballSpecialEffectsController.Effects.FIRETOWER);
            }
            EventManager.instance.OnTowerTriggerEnter(gameObject.GetInstanceID());
        }


    }

    private void applyChange()
    {
        GetComponentInChildren<MaterialChange>().applyChange(finalupgradeMat);
    }
}
