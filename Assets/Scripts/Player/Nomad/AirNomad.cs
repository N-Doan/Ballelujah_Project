using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AirNomad : BaseCharAbility
{
    [SerializeField]
    private GameObject p1WindBox;

    [SerializeField]
    private GameObject p2WindBox;

    [Header("VFX")]
    [SerializeField]
    private VisualEffect P1;
    [SerializeField]
    private VisualEffect P2;


    private void Start()
    {
        p1WindBox.SetActive(false);
        p2WindBox.SetActive(false);
        P1 = p1WindBox.GetComponentInChildren<VisualEffect>();
        P2 = p2WindBox.GetComponentInChildren<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        //Activates when Nomad buttons is pressed down used to get all of the rigidbodies from pinballs in scene then set mass to a lower value
        
        /*if (Input.GetButtonDown("Nomad") && abilityActivated == false && inCooldown == false)
        {
            activateAbility(side);
        }*/
    }

    public override void activateAbility(bool s)
    {

        /*pinballs = GameObject.FindGameObjectsWithTag("Pinball");

        foreach (GameObject pBalls in pinballs)
        {
            //Used to record original mass to later reset once coroutine is done
            pballOrgMass = pBalls.GetComponent<Rigidbody>().mass;
            pBalls.GetComponent<Rigidbody>().mass = newPBallMass;
        }*/


        if (s)
        {
            if (!p1AbilityActivated)
            {
                p2WindBox.SetActive(true);
                p1AbilityActivated = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/AirNomad");
                StartCoroutine(abilityCountDown(s));
                characterAbliitesUI.updateSlidersP1();
            }
        }
        else
        {
            if (!p2AbilityActivated)
            {
                p1WindBox.SetActive(true);
                p2AbilityActivated = true;
                FMODUnity.RuntimeManager.PlayOneShot("event:/AirNomad");
                StartCoroutine(abilityCountDown(s));
                characterAbliitesUI.updateSlidersP2();
            }
        }

    }

    IEnumerator abilityCountDown(bool s)
    {
        //Forces coroutine to wait till the duration of the ability has been reached before continuing
        yield return new WaitForSeconds(duration);
        P1.Stop();
        P2.Stop();
        yield return new WaitForSeconds(1f);
        p1WindBox.SetActive(false);
        p2WindBox.SetActive(false);
        /*//Resets each pinballs mass to their original value
        foreach (GameObject pBalls in pinballs)
        {
            pBalls.GetComponent<Rigidbody>().mass = pballOrgMass;
        }*/

        StartCoroutine(abilityCooldown(s));

        //Stops coroutine from running
        StopCoroutine(abilityCountDown(s));
    }

}
