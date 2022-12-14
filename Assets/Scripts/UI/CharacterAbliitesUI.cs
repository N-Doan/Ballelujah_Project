using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAbliitesUI : MonoBehaviour
{

    public LevelManager level;
    private float maxValue = 55f;
    private float minValue = 0f;
    private float lerpDuration = 0.9f;
    private float timeElapsed;
    private float timeElapsed1;
    [SerializeField]
    private AirNomad air;
    [SerializeField]
    private EarthNomad earth;
    [SerializeField]
    private WaterNomad water;
    [SerializeField]
    private FireNomad fire;

    [SerializeField]
    private GameObject characterAbilitiesObjects;

    [Header("PLAYER 1 SLIDERS")]

    [SerializeField]
    private Slider fireSlider;
    [SerializeField]
    private Slider waterSlider;
    [SerializeField]
    private Slider earthSlider;
    [SerializeField]
    private Slider airSlider;
    [Header("PLAYER 2 SLIDERS")]

    [SerializeField]
    private Slider fireSlider1;
    [SerializeField]
    private Slider waterSlider1;
    [SerializeField]
    private Slider earthSlider1;
    [SerializeField]
    private Slider airSlider1;

    [SerializeField]
    private GameObject[] p1Particles;

    [SerializeField]
    private GameObject[] p2Particles;

    [SerializeField]
    private GameObject p1AbilityIndicators;
    [SerializeField]
    private GameObject p2AbilityIndicators;

    // Start is called before the first frame update
    private string p1Nomad;
    private string p2Nomad;

    public float timer;

    public bool active;

    void Start()
    {
        p1Nomad = VariableSending.Player1Character;
        p2Nomad = VariableSending.Player2Character;

        air = characterAbilitiesObjects.GetComponent<AirNomad>();
        fire = characterAbilitiesObjects.GetComponent<FireNomad>();
        water = characterAbilitiesObjects.GetComponent<WaterNomad>();
        earth = characterAbilitiesObjects.GetComponent<EarthNomad>();
        checkEnabledP1();
        checkEnabledP2();
        setSlidersValue();

        foreach(GameObject g in p1Particles)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in p2Particles)
        {
            g.SetActive(false);
        }
        checkEnabledP1();
        checkEnabledP2();

        if (VariableSending.controlnameP1.name.Contains("Keyboard"))
        {
            p1AbilityIndicators.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            p1AbilityIndicators.transform.GetChild(1).gameObject.SetActive(false);
        }

        if (VariableSending.controlnameP2.name.Contains("Keyboard"))
        {
            p2AbilityIndicators.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            p2AbilityIndicators.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void setSlidersValue()
    {
        airSlider.value = maxValue;
        waterSlider.value = maxValue;
        fireSlider.value = maxValue;
        earthSlider.value = maxValue;

        airSlider1.value = maxValue;
        waterSlider1.value = maxValue;
        fireSlider1.value = maxValue;
        earthSlider1.value = maxValue;
    }
    private void checkEnabledP1()
    {
        if (p1Nomad == "WATER")
        {
            waterSlider.gameObject.SetActive(true);
            p1Particles[0].SetActive(true);
        }
        else
        {
            waterSlider.gameObject.SetActive(false);
        }

        if (p1Nomad == "EARTH")
        {
            earthSlider.gameObject.SetActive(true);
            p1Particles[3].SetActive(true);
        }
        else
        {
            earthSlider.gameObject.SetActive(false);
        }

        if (p1Nomad == "FIRE")
        {
            fireSlider.gameObject.SetActive(true);
            p1Particles[1].SetActive(true);
        }
        else
        {
            fireSlider.gameObject.SetActive(false);
        }

        if (p1Nomad == "AIR")
        {
            airSlider.gameObject.SetActive(true);
            p1Particles[2].SetActive(true);
        }
        else
        {
            airSlider.gameObject.SetActive(false);
        }
    }
    private void checkEnabledP2()
    {
        if (p2Nomad == "WATER")
        {
            waterSlider1.gameObject.SetActive(true);
            p2Particles[0].SetActive(true);
        }
        else
        {
            waterSlider1.gameObject.SetActive(false);
        }

        if (p2Nomad == "EARTH")
        {
            earthSlider1.gameObject.SetActive(true);
            p2Particles[3].SetActive(true);
        }
        else
        {
            earthSlider1.gameObject.SetActive(false);
        }

        if (p2Nomad == "FIRE")
        {
            fireSlider1.gameObject.SetActive(true);
            p2Particles[1].SetActive(true);
        }
        else
        {
            fireSlider1.gameObject.SetActive(false);
        }

        if (p2Nomad == "AIR")
        {
            airSlider1.gameObject.SetActive(true);
            p2Particles[2].SetActive(true);
        }
        else
        {
            airSlider1.gameObject.SetActive(false);
        }
    }
    public void updateSlidersP1()
    {

        if (air.p1AbilityActivated == true)
        {
            StopCoroutine(waitFor45P1(airSlider));
            StartCoroutine(waitFor45P1(airSlider));
            p1Particles[2].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(true));
            disableNomadAbilityIndicator(true);
        }

        else if (water.p1AbilityActivated == true)
        {
            StopCoroutine(waitFor45P1(waterSlider));
            StartCoroutine(waitFor45P1(waterSlider));
            p1Particles[0].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(true));
            disableNomadAbilityIndicator(true);
        }

        else if (earth.p1AbilityActivated == true)
        {
            StopCoroutine(waitFor45P1(earthSlider));
            StartCoroutine(waitFor45P1(earthSlider));
            p1Particles[3].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(true));
            disableNomadAbilityIndicator(true);
        }

        else if (fire.p1AbilityActivated == true)
        {
            StopCoroutine(waitFor45P1(fireSlider));
            StartCoroutine(waitFor45P1(fireSlider));
            p1Particles[1].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(true));
            disableNomadAbilityIndicator(true);
        }

    }
    public void updateSlidersP2()
    {
        if (air.p2AbilityActivated == true)
        {
            StopCoroutine(waitFor45P2(airSlider1));
            StartCoroutine(waitFor45P2(airSlider1));
            p2Particles[2].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(false));
            disableNomadAbilityIndicator(false);
        }

        else if (water.p2AbilityActivated == true)
        {
            StopCoroutine(waitFor45P2(waterSlider1));
            StartCoroutine(waitFor45P2(waterSlider1));
            p2Particles[0].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(false));
            disableNomadAbilityIndicator(false);
        }

        else if (earth.p2AbilityActivated == true)
        {
            StopCoroutine(waitFor45P2(earthSlider1));
            StartCoroutine(waitFor45P2(earthSlider1));
            p2Particles[3].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(false));
            disableNomadAbilityIndicator(false);

        }

        else if (fire.p2AbilityActivated == true)
        {
            StopCoroutine(waitFor45P2(fireSlider1));
            StartCoroutine(waitFor45P2(fireSlider1));
            p2Particles[1].SetActive(false);
            StopCoroutine(enableNomadAbilityIndicator(false));
            disableNomadAbilityIndicator(false);
        }
    }

    private IEnumerator waitFor45P1(Slider slider) 
    {
        slider.value = 0;
        //slider.value += 1f;
        //yield return new WaitForSeconds(maxValue);
        for(int i = 0; i < maxValue/0.1f; i++)
        {
            slider.value += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        //slider.value = 55;
        StopCoroutine(waitFor45P1(slider));
        checkEnabledP1();

        StartCoroutine(enableNomadAbilityIndicator(true));

        yield return null;
    }

    private IEnumerator waitFor45P2(Slider slider)
    {
        slider.value = 0;
        //slider.value += 1f;
        //yield return new WaitForSeconds(maxValue);
        for (int i = 0; i < maxValue / 0.1f; i++)
        {
            slider.value += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        //slider.value = 55;
        StopCoroutine(waitFor45P2(slider));
        checkEnabledP2();

        StartCoroutine(enableNomadAbilityIndicator(false));

        yield return null;
    }

    private IEnumerator enableNomadAbilityIndicator(bool side)
    {
        yield return new WaitForSeconds(4);
        if (side)
        {
            if (VariableSending.controlnameP1.name.Contains("Keyboard"))
            {
                p1AbilityIndicators.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                p1AbilityIndicators.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            if (VariableSending.controlnameP2.name.Contains("Keyboard"))
            {
                p2AbilityIndicators.transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                p2AbilityIndicators.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void disableNomadAbilityIndicator(bool side)
    {
        if (side)
        {
            p1AbilityIndicators.transform.GetChild(0).gameObject.SetActive(false);
            p1AbilityIndicators.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            p2AbilityIndicators.transform.GetChild(0).gameObject.SetActive(false);
            p2AbilityIndicators.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
