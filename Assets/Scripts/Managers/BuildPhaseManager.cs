using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildPhaseManager : MonoBehaviour
{
    //Used to track pinballs game objects in scene
    private GameObject[] pinballs;
    //Serialized fields to customize length of effects and phases
    [SerializeField]
    private float buildPhaseLength = 15;
    [SerializeField]
    private float playPhaseLength = 15;
    [SerializeField]
    private float startGameLength = 3;
    [SerializeField]
    private GameObject pinballParticleEffect;
    [SerializeField]
    private GameObject buildPhaseStartText;
    [SerializeField]
    private GameObject playPhaseStartText;
    [SerializeField]
    private float phaseTextLength = 2.5f;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private GameObject phaseCountText;
    [SerializeField]
    Timer timer;

    [SerializeField]
    private GivePointsOnCollision[] playerNetZones;

    private float phaseTextCount;
    private bool countBuildPhase = false;
    private bool countStartPhase = false;
    private bool countPlayPhase = false;
    private float buildPhaseCount = 0;
    private int counter = 0;

    //Called on Start
    void Start()
    {
        timer.enabled = false;
        buildPhaseStartText.SetActive(false);
        playPhaseStartText.SetActive(false);
        StartCoroutine(startGamePhase());
    }

    void Update()
    {
        if (countBuildPhase == true)
        {
            displayTime(phaseTextCount);
            if (phaseTextCount <= 6.0f && phaseTextCount>=0.0f)
            {
                phaseCountText.SetActive(true);
            }
            phaseTextCount -= Time.deltaTime;
        }
        else if (countPlayPhase == true)
        {
            displayTime(phaseTextCount);
            if (phaseTextCount <= 6.0f && phaseTextCount>=0.0f)
            {
                phaseCountText.SetActive(true);
            }
            phaseTextCount -= Time.deltaTime;
        }
        else if (countStartPhase == true)
        {
            if (phaseTextCount > 0)
            {
                displayTime(phaseTextCount);
            }
            else
            {
                phaseCountText.GetComponent<TMP_Text>().text = "GO!";
            }
            phaseTextCount -= Time.deltaTime;
        }
        
        if (counter == 4) CancelInvoke("playAudio");
    }

    private void displayTime(float timeDisplay)
    {
        if (timeDisplay < 0)
        {
            timeDisplay = 0;
        }

        int tmpInt = (int)(phaseTextCount);
        phaseCountText.GetComponent<TMP_Text>().text = tmpInt.ToString();
    }

    private void playAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/CountdownUsing");
        counter += 1;
    }

    IEnumerator startGamePhase()
    {
        countStartPhase = true;
        phaseTextCount = startGameLength;
        buildPhaseStartText.SetActive(false);
        playPhaseStartText.SetActive(false);
        phaseCountText.SetActive(true);

        yield return new WaitForSeconds(startGameLength);
        timer.enabled = true;
        StartCoroutine(startBuildPhase());
        StopCoroutine(startGamePhase());
    }

    //Coroutine to start the build phase by disabling and enabling appropriate text as well as destroying all pinballs
    IEnumerator startBuildPhase()
    {
        buildPhaseCount++;
        if (buildPhaseCount > 2)
        {
            foreach (GivePointsOnCollision n in playerNetZones)
            {
                n.pointsGiven = -500;
            }
            StopCoroutine(startBuildPhase());
        }
        else {
            timer.timeCount = buildPhaseLength;
        }
        if(buildPhaseCount == 2)
        {
            foreach(GivePointsOnCollision n in playerNetZones)
            {
                n.pointsGiven = -1500;
            }
        }
        phaseTextCount = buildPhaseLength;
        countPlayPhase = false;
        InvokeRepeating("playAudio", 11, 1);
        levelManager.StopCoroutine("addPinballs");
        levelManager.StopCoroutine("delayedSpawn");
        countBuildPhase = true;
        countStartPhase = false;
        phaseCountText.SetActive(false);
        levelManager.playPhase = false;
        //Finds all objects that are tagged as Pinball and stores them in an array
        pinballs = GameObject.FindGameObjectsWithTag("Pinball");
        buildPhaseStartText.SetActive(true);
        playPhaseStartText.SetActive(false);
        //Used to remove the pinballs that were being tracked from the lists in levelmanager
        levelManager.incPlayerPinballs(levelManager.getP1Pinballs()*-1, true);
        levelManager.incPlayerPinballs(levelManager.getP2Pinballs() * -1, false);

        //Destroys all of the pinballs in scene and leaves a particle effect to play
        if (pinballs != null)
        {
            foreach (GameObject pBall in pinballs)
            {
                Instantiate(pinballParticleEffect, pBall.transform.position, Quaternion.identity);
                pBall.GetComponent<PinballSpecialEffectsController>().stopCoroutines();
                Destroy(pBall);
            }
        }
        //Waits for length of build phase to swap over to play phase
        yield return new WaitForSeconds(buildPhaseLength);
        counter = 0;

        StartCoroutine(startPlayPhase());
        StopCoroutine(startBuildPhase());
    }
    //Used to start the playphase by renabling and disabling text as well as restarting pinball spawning
    IEnumerator startPlayPhase()
    {
        timer.timeCount = playPhaseLength;
        phaseCountText.SetActive(false);
        phaseTextCount = playPhaseLength;
        levelManager.StartCoroutine("addPinballs");
        countBuildPhase = false;
        countPlayPhase = true;
        levelManager.playPhase = true;
        playPhaseStartText.SetActive(true);
        buildPhaseStartText.SetActive(false);
        //Used to start pinball spawning
        levelManager.incPlayerPinballs(0, true);
        levelManager.incPlayerPinballs(0, false);
        StartCoroutine(countPhaseText());

        //Starts build phase on end of play phase
        yield return new WaitForSeconds(playPhaseLength);

        StartCoroutine(startBuildPhase());
        StopCoroutine(startPlayPhase());
    }
    //Coroutine to track how long to keep phase change text on screen
    IEnumerator countPhaseText()
    {
        yield return new WaitForSeconds(phaseTextLength);
        buildPhaseStartText.SetActive(false);
        playPhaseStartText.SetActive(false);
        StopCoroutine(countPhaseText());
    }
}
