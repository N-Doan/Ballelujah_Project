using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PinballSpecialEffectsController : MonoBehaviour
{
    public enum Effects
    {
        NONE, WINDTOWER, FIRETOWER
    }

    public bool windTowerEffectActive = false;
    public bool fireTowerEffectActive = false;

    //wind tower effect vars
    [SerializeField]
    private float newPBallMass = 0.15f;

    private float origPBallMass;
    private Color origPBallColour;

    private int spawnRate1, spawnRate2;

    Effects activeEffect;

    [SerializeField]
    private VisualEffect windTowerTrailVFX;
    [SerializeField]
    private VisualEffect fireTowerTrailVFX;

    [SerializeField]
    private TrailRenderer windTrail;
    [SerializeField]
    private TrailRenderer fireTrail;

    // Start is called before the first frame update
    void Start()
    {
        spawnRate1 = windTowerTrailVFX.GetInt("SpawnRate1");
        spawnRate2 = windTowerTrailVFX.GetInt("SpawnRate2");

        activeEffect = Effects.NONE;
        origPBallMass = gameObject.GetComponent<Rigidbody>().mass;
        origPBallColour = gameObject.GetComponent<Renderer>().material.color;
        windTowerTrailVFX.SetInt("SpawnRate1", 0);
        windTowerTrailVFX.SetInt("SpawnRate2", 0);
        windTrail.emitting = false;

        fireTowerTrailVFX.SetInt("SpawnRate1", 0);
        fireTowerTrailVFX.SetInt("SpawnRate2", 0);
        fireTrail.emitting = false;
    }

    public void changePinballState(Effects toApply)
    {
        switch (toApply)
        {
            case Effects.NONE:
                activeEffect = toApply;
                break;
            case Effects.FIRETOWER:
                activeEffect = toApply;
                applyBumperTowerEffect();
                break;
            case Effects.WINDTOWER:
                activeEffect = toApply;
                applyWindTowerEffect();
                break;
        }
    }

    private void applyWindTowerEffect()
    {
        gameObject.GetComponent<Rigidbody>().mass = newPBallMass;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
        windTowerEffectActive = true;

        windTrail.emitting = true;
        windTowerTrailVFX.SetInt("SpawnRate1", spawnRate1);
        windTowerTrailVFX.SetInt("SpawnRate2", spawnRate2);

        StartCoroutine(windTowerEffectTimer());
    }

    private void applyBumperTowerEffect()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;
        fireTowerEffectActive = true;

        fireTrail.emitting = true;
        fireTowerTrailVFX.SetInt("SpawnRate1", spawnRate1);
        fireTowerTrailVFX.SetInt("SpawnRate2", spawnRate2);

        StartCoroutine(bumperTowerEffectTimer());
    }

    public void stopCoroutines()
    {
        StopAllCoroutines();
    }
    IEnumerator windTowerEffectTimer()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Rigidbody>().mass = origPBallMass;
        gameObject.GetComponent<Renderer>().material.color = origPBallColour;
        windTowerEffectActive = false;

        windTrail.emitting = false;
        windTowerTrailVFX.SetInt("SpawnRate1", 0);
        windTowerTrailVFX.SetInt("SpawnRate2", 0);

        StopCoroutine(windTowerEffectTimer());

    }

    IEnumerator bumperTowerEffectTimer()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Renderer>().material.color = origPBallColour;
        fireTowerEffectActive = false;

        fireTrail.emitting = false;
        fireTowerTrailVFX.SetInt("SpawnRate1", 0);
        fireTowerTrailVFX.SetInt("SpawnRate2", 0);

        StopCoroutine(bumperTowerEffectTimer());
    }

}
