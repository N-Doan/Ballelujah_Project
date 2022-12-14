using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePointsCannonTower : BasePointsScript
{
    private float multiplier = 1.0f;
    private CannonTowerAi ai;

    private void Start()
    {
        ai = GetComponent<CannonTowerAi>();
    }
    public void bumperTowerUpgraded()
    {
        multiplier = 2.0f;
        StartCoroutine(bumperTowerEffectTimer());
    }
    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pinball" && ai.canFire == true)
        {
            playerResourceManager.givePlayerPoints(pointsGiven * (int)multiplier);
        }
    }*/

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pinball")
        {
            playerResourceManager.givePlayerPoints(pointsGiven * (int)multiplier);
        }
    }*/

    public void givePoints()
    {
        playerResourceManager.givePlayerPoints(pointsGiven * (int)multiplier);
    }

    IEnumerator bumperTowerEffectTimer()
    {
        yield return new WaitForSeconds(3);
        multiplier = 1.0f;
        StopCoroutine(bumperTowerEffectTimer());
    }
}
