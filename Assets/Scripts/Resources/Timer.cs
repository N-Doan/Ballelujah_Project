using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Game Timer : default to 5 mintues
/// note: UIManager should be the one updating the timer text
/// </summary>

public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timeCount; 
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private LevelManager level;

    private float timeHolder;
    private void Start()
    {
        timeHolder = timeCount;
    }
    // Update is called once per frame
    void Update()
    {
        if(timeCount>0)
        {
            timeCount -= Time.deltaTime;
        }
        else
        {
            timeCount = 0;
            //game end
            level.gameOver();
        
        }
        displayTime(timeCount);
    }

    private void displayTime(float timeDisplay)
    {
        if(timeDisplay<0)
        {
            timeDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeDisplay / 60);
        float seconds = Mathf.FloorToInt(timeDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    //reset timer
    public void resetTimer()
    {
        timeCount = timeHolder;
    }
}
