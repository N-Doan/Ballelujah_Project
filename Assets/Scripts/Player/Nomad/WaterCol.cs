using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCol : MonoBehaviour
{
    //Used to delay when shutting down column again so it has time to renable towers
    [SerializeField]
    private float disableSpeed = 2.0f;
    //Particle effects used as visuals for when towers are disabled by effect
    [SerializeField]
    private GameObject[] waterSpheres;
    //Tracks when tower should be enabled or disabled
    private bool enableTowers = false;

    //Plays on Awake
    public void Awake()
    {
        //Enables all  the visuals and the base bool state
        enableTowers = true;
        foreach (GameObject waterSphere in waterSpheres)
        {
            waterSphere.SetActive(true);
        } 
    }
    
    //Used to disable tower scripts
    public void DisableTowers()
    {
        enableTowers = false;
    }
    //Used to reenable towers and start a coroutine to disable this gameobject
    public void EnableTowers()
    {
        enableTowers = true;
        StartCoroutine(disableGameObject());
    }

    //Used to enable and disable all scripts on tower within the box collider of this col object based on the enableTowers bool
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tower")
        {
            MonoBehaviour[] scripts = other.gameObject.GetComponents<MonoBehaviour>();
            AdvancedFSM[] advScripts = other.gameObject.GetComponents<AdvancedFSM>();
            BasePointsScript[] bpScripts = other.gameObject.GetComponents<BasePointsScript>();
            BaseTowerUpgradePath[] btuScripts = other.gameObject.GetComponents<BaseTowerUpgradePath>();

            if (enableTowers == false)
            {
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = false;
                }

                foreach (AdvancedFSM script in advScripts)
                {
                    script.enabled = false;
                }
                foreach (BasePointsScript script in bpScripts)
                {
                    script.enabled = false;
                }
                foreach (BaseTowerUpgradePath script in btuScripts)
                {
                    script.enabled = false;
                }
            }
            else
            {
                foreach (MonoBehaviour script in scripts)
                {
                    script.enabled = true;
                }
                foreach (AdvancedFSM script in advScripts)
                {
                    script.enabled = true;
                }
                foreach (BasePointsScript script in bpScripts)
                {
                    script.enabled = true;
                }
                foreach (BaseTowerUpgradePath script in btuScripts)
                {
                    script.enabled = true;
                }
            }
        }
    }

    //Coroutine to delay column shutdown so all tower scripts can be reenabled
    IEnumerator disableGameObject()
    {
        foreach (GameObject waterSphere in waterSpheres)
        {
            waterSphere.SetActive(false);
        }
        yield return new WaitForSeconds(disableSpeed);
        this.gameObject.SetActive(false);
    }
}
