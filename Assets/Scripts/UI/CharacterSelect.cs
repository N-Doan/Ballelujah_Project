using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;

public class CharacterSelect : MonoBehaviour
{
    EventSystem eventSystem;
    [SerializeField]
    private UiController ui;
    
    [Header("Nomad Ablitiy Display")]
    [SerializeField]
    private NomadAblilityDisplayCanvas nomadAblilityDisplay;
    [SerializeField]
    private GameObject nomadAblityCanvas;
    
    [Header("GameObjects Ints")]
    public int selectedCharacter;
    public int P1selectedCharacter;
    public int P2selectedCharacter;

    [Header("UI Elemnts")]
    public Text playertext;
    public Image image;
    public Text text;
    public Image triImage;

    [Header("CharacterSelect GameObjects")]
    public GameObject characterSelect;
    public int turn = 0;
    public GameObject play;
    public GameObject back;

    public GameObject P1Button;
    public GameObject P2Button;

    [Header("Character Nomads Animators")]
    public Animator airNomadAnime;
    public Animator fireNomadAnime;
    public Animator waterNomadAnime;
    public Animator earthNomadAnime;

    [Header("Character Nomads Textures")]
    
    [SerializeField]
    private GameObject[] characters;

    [SerializeField]
    private Material[] happyMaterials;
    [SerializeField]
    private Material[] baseMaterials;



    [Header("Character Nomads GameObject Poisitions")]
    [Tooltip("Each element of the circle Pos, is important for the select methods")]
    [SerializeField]
    private Transform[] circlePos;

    [Header("Cursor GameObjects")]
    [SerializeField]
    private GameObject circleP1;
    [SerializeField]
    private GameObject circleP2;

    [Header("Input Devices")]
    private int p1;
    private int p2;

    [SerializeField]
    private UiController uiController;
    [SerializeField]
    private GameObject deviceTip;
    private int isReadyToCharacter;


    private bool p1GamePad;
    private bool p2GamePad;

    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private DeviceSelection deviceSelection;

    public bool GetP1GamePad() { return p1GamePad; }
    public bool GetP2GamePad() { return p2GamePad; }
    void Start()
    {
        eventSystem = EventSystem.current;
        isReadyToCharacter = 0;
    }

    void Update()
    {
        showCursorsDuringTurns();
        playTurnUpdate();
    }
    private void playTurnUpdate()
    {
        if (turn == 1)
        {
            playertext.text = "Player 1";
        }
        if (turn == 2)
        {
            playertext.text = "Player 2";
        }
    }
    public void setCursorPositions()
    {
        circleP1.transform.position = circlePos[0].position;
        circleP2.transform.position = circlePos[0].position;
    }
    public void characterHideCursour()
    {
        circleP1.SetActive(false);
        circleP2.SetActive(false);

    }
    public void showAblityCursor()
    {
        nomadAblityCanvas.SetActive(true);
    }
    public void hideAblityCursor()
    {
        nomadAblityCanvas.SetActive(false);
    }
    public void characterShowCursour()
    {
        circleP1.SetActive(true);
        circleP2.SetActive(true);
    }

    public void needMultiple()
    {
        image.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        triImage.gameObject.SetActive(true);
    }
    public void needMultipleHide()
    {
        image.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        triImage.gameObject.SetActive(false);
    }
    private void showCursorsDuringTurns()
    {
        //Shows P1 and P2 Cursors depending on the player turn

        if (turn == 1)
        {
            circleP1.SetActive(true);

        }
        else
        {
            circleP1.SetActive(false);
        }
        if (turn == 2)
        {
            circleP2.SetActive(true);

        }
        else
        {
            circleP2.SetActive(false);
        }
    }

    public void SelectFire()
    {
        //Event Trigger for moving the Cursor GameObjects 
        if (turn == 1)
        {
            circleP1.transform.position = circlePos[0].position;
        }
        else
        {
            circleP2.transform.position = circlePos[0].position;
        }

        nomadAblilityDisplay.setFireText();
    }

    public void SelectAir()
    {
        //Event Trigger for moving the Cursor GameObjects 

        if (turn == 1)
        {
            circleP1.transform.position = circlePos[1].position;
        }
        else
        {
            circleP2.transform.position = circlePos[1].position;
        }
        nomadAblilityDisplay.setAirText();
    }

    public void SelectEarth()
    {
        //Event Trigger for moving the Cursor GameObjects 

        if (turn == 1)
        {
            circleP1.transform.position = circlePos[2].transform.position;
        }
        else
        {
            circleP2.transform.position = circlePos[2].transform.position;
        }
        nomadAblilityDisplay.setEarthText();
    }

