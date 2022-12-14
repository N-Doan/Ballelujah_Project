using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerOne;
    [SerializeField]
    private GameObject playerTwo;

    [SerializeField]
    private GameObject p1Cursor;
    [SerializeField]
    private GameObject p2Cursor;

    [SerializeField]
    private PlayerController p1Control;
    [SerializeField]
    private PlayerController p2Control;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private UIManager ui;

    [SerializeField]
    private float newPinballCooldown = 5f;
    [SerializeField]
    private float gracePeriodLength = 30f;

    [SerializeField]
    private PinballSpawner spawner;

    [SerializeField]
    private BuildPhaseManager buildPhaseManager;

    [SerializeField]
    private float reservoirPenalty = 0.03f;

    [SerializeField]
    private PlayerInputManager playerInputManager;

    [SerializeField]
    private GameObject charAbilities;

    public GameObject[] playerInput;

    [SerializeField]
    private bool isTutorial = false;

    private ResourceManager resourceOne;
    private ResourceManager resourceTwo;
    private int winnerScore;
    private int loserScore;
    //p1 win ---> isWin = 0, p2 win ---> isWin = 1, tie ---> isWin = -1;
    private int isWin;

    private int p1Pinballs = 0;
    private int p2Pinballs = 0;

    private int p1Reservoir = 0;
    private int p2Reservoir = 0;

 
    [SerializeField]
    public GameObject pauseScreen;

    //The nomad character models
    [SerializeField]
    private GameObject[] nomadModels;
    [SerializeField]
    private Transform p1NomadSpawn;
    [SerializeField]
    private Transform p2NomadSpawn;

    //0 = FIRE, 1 = WATER, 2 = EARTH, 3 = AIR
    [SerializeField]
    private Material[] normalMats;
    [SerializeField]
    private Material[] happyMats;

    public bool playPhase = false;
    public bool gameIsPaused;
    private bool isPauseCheck;

    //bool so reservoir penalty is only applied once
    private bool reservoirApplied = false;
    // Start is called before the first frame update

    public int GameOverTImeCount;
    void Start()
    {
        //disabling all char abilities at start
        charAbilities.GetComponent<AirNomad>().enabled = false;
        charAbilities.GetComponent<FireNomad>().enabled = false;
        charAbilities.GetComponent<EarthNomad>().enabled = false;
        charAbilities.GetComponent<WaterNomad>().enabled = false;

        resourceOne = playerOne.GetComponent<ResourceManager>();
        resourceTwo = playerTwo.GetComponent<ResourceManager>();
        isWin = -1;
        if (!isTutorial)
        {
            StartCoroutine(gracePeriod());
            //StartCoroutine(addPinballs());
        }

        string p1Nomad = VariableSending.Player1Character;
        string p2Nomad = VariableSending.Player2Character;

        GameObject nomad = null;
        Material normal = null;
        Material happy = null;

        //reactivating the char abilities that are chosen
        switch (p1Nomad)
        {
            case "FIRE":
                charAbilities.GetComponent<FireNomad>().enabled = true;
                p1Control.ability = charAbilities.GetComponent<FireNomad>();
                nomad = Instantiate(nomadModels[0], p1NomadSpawn);
                normal = normalMats[0];
                happy = happyMats[0];
                break;
            case "WATER":
                charAbilities.GetComponent<WaterNomad>().enabled = true;
                p1Control.ability = charAbilities.GetComponent<WaterNomad>();
                nomad = Instantiate(nomadModels[1], p1NomadSpawn);
                normal = normalMats[1];
                happy = happyMats[1];
                break;
            case "EARTH":
                charAbilities.GetComponent<EarthNomad>().enabled = true;
                p1Control.ability = charAbilities.GetComponent<EarthNomad>();
                nomad = Instantiate(nomadModels[2], p1NomadSpawn);
                normal = normalMats[2];
                happy = happyMats[2];
                break;
            case "AIR":
                charAbilities.GetComponent<AirNomad>().enabled = true;
                p1Control.ability = charAbilities.GetComponent<AirNomad>();
                nomad = Instantiate (nomadModels[3], p1NomadSpawn);
                normal = normalMats[3];
                happy = happyMats[3];
                break;
            case null:
                charAbilities.GetComponent<FireNomad>().enabled = true;
                p1Control.ability = charAbilities.GetComponent<FireNomad>();
                nomad = Instantiate(nomadModels[0], p1NomadSpawn);
                normal = normalMats[0];
                happy = happyMats[0];
                break;
        }
        p1Control.nomadModelAnimator = nomad.GetComponent<Animator>();
        p1Control.baseMat = normal;
        p1Control.happyMat = happy;

        switch (p2Nomad)
        {
            case "FIRE":
                charAbilities.GetComponent<FireNomad>().enabled = true;
                p2Control.ability = charAbilities.GetComponent<FireNomad>();
                nomad = Instantiate(nomadModels[0], p2NomadSpawn);
                normal = normalMats[0];
                happy = happyMats[0];
                break;
            case "WATER":
                charAbilities.GetComponent<WaterNomad>().enabled = true;
                p2Control.ability = charAbilities.GetComponent<WaterNomad>();
                nomad = Instantiate(nomadModels[1], p2NomadSpawn);
                normal = normalMats[1];
                happy = happyMats[1];
                break;
            case "EARTH":
                charAbilities.GetComponent<EarthNomad>().enabled = true;
                p2Control.ability = charAbilities.GetComponent<EarthNomad>();
                nomad = Instantiate(nomadModels[2], p2NomadSpawn);
                normal = normalMats[2];
                happy = happyMats[2];
                break;
            case "AIR":
                charAbilities.GetComponent<AirNomad>().enabled = true;
                p2Control.ability = charAbilities.GetComponent<AirNomad>();
                nomad = Instantiate(nomadModels[3], p2NomadSpawn);
                normal = normalMats[3];
                happy = happyMats[3];
                break;
            case null:
                charAbilities.GetComponent<FireNomad>().enabled = true;
                p1Control.ability = charAbilities.GetComponent<FireNomad>();
                nomad = Instantiate(nomadModels[0], p1NomadSpawn);
                normal = normalMats[0];
                happy = happyMats[0];
                break;
        }
        p2Control.nomadModelAnimator = nomad.GetComponent<Animator>();
        p2Control.baseMat = normal;
        p2Control.happyMat = happy;

        spawnPlayer();

        gameIsPaused = false;

        playerInput = GameObject.FindGameObjectsWithTag("PlayerInput");
        isPauseCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPauseCheck)
        {
            if (gameIsPaused && Keyboard.current.escapeKey.wasReleasedThisFrame || Gamepad.current.selectButton.wasReleasedThisFrame && gameIsPaused) //Huh??? nullrefernce exception {becasue you start from main level}
            {
                for (int i = 0; i < playerInput.Length; i++)
                {
                    playerInput[i].GetComponent<PlayerInput>().actions.Enable();
                }
                ResumeGame();
            }
        }
        if (Keyboard.current.escapeKey.wasReleasedThisFrame && isPauseCheck || Gamepad.current.selectButton.wasReleasedThisFrame && gameIsPaused && isPauseCheck)
        {
            isPauseCheck = false;
        }
    }


    public void spawnPlayer()
    {
        if(VariableSending.controlnameP1!=null)
        {
            if (VariableSending.controlnameP1.ToString().Contains("Keyboard"))
            {
                p1Cursor.GetComponent<CursorHelper>().setKeyboard();
            }
            else
            {
                p1Cursor.GetComponent<CursorHelper>().setGamePad();
            }
        }
        if(VariableSending.controlnameP2!=null)
        {
            if (VariableSending.controlnameP2.ToString().Contains("Keyboard"))
            {
                p2Cursor.GetComponent<CursorHelper>().setKeyboard();
            }
            else
            {
                p2Cursor.GetComponent<CursorHelper>().setGamePad();
            }
        }
        playerInputManager.JoinPlayer(0, -1, null, VariableSending.controlnameP1);
        playerInputManager.JoinPlayer(1, -1, null, VariableSending.controlnameP2);
    }

    /// <summary>
    /// Reset Game
    /// 1.timer
    /// 2.player scores
    /// 3.player essence
    /// </summary>
    public void resetGame()
    {
        resourceOne.resetNums();
        resourceTwo.resetNums();

        timer.resetTimer();
    }

    /// <summary>
    /// Compare player's score
    /// Display winning screen
    /// </summary>
    public void gameOver()
    {
        //Stop all couroutines
        StopAllCoroutines();

        //stop vibration
        InputSystem.ResetHaptics();

        //stop game
        Time.timeScale = 0;

        //clean device
        VariableSending.controlnameP1 = null;
        VariableSending.controlnameP2 = null;

        if(reservoirApplied == false) 
        {
            //APPLY RESERVOIR EFFECT
            applyReservoirPenalty();
            reservoirApplied = true;
        }

        
        for(int i = 0; i < playerInput.Length;i++)
        {
            playerInput[i].GetComponent<PlayerInput>().actions.Disable();
        }

        //compare score 
        //player 1 win
        if (resourceOne.getPlayerPoints() > resourceTwo.getPlayerPoints())
        {
            isWin = 0;
            winnerScore = resourceOne.getPlayerPoints();
            loserScore = resourceTwo.getPlayerPoints();
            p1Control.playVictoryAnim();
            p2Control.playDissapointAnim();
        }
        //player 2 win
        else if (resourceOne.getPlayerPoints() < resourceTwo.getPlayerPoints())
        {
            isWin = 1;
            loserScore = resourceOne.getPlayerPoints();
            winnerScore = resourceTwo.getPlayerPoints();
            p2Control.playVictoryAnim();
            p1Control.playDissapointAnim();
        }
        //tie
        else
        {
            isWin = -1;
            winnerScore = loserScore = resourceOne.getPlayerPoints();
            p1Control.playVictoryAnim();
            p2Control.playVictoryAnim();
        }


        ui.gameOver(winnerScore,loserScore,isWin);

        
    }

    public void setIsTutorial(bool tut) 
    {
        isTutorial = tut;
        
    }

    public int getP1Pinballs() 
    {
        return p1Pinballs; 
    }
    public int getP2Pinballs() 
    { 
        return p2Pinballs; 
    }

    //P1 = TRUE p2 = FALSE
    //updates the pinball count for each player and increments the reservoir if either goes over the limit
    public bool incPlayerPinballs(int inc, bool player)
    {
        if (player)
        {
            if(p1Pinballs < 10 || inc <= 0)
            {
                p1Pinballs += inc;
                //Debug.Log("P1 PINBALLS: "+p1Pinballs);
                if(p1Pinballs <= 0 && playPhase == true)
                {
                    StartCoroutine(delayedSpawn(true));
                }
                else if(p1Pinballs == 1 && playPhase == true)
                {
                    StopCoroutine(delayedSpawn(true));
                    //Debug.Log("P1 DELAYED SPAWN STOPPED");
                }
            }
            else
            {
                p1Reservoir += 1;
                //Debug.Log("P1 RESEVOIR INC");
                //return false when reservoir was incremented
                return false;
            }
            
        }
        else
        {
            if(p2Pinballs < 10 || inc <= 0)
            {
                p2Pinballs += inc;
                //Debug.Log("P2 PINBALLS: "+p2Pinballs);
                if (p2Pinballs <= 0 && playPhase == true)
                {
                    StartCoroutine(delayedSpawn(false));
                }
                else if(p2Pinballs == 1 && playPhase == true)
                {
                    StopCoroutine(delayedSpawn(false));
                    //Debug.Log("P2 DELAYED SPAWN STOPPED");
                }
            }
            else
            {
                p2Reservoir += 1;
                //Debug.Log("P2 RESEVOIR INC");
                //return false when reservoir was incremented
                return false;
            }
        }
        //return true when the reservoir wasn't incremented
        return true;
    }

    //Applies the reservioir penalty to both players based off their reservoir count
    private void applyReservoirPenalty()
    {
        float p1ReservoirTotal = -1 * resourceOne.getPlayerPoints() * (reservoirPenalty * p1Reservoir);
        float p2ReservoirTotal = -1 * resourceTwo.getPlayerPoints() * (reservoirPenalty * p2Reservoir);

        resourceOne.givePlayerPoints((int)p1ReservoirTotal);
        resourceTwo.givePlayerPoints((int)p2ReservoirTotal);
    }

    //Coroutine for the grace period. Starts spawning pinballs after the grace period ends
    private IEnumerator gracePeriod()
    {
        yield return new WaitForSeconds(gracePeriodLength);
        //Debug.Log("GRACE PERIOD OVER");
        if (playPhase == true)
        {
            StartCoroutine(addPinballs());
        }
        StopCoroutine(gracePeriod());
    }

    //coroutine that spawns a new pinball every newPinballCooldown seconds
    public IEnumerator addPinballs()
    {
        for( ; ; )
        {
            if(p1Pinballs < 10)// && playPhase == true)
            {
                spawner.spawnPinball(true);
            }
            if(p2Pinballs < 10)// && playPhase == true)
            {
                spawner.spawnPinball(false);
            }
            yield return new WaitForSeconds(newPinballCooldown);
        }
    }

    private IEnumerator delayedSpawn(bool side)
    {
        yield return new WaitForSeconds(2);
        if(p1Pinballs == 0 || p2Pinballs == 0)
        {
            spawner.spawnPinball(side);
        }
        StopCoroutine(delayedSpawn(side));
    }

    private void ResumeGame()
    {
        if (pauseScreen != null) pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        gameIsPaused = false;
    }
    private void PauseGAME()
    {
        InputSystem.ResetHaptics();
        for (int i = 0; i < playerInput.Length; i++)
        {
            playerInput[i].GetComponent<PlayerInput>().actions.Disable();
        }
        isPauseCheck = true;
        if (pauseScreen != null) pauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        gameIsPaused = true;
    }

    public void CheckForPause() 
    {
        if (gameIsPaused) 
        {
            ResumeGame(); 
        }
        else
        {
            PauseGAME();
        }
    }

   
}
