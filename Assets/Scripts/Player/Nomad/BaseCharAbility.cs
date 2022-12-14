using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharAbility : MonoBehaviour
{
    public CharacterAbliitesUI characterAbliitesUI;
    //bool for when ability is active or not
    public bool p1AbilityActivated = false;
    public bool p2AbilityActivated = false;

    //time (in seconds) the ability lasts for
    [SerializeField]
    public float duration;

    //time (in seconds) the ability is in cooldown after being used
    [SerializeField]
    public float cooldown;

    //override this with the ability effect
    public abstract void activateAbility(bool side);

    public virtual IEnumerator abilityCooldown(bool side)
    {
        yield return new WaitForSeconds(cooldown);
        Debug.Log("ABILITY READY!");
        if (side)
        {
            p1AbilityActivated = false;
           
        }
        else
        {
            p2AbilityActivated = false;
        }
       
    }
}
