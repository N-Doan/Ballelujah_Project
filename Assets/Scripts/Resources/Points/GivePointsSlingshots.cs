using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePointsSlingshots : BasePointsScript
{
    private float multiplier = 1.0f;

    public void bumperTowerUpgraded()
    {
        multiplier = 2.0f;
        StartCoroutine(bumperTowerEffectTimer());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pinball")
        {
            // ? =if : = else
            if (gameObject.GetComponent<SlingShot>().isLeft ? collision.gameObject.transform.position.z - collision.contacts[0].point.z <= 0.005 : collision.gameObject.transform.position.z - collision.contacts[0].point.z >= 0.005)
            {
                playerResourceManager.givePlayerPoints(pointsGiven * (int)multiplier);
            } 
        }
    }

    IEnumerator bumperTowerEffectTimer()
    {
        yield return new WaitForSeconds(3);
        multiplier = 1.0f;
        StopCoroutine(bumperTowerEffectTimer());
    }
}