    public void SelectWater()
    {
        //Event Trigger for moving the Cursor GameObjects 

        if (turn == 1)
        {
            circleP1.transform.position = circlePos[3].transform.position;
        }
        else
        {
            circleP2.transform.position = circlePos[3].transform.position;
        }
        nomadAblilityDisplay.setWaterText();
    }
    private void swapTexture(GameObject character, int characterNum, Material[] mat)
    {
        Debug.Log(character.GetComponent<SkinnedMeshRenderer>().material.name);
        character.GetComponent<SkinnedMeshRenderer>().material = mat[characterNum];
        Debug.Log(character.GetComponent<SkinnedMeshRenderer>().material.name);
    }
    public void FireNomad()
    {
        //Select Fire 
        FMODUnity.RuntimeManager.PlayOneShot("event:/CharacterSelect");
        selectedCharacter = 1;

        if (turn == 1)
        {
            playNomadAnims(fireNomadAnime, "Excitement");
            //textureswap
            swapTexture(characters[0], 0, happyMaterials);
            P1choices(selectedCharacter);
        }
        else if (turn == 2)
        {
            playNomadAnims(fireNomadAnime, "Excitement");
            swapTexture(characters[0], 0, happyMaterials);
            P2choices(selectedCharacter);
        }
    }
    public void AirNomad()
    {

        //select Air
        FMODUnity.RuntimeManager.PlayOneShot("event:/CharacterSelect");
        selectedCharacter = 2;

        if (turn == 1)
        {
            playNomadAnims(airNomadAnime, "Excitement");
            swapTexture(characters[1], 1, happyMaterials);
            P1choices(selectedCharacter);
        }
        else if (turn == 2)
        {
            playNomadAnims(airNomadAnime, "Excitement");
            P2choices(selectedCharacter);
            swapTexture(characters[1], 1, happyMaterials);
        }
    }

    public void EarthNomad()
    {
        //Select Earth 
        FMODUnity.RuntimeManager.PlayOneShot("event:/CharacterSelect");
        selectedCharacter = 3;

        if (turn == 1)
        {
            playNomadAnims(earthNomadAnime, "Excitement");
            P1choices(selectedCharacter);
            swapTexture(characters[2], 2, happyMaterials);
        }
        else if (turn == 2)
        {
            playNomadAnims(earthNomadAnime, "Excitement");
            P2choices(selectedCharacter);
            swapTexture(characters[2], 2, happyMaterials);
        }
    }
    public void WaterNoamd()
    {
        //Select Water
        FMODUnity.RuntimeManager.PlayOneShot("event:/CharacterSelect");
        selectedCharacter = 4;

        if (turn == 1)
        {
            playNomadAnims(waterNomadAnime, "Excitement");
            P1choices(selectedCharacter);
            swapTexture(characters[3], 3, happyMaterials);
        }
        else if (turn == 2)
        {
            playNomadAnims(waterNomadAnime, "Excitement");
            swapTexture(characters[3], 3, happyMaterials);
            P2choices(selectedCharacter);
        }
    }

    private void playNomadAnims(Animator anim, string name)
    {
        anim.SetBool(name, true);
    }

