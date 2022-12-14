using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballParticleControllers : MonoBehaviour
{
    [SerializeField]
    private float effectLifeSpan = 0.5f;

    // Awake is called on object initialization
    void Awake()
    {
        StartCoroutine(lifeSpanCountdown());
    }

    IEnumerator lifeSpanCountdown()
    {
        yield return new WaitForSeconds(effectLifeSpan);
        Destroy(this.gameObject);
        StopCoroutine(lifeSpanCountdown());
    }
}
