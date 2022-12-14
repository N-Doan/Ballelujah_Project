using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{

    public int GamesPlayed;

    [Header("Texts GameObject")]
    [SerializeField]
    private GameObject playText;
    [SerializeField]
    private GameObject HTPText;
    [SerializeField]
    private GameObject EXText;
    [SerializeField]
    private GameObject CRText;

    [Header("EventSystem")]
    public EventSystem eventSystem;

    [Header("Fading")]
    public FadeToBlack fade;

    [Header("CharacterSelect")]
    [SerializeField]
    public CharacterSelect characterSelect;
    [SerializeField]
    private int CharacterSelectPlayerTurn;

    [Header("HowToPlayText")]
    [SerializeField]
    public HowToPlayText howToPlayText;

    [Header("MainMenu Canvas GameObjects")]
    [SerializeField]
    private GameObject mainMenuGameObject;
    [SerializeField]
    private GameObject creditsGameObject;
    [SerializeField]
    private GameObject characterSelectGameObject;
    [SerializeField]
    private GameObject HowToPlayGameObject;
    [SerializeField]
    public GameObject gateGameObject;
    [SerializeField]
    private GameObject deviceSelection;

    [Header("MainMenu Button GameObjects")]
    [SerializeField]
    private GameObject FirstButtonMain;
    [SerializeField]
    private GameObject FirstButtonCredits;
    [SerializeField]
    private GameObject FirstButtonHowTo;
    [SerializeField]
    private GameObject FirstCharacter;
    [SerializeField]
    private GameObject p1DeviceBtn;

    [Header("MainMenu Camera Animator")]
    [SerializeField]
    public Animator animator;

    [Header("Gate Animator")]
    [SerializeField]
    public Animator gateLeft;
    [SerializeField]
    public Animator gateRight;

    [Header("Booleans")]
    public bool onCharacterSelectScreen;

    public bool onHowToPlayScreen;

    public bool onCreditsScreen;

    public bool onMainScreen;

    public bool onGateScreen;

    public bool onDeviceSelectScreen;

    [SerializeField]
    private PlayerInput playerInput;

    void Start()
    {
        onGameStart();
        //event System 
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(FirstButtonMain);
        GamesPlayed = GamesPlayed + 1;
        InputSystem.EnableDevice(InputSystem.GetDevice<Keyboard>());
        InputSystem.EnableDevice(InputSystem.GetDevice<Gamepad>());
    }


    private void onGameStart()
    {
        //Bools start up 
        onMainScreen = true;
        onCharacterSelectScreen = false;
        onCreditsScreen = false;
        onHowToPlayScreen = false;
        onDeviceSelectScreen = false;
        howToPlayText.howToPlayStartUp();
        characterSelect.characterHideCursour();
        characterSelect.hideAblityCursor();
        Time.timeScale = 1.0f;

    }


    private void Update()
    {
        checkForBackButtons();
    }


    public void buttonSelect()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/button select");
    }

    public void buttonBack()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Back select");
    }


    private void checkForBackButtons()
    {
        var keyboard = InputSystem.GetDevice<Keyboard>();
        var gamepad = InputSystem.GetDevice<Gamepad>();


        if (onCharacterSelectScreen && gamepad.buttonEast.isPressed || onCharacterSelectScreen && keyboard.escapeKey.isPressed)
        {
            CharacterToMain();
        }
        if (onHowToPlayScreen && gamepad.buttonEast.isPressed || onHowToPlayScreen && keyboard.escapeKey.isPressed)
        {
            HowToToMain();
        }
        if (onCreditsScreen && gamepad.buttonEast.isPressed || onCreditsScreen && keyboard.escapeKey.isPressed)
        {
            CreditsToMain();
        }
        if (onGateScreen && gamepad.buttonEast.isPressed || onGateScreen && keyboard.escapeKey.isPressed)
        {
            BacktoCharacter();
        }
        if (onDeviceSelectScreen && gamepad.buttonEast.isPressed || onDeviceSelectScreen && keyboard.escapeKey.isPressed)
        {
            DeviceToMain();
        }
    }

    public void mainToC()
    {
        onMainScreen = false;
        OnSelectButton("mainToCredits");
        onCreditsScreen = true;
    }
    public void mainToH()
    {
        onMainScreen = false;
        OnSelectButton("mainToHTP");
        onHowToPlayScreen = true;
    }
    public void mainToD()
    {
        onMainScreen = false;
        OnSelectButton("mainToDevice");
        onDeviceSelectScreen = true;
    }

    public void DeviceToMain()
    {
        //clean all the devices
        characterSelect.cleanDevices();
        characterSelect.needMultipleHide();
        onDeviceSelectScreen = false;
        buttonPressedSetAction(deviceSelection, null, mainMenuGameObject, FirstButtonMain);
        onMainScreen = true;
    }

    public void DeviceToCharacterSelection()
    {
        //Changes Main menu to credits 
        onDeviceSelectScreen = false;
        OnSelectButton("DeviceToChar");
        characterSelect.showAblityCursor();
        onCharacterSelectScreen = true;

        InputSystem.DisableDevice(VariableSending.controlnameP2);
        if (!characterSelect.GetP1GamePad())
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
    public void CreditsToMain()
    {
        //Changes Credits to Main Menu
        onMainScreen = true;
        buttonPressedSetAction(creditsGameObject, "CreditsToMain", mainMenuGameObject, FirstButtonMain);
        onCreditsScreen = false;
    }
    public void HowToToMain()
    {
        //Changes HowToPlay to Main Menu
        howToPlayText.pageCount = 0;
        howToPlayText.displayText();
        onMainScreen = true;
        buttonPressedSetAction(HowToPlayGameObject, "HTPToMain", mainMenuGameObject, FirstButtonMain);
        onHowToPlayScreen = false;
    }

    public void CharacterToMain()
    {
        //clean all the devices
        characterSelect.cleanDevices();

        //Changes Character Select to Main Menu

        if (characterSelect.turn == 1)
        {
            onMainScreen = true;
            OnSelectButton("CharSelectToMain");
            onCharacterSelectScreen = false;
            characterSelect.hideAblityCursor();
        }
        else if (characterSelect.turn == 2)
        {
            characterSelect.turn = characterSelect.turn - 1;
        }
    }
    public void BacktoCharacter()
    {
        //clean up things 
        characterSelect.cleanDevices();

        //Changes CharacterSelect 
        onGateScreen = false;
        OnSelectButton("BackToCharacter");
        characterSelect.showAblityCursor();
        onCharacterSelectScreen = true;
    }

    public void playGameSceneChange()
    {
        onGateScreen = false;
        gateGameObject.SetActive(false);
        StartCoroutine(waitForStartGameAnime());
    }

    public void playTutSceneChange()
    {
        onGateScreen = false;
        gateGameObject.SetActive(false);
        StartCoroutine(waitForStartTutAnime());
    }

    private void OnSelectButton(string Current)
    {
        //string formerString = Current;

        if (Current == "mainToCredits")
        {
            buttonPressedSetAction(mainMenuGameObject, "MainToCredits", creditsGameObject, FirstButtonCredits);
        }
        else if (Current == "mainToDevice")
        {
            buttonPressedSetAction(mainMenuGameObject, null, deviceSelection, p1DeviceBtn);
        }
        else if (Current == "mainToHTP")
        {
            buttonPressedSetAction(mainMenuGameObject, "MainToHTP", HowToPlayGameObject, FirstButtonHowTo);
        }

        else if (Current == "DeviceToChar")
        {
            characterSelect.turn = characterSelect.turn + 1;
            buttonPressedSetAction(deviceSelection, "MainToCharSelect", characterSelectGameObject, FirstCharacter);
        }
        else if (Current == "CharSelectToMain")
        {
            characterSelect.turn = characterSelect.turn - 1;
            characterSelect.needMultipleHide();
            characterSelect.setIdleAnims();
            characterSelect.characterHideCursour();
            characterSelect.showAblityCursor();
            buttonPressedSetAction(characterSelectGameObject, "CharSelectToMain", mainMenuGameObject, FirstButtonMain);
            characterSelect.setCursorPositions();
        }
        else if (Current == "BackToCharacter")
        {
            characterSelect.turn = 1;
            characterSelect.setIdleAnims();
            buttonPressedSetAction(gateGameObject, "GateToCharSelect", characterSelectGameObject, FirstCharacter);
            characterSelect.characterShowCursour();
        }
    }

    public void buttonPressedSetAction(GameObject canvas, string anim, GameObject canvasChange, GameObject button)
    {
        canvas.SetActive(false);
        if (anim == null)
        {

        }
        else
        {
            animator.SetTrigger(anim);
        }

        canvasChange.SetActive(true);
        eventSystem.SetSelectedGameObject(button);
    }



    public void exitApp()
    {
        InputSystem.ResetHaptics();
        Application.Quit();
    }

    public IEnumerator waitForStartGameAnime()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/dooropen");
        playGate();
        animator.SetBool("StartGame", true);
        yield return new WaitForSeconds(1.2f);
        fade.fading(0);
    }

    public IEnumerator waitForStartTutAnime()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/dooropen");
        playGate();
        animator.SetBool("StartGame", true);
        yield return new WaitForSeconds(1.2f);
        fade.fading(1);
    }
    public void playGate()
    {
        gateLeft.SetTrigger("OpenLeftGate");
        gateRight.SetTrigger("OpenRightGate");
    }

    public void SelectPlay()
    {
        //Event Trigger for moving the Cursor GameObjects 

        playText.SetActive(true);

    }
    public void deSelectPlay()
    {
        //Event Trigger for moving the Cursor GameObjects 


        playText.SetActive(false);

    }
    public void SelectHTP()
    {
        //Event Trigger for moving the Cursor GameObjects 

        HTPText.SetActive(true);


    }
    public void deSelectHTP()
    {

        HTPText.SetActive(false);


    }
    public void SelectCredits()
    {

        CRText.SetActive(true);


    }
    public void deSelectCredits()
    {
        CRText.SetActive(false);

    }

    public void SelectExit()
    {

        EXText.SetActive(true);


    }
    public void deSelectExit()
    {

        EXText.SetActive(false);


    }
}
