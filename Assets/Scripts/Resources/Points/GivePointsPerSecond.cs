using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePointsPerSecond : BasePointsScript
{
    [SerializeField]
    private float timeBetweenPoints = 1f;
    //turn give points coroutine on & off based on if a pinball is within the active zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pinball" && GetComponent<GivePointsPerSecond>().enabled)
        {
            StartCoroutine(givePlayerPPS());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pinball")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator givePlayerPPS()
    {
        for( ; ; )
        {
            playerResourceManager.givePlayerPoints(pointsGiven);
            yield return new WaitForSeconds(timeBetweenPoints);
        }
    }
}
