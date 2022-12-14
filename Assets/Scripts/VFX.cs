using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFX : MonoBehaviour
{
    [Header("VFX GameObjects")]
    public GameObject upgradeGameObject;
    public GameObject placeGameObject;
    public GameObject deleteGameObject;

    [Header("VFX")]
    public VisualEffect upgrade;
    public VisualEffect place;
    public VisualEffect delete; 
    

    [Header("Upgrade VFX PlayTime")]
    [Tooltip("Length of Seconds playtime for upgrade Vfx")]
    public float upgradePlayTime;
    public float deletePlayTime;
    public float placePlayTime;
    public void showPlaceTower() 
    {

     



    }
    public void showTowerUpgrade() 
    {
        StartCoroutine(showTowerUpgradecot());
    }

    public void deleteTower() 
    {
        
    }

    private IEnumerator showTowerUpgradecot() 
    {
        upgradeGameObject.SetActive(true);
        upgrade.Play();
        yield return new WaitForSeconds(upgradePlayTime);
        upgrade.Stop();
        //upgradeGameObject.SetActive(false);

    }

    private IEnumerator showTowerPlaceCot() 
    {
       
        place.Play();
       
        yield return new WaitForSeconds(placePlayTime);
        //place.Stop();
        placeGameObject.SetActive(false);
    }

   

}