    public void checkDeviceP1()
    {
        needMultipleHide();
        //Check the Input System 
        if (InputSystem.GetDevice<Keyboard>().enterKey.isPressed)
        {
            VariableSending.controlnameP1 = InputSystem.GetDevice<Keyboard>();
            p1GamePad = false;
            VariableSending.p1GamePad = false;
            deviceSelection.cleanP1();
            deviceSelection.p1KeyBoard();
            Debug.Log("KEYBOARD");
        }
        else
        {
            Debug.Log("GAMEPAD");
            deviceSelection.cleanP1();
            deviceSelection.p1Gamepad();
            p1GamePad = true;
            VariableSending.p1GamePad = true;
            VariableSending.controlnameP1 = InputSystem.GetDevice<Gamepad>();
            controllerViberate();
            Invoke("stopViberate", 1);
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
        p1 = 0;
        VariableSending.player1 = p1;


        isReadyToCharacter++;
        playerSelectDevice();
    }


    public void checkDeviceP2()
    {
        needMultipleHide();
        //Check the Input System
        if (InputSystem.GetDevice<Keyboard>().enterKey.isPressed)
        {
            p2GamePad = false;
            VariableSending.p2GamePad = false;
            VariableSending.controlnameP2 = InputSystem.GetDevice<Keyboard>();
            deviceSelection.cleanP2();
            deviceSelection.p2KeyBoard();
        }
        else
        {
            controllerViberate();
            Invoke("stopViberate", 1);
            p2GamePad = true;
            deviceSelection.cleanP2();
            deviceSelection.p2Gamepad();
            VariableSending.p2GamePad = true;
            VariableSending.controlnameP2 = InputSystem.GetDevice<Gamepad>();
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
        p2 = 1;
        VariableSending.player2 = p2;
        isReadyToCharacter++;
        playerSelectDevice();

    }
    public void P1choices(int x)
    {
        P1selectedCharacter = x;
        turn++;
        sendP1(x);
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
        circleP2.transform.position = circlePos[0].position;
        InputSystem.DisableDevice(VariableSending.controlnameP1);
        InputSystem.EnableDevice(VariableSending.controlnameP2);
        if (!GetP2GamePad())
        {
            var user = playerInput.user;
            user.ActivateControlScheme("Keyboard&Mouse");
        }
        else
        {
            var user = playerInput.user;
            user.ActivateControlScheme("GamePad");
        }
    }

    public void P2choices(int x)
    {
        P2selectedCharacter = x;
        CharacterToGate();
        sendP2(x);
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
        enableDevices();
    }
    public void enableDevices()
    {
        InputSystem.EnableDevice(VariableSending.controlnameP2);
        InputSystem.EnableDevice(VariableSending.controlnameP1);
    }
    private void CharacterToGate()
    {
        ui.onGateScreen = true;
        ui.buttonPressedSetAction(characterSelect, "CharSelectToGate", ui.gateGameObject, play);
        turn++;
        ui.onCharacterSelectScreen = false;

    }

    private void nextLevel()
    {
        needMultipleHide();
        uiController.DeviceToCharacterSelection();
        playerInput.GetComponent<PlayerInput>().actions.FindAction("Submit").Enable();
    }

    public void cleanInvoke()
    {
        playerInput.GetComponent<PlayerInput>().actions.FindAction("Submit").Enable();
        CancelInvoke("nextLevel");
    }
    public bool playerSelectDevice()
    {
        if (isReadyToCharacter < 2) return false;
        if (VariableSending.controlnameP1 != null && VariableSending.controlnameP2 != null && VariableSending.controlnameP1 != VariableSending.controlnameP2)
        {
            playerInput.GetComponent<PlayerInput>().actions.FindAction("Submit").Disable();
            //nextLevel();
            Invoke("nextLevel", 1.2f);
            return true;
        }
        else
        {
            if (VariableSending.controlnameP1 != null && VariableSending.controlnameP2 != null)
            {
                needMultiple();
                FMODUnity.RuntimeManager.PlayOneShot("event:/failtojoin");
            }
        }
        return false;
    }
    public void cleanDevices()
    {
        if (VariableSending.controlnameP1 != null) InputSystem.EnableDevice(VariableSending.controlnameP1);
        if (VariableSending.controlnameP2 != null) InputSystem.EnableDevice(VariableSending.controlnameP2);

        cleanInvoke();
        deviceSelection.GetComponent<DeviceSelection>().cleanBoth();
        VariableSending.controlnameP1 = null;
        VariableSending.controlnameP2 = null;
    }

    private void sendP1(int x)
    {
        if (x == 1)
        {
            VariableSending.Player1Character = "FIRE";
        }
        else if (x == 2)
        {
            VariableSending.Player1Character = "AIR";
        }
        else if (x == 3)
        {
            VariableSending.Player1Character = "EARTH";
        }
        else if (x == 4)
        {
            VariableSending.Player1Character = "WATER";
        }
    }
    private void sendP2(int x)
    {
        if (x == 1)
        {
            VariableSending.Player2Character = "FIRE";
        }
        else if (x == 2)
        {
            VariableSending.Player2Character = "AIR";
        }
        else if (x == 3)
        {
            VariableSending.Player2Character = "EARTH";
        }
        else if (x == 4)
        {
            VariableSending.Player2Character = "WATER";
        }
    }
    public void setIdleAnims()
    {
        airNomadAnime.SetTrigger("Idle");
        swapTexture(characters[1], 1, baseMaterials);
        fireNomadAnime.SetTrigger("Idle");
        swapTexture(characters[0], 0, baseMaterials);
        earthNomadAnime.SetTrigger("Idle");
        swapTexture(characters[2], 2, baseMaterials);
        waterNomadAnime.SetTrigger("Idle");
        swapTexture(characters[3], 3, baseMaterials);

    }

    private void controllerViberate()
    {
        InputSystem.pollingFrequency = 120;
        Gamepad.current.SetMotorSpeeds(0.3f, 0.3f);
    }

    private void stopViberate()
    {
        InputSystem.ResetHaptics();
    }


}
