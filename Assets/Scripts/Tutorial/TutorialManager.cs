using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    [Header("LevelManager")]
    [SerializeField]
    private LevelManager level;

    [Header("Tutorial UI")]
    [SerializeField]
    private TutorialUi tutorialUi;

    [Header("Tutorial Manager")]
    [SerializeField]
    private GameObject P1Cursor;
    [SerializeField]
    private GameObject P2Cursor;
    [SerializeField]
    private PinballSpawner spawner;

    [SerializeField]
    private float TutorialPartTimer = 10.0f;

    private int currentTutorialIndex = 1;

    private GameObject[] playerInputs;

    [SerializeField]
    private GameObject characterUi1;
    [SerializeField]
    private GameObject characterUi2;

    // Start is called before the first frame update
    void Start()
    {
        P2Cursor.SetActive(false);
        P1Cursor.SetActive(false);
        //displayPart0();
        tutorialUi.titleShow();
        level.setIsTutorial(true);
        playerInputs = GameObject.FindGameObjectsWithTag("PlayerInput");
        for(int i = 0; i < playerInputs.Length;i++)
        {
            playerInputs[i].GetComponent<PlayerInput>().actions.FindAction("CharacterAbility").Disable();
            //characterUi1.SetActive(false);
            //characterUi2.SetActive(false);

        }

        StartCoroutine(displayPart0());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator displayPart0()
    {
        //Enable UI text welcoming players to tutorial

        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 0
        tutorialUi.titleHide();
        displayPart1();
    }

    private void displayPart1()
    {
        //Enable UI text for part 1
        
        tutorialUi.setTutorialPromptActive(true,0);
        spawner.spawnPinball(true);
        spawner.spawnPinball(false);
        //Debug.Log("TUTORIAL PART 1");
    }

    //condition for leaving part 1 of tutorial
    public void onFlipperHit()
    {
        //Disable UI text for part 1

        tutorialUi.setTutorialPromptActive(false,0);
        if(currentTutorialIndex == 1)
        {
            StartCoroutine(displayPart2());
            currentTutorialIndex++;
        }
    }

    private IEnumerator displayPart2()
    {
        //Enable UI text for part 2
       
        tutorialUi.setTutorialPromptActive(true,1);
       //Debug.Log("TUTORIAL PART 2");
        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 2
        tutorialUi.setTutorialPromptActive(false, 1);
        currentTutorialIndex++;
        StopCoroutine(displayPart2());
        StartCoroutine(displayPart3());
    }

    private IEnumerator displayPart3()
    {
        //Enable UI text for part 3
        //Debug.Log("TUTORIAL PART 3");
        tutorialUi.setTutorialPromptActive(true, 2);
        
        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 3
        tutorialUi.setTutorialPromptActive(false, 2);
        currentTutorialIndex++;
        StopCoroutine(displayPart3());
        StartCoroutine(displayPart4());
    }

    private IEnumerator displayPart4()
    {
        //Enable UI text for part 4
        tutorialUi.setTutorialPromptActive(true, 3);
        //Debug.Log("TUTORIAL PART 4");
        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 4
        tutorialUi.setTutorialPromptActive(false, 3);
        currentTutorialIndex++;
        StopCoroutine(displayPart4());
        displayPart5();
    }

    private void displayPart5()
    {
        //Enable UI text for part 5
        tutorialUi.setTutorialPromptActive(true, 4);
        P1Cursor.SetActive(true);
        P2Cursor.SetActive(true);
        //Debug.Log("TUTORIAL PART 5");
    }

    //Condition for leaving part 5 of tutorial
    public void onTowerMenuOpened()
    {
        if(currentTutorialIndex == 5)
        {
            currentTutorialIndex++;
            //Disable UI text for part 5
            tutorialUi.setTutorialPromptActive(false, 4);
            displayPart6();
        }

    }

    private void displayPart6()
    {
        //enable UI text for part 6
        tutorialUi.setTutorialPromptActive(true, 5);
        //Debug.Log("TUTORIAL PART 6");
    }

    //Condition for leaving part 6 of tutorial
    public void onTowerBuilt()
    {
        if(currentTutorialIndex == 6)
        {
            //Disable UI text for part 6
            tutorialUi.setTutorialPromptActive(false, 5);
            currentTutorialIndex++;
            displayPart7();
        }
    }

    private void displayPart7()
    {
        //Enable UI text for part 7
        tutorialUi.setTutorialPromptActive(true, 6);
        //Debug.Log("TUTORIAL PART 7");
    }

    public void onUpgradeMenuOpened()
    {
        if (currentTutorialIndex == 7)
        {
            //Disable UI text for part 7
            tutorialUi.setTutorialPromptActive(false, 6);
            currentTutorialIndex++;
            displayPart8();
        }
    }

    private void displayPart8()
    {
        tutorialUi.setTutorialPromptActive(true, 7);
    }

    public void onTowerUpgraded()
    {
        if(currentTutorialIndex == 8)
        {
            //Disable UI text for part 8
            tutorialUi.setTutorialPromptActive(false, 7);
            currentTutorialIndex++;
            displayPart9();
        }
    }

    /*private IEnumerator displayPart8()
    {
        tutorialUi.setTutorialPromptActive(true, 7);
        //Enable UI text for part 8
        Debug.Log("TUTORIAL PART 8");
        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 8
        tutorialUi.setTutorialPromptActive(false, 7);
        currentTutorialIndex++;
        StopCoroutine(displayPart8());
        displayPart9();
    }*/

    private void displayPart9()
    {
        //enable ability
        for (int i = 0; i < playerInputs.Length; i++)
        {
            playerInputs[i].GetComponent<PlayerInput>().actions.FindAction("CharacterAbility").Enable();
        }

        tutorialUi.setTutorialPromptActive(true, 8);
        //Enable UI text for part 9
        //characterUi1.SetActive(true);
        //characterUi2.SetActive(true);
        //Debug.Log("TUTORIAL PART 9");
    }

    public void onNomadAbilityUsed()
    {
        if (currentTutorialIndex == 9)
        {
            //Disable UI text for part 9
            tutorialUi.setTutorialPromptActive(false, 8);
            currentTutorialIndex++;
            StartCoroutine(displayPart10());
        }
    }

    private IEnumerator displayPart10()
    {
        //Enable UI text for part 10
        tutorialUi.setTutorialPromptActive(true, 9);
        //Debug.Log("TUTORIAL PART 10");
        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 10
        tutorialUi.setTutorialPromptActive(false, 9);
        currentTutorialIndex++;
        StopCoroutine(displayPart10());
        StartCoroutine(displayPart11());
    }

    private IEnumerator displayPart11()
    {
        //Enable UI text for part 11
        //Debug.Log("TUTORIAL PART 11");
        tutorialUi.setTutorialPromptActive(true, 10);
        yield return new WaitForSeconds(TutorialPartTimer);
        //Disable UI text for part 11
        tutorialUi.setTutorialPromptActive(false, 10);
        currentTutorialIndex++;
        StopCoroutine(displayPart11());
        tutorialEnd();
    }

    private void tutorialEnd()
    {
        level.setIsTutorial(false);
        SceneManager.LoadSceneAsync("MainMenuWhiteBox");
        //Debug.Log("TUTORIAL IS DONE");
    }
}
