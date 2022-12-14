using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FireNomad : BaseCharAbility
{
    //Stores particle effects for each side of the board
    [SerializeField]
    GameObject smokeCloudPlay1Upper;
    [SerializeField]
    GameObject smokeCloudPlay1Lower;
    [SerializeField]
    GameObject smokeCloudPlay2Upper;
    [SerializeField]
    GameObject smokeCloudPlay2Lower;

    [SerializeField]
    GameObject meteor1;
    [SerializeField]
    GameObject meteor2;

    //Length the ability lasts for
    [SerializeField]
    private float abilityDurCount = 5;

    //Activates ability on a certain player's side depending on the side bool
    public override void activateAbility(bool side) 
    {

        if (side)
        {
            if (!p1AbilityActivated)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/FireNomad");

                
                StartCoroutine(startMeteors(side));

                p1AbilityActivated = true;
                characterAbliitesUI.updateSlidersP1();
            }
        }
        else
        {
            if (!p2AbilityActivated)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/FireNomad");

                
                StartCoroutine(startMeteors(side));

                p2AbilityActivated = true;
                characterAbliitesUI.updateSlidersP2();
            }
        }

        /*if (!abilityActivated)
        {
            abilityActivated = true;
            if (side == false)
            {
                smokeCloudPlay1.SetActive(true);
                StartCoroutine(abilityDuration(side));
            }
            else
            {
                smokeCloudPlay2.SetActive(true);
                StartCoroutine(abilityDuration(side));
            }
        }*/

    }

    IEnumerator startMeteors(bool side)
    {
        if (side == false)
        {
            meteor1.SetActive(true);
        }
        else
        {
            meteor2.SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        if (side == false)
        {
            meteor1.SetActive(false);
            smokeCloudPlay1Upper.SetActive(true);
            smokeCloudPlay1Lower.SetActive(true);
        }
        else
        {
            meteor2.SetActive(false);
            smokeCloudPlay2Upper.SetActive(true);
            smokeCloudPlay2Lower.SetActive(true);
        }

        StartCoroutine(abilityDuration(side));
        StopCoroutine(startMeteors(side));
    }

    //Coroutine used to make the ability last a certain period of time then disable it and start the abilityCooldown coroutine
    IEnumerator abilityDuration(bool side)
    {
        yield return new WaitForSeconds(abilityDurCount);
        
        if (side == false)
        {
            smokeCloudPlay1Upper.GetComponent<VisualEffect>().Stop();
            smokeCloudPlay1Lower.GetComponent<VisualEffect>().Stop();
            yield return new WaitForSeconds(2);
            smokeCloudPlay1Upper.SetActive(false);
            smokeCloudPlay1Lower.SetActive(false);

        }
        else
        {
            smokeCloudPlay2Upper.GetComponent<VisualEffect>().Stop();
            smokeCloudPlay2Lower.GetComponent<VisualEffect>().Stop();
            yield return new WaitForSeconds(2);
            smokeCloudPlay2Upper.SetActive(false);
            smokeCloudPlay2Lower.SetActive(false);

        }
        StartCoroutine(abilityCooldown(side));
        StopCoroutine(abilityDuration(side));
    }
}
