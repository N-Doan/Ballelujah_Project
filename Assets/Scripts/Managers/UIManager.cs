using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    public sceneLoad scenelOAD;
    [SerializeField]
    private GameObject winningScreen;
    [SerializeField]
    private TextMeshProUGUI winnerScore;
   
    [SerializeField]
    private TextMeshProUGUI loserScore;
    [SerializeField]
    private TextMeshProUGUI winnerName;
    [SerializeField]
    private TextMeshProUGUI loserName;
    [SerializeField]
    private TextMeshProUGUI returnText;
    [SerializeField]
    private GameObject victoryImage;
    [SerializeField]
    private GameObject drawimage;

    [SerializeField]
    private LevelManager level;

    [SerializeField]
    private GameObject victoryScreenButton;

    private EventSystem eventSystem;

    [SerializeField]
    private int countDown = 10;

    public string currentLevel;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(victoryScreenButton);
    }

    // Update is called once per frame
    void Update()
    {
        //returnText.text = "Returning to Main Menu in " + level.GameOverTImeCount.ToString() ;
        if(eventSystem.currentSelectedGameObject == null) 
        {
            eventSystem.SetSelectedGameObject(victoryScreenButton);
        }
    }

    /// <summary>
    /// showing winning screen
    /// </summary>
    /// <param name="winScore">win player's score</param>
    /// <param name="loseScore">lose player's score</param>
    /// <param name="winNum">define who is the winner. 0 p1,1 p2, -1 tie</param>
    public void gameOver(int winScore,int loseScore,int winNum)
    {
        winningScreen.SetActive(true);
        //eventSystem.SetSelectedGameObject(victoryScreenButton);
        //p1 win
        if(winNum == 0)
        {
            victoryImage.SetActive(true);
            winnerName.text = "PLAYER ONE";
            loserName.text = "PLAYER TWO";
        }
        //p2 win
        else if(winNum == 1)
        {
            victoryImage.SetActive(true);
            loserName.text = "PLAYER ONE";
            winnerName.text = "PLAYER TWO";
        }
        winnerScore.text = "score: " + winScore.ToString();
        loserScore.text = "score: "+loseScore.ToString();
        //tie
        if (winNum == -1)
        {
            drawimage.SetActive(true);
            winnerName.text = "PLAYER ONE";
            winnerScore.text = "score: " + winScore.ToString();
            loserScore.text = "Score" + winScore.ToString(); ;
            loserName.text = "PLAYER TWO" + winScore.ToString(); ;
        }

       
    }

    
    /// <summary>
    /// back to main menu
    /// reset the game
    /// </summary>
    public void backToMain()
    {
        Debug.Log("BACK TO MAIN");
        winningScreen.SetActive(false);
        level.resetGame();
        backToMainMenu();
    }

    private void backToMainMenu() 
    {
        //SceneManager.LoadScene(0);
        StartCoroutine(scenelOAD.loadAsyncLevel(0));
    }



}
