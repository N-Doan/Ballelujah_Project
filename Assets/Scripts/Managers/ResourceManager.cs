using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class ResourceManager : MonoBehaviour
{
    private int playerEssence = 5;
    public int getPlayerEssence() { return playerEssence; }

    private int playerPoints = 0;
    public int getPlayerPoints() { return playerPoints; }

    [SerializeField]
    private Text playerScoreText;

    [SerializeField]
    private bool side;
    [SerializeField]
    private float playerEssenceGainCooldown = 6f;
    [SerializeField]
    private int playerEssenceGain = 1;
    [SerializeField]
    private int maxEssence = 10;

    [SerializeField]
    private VisualEffect[] essenceVFX;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playerEssenceGainFunc());
        updateEssenceVFX();
    }

    //stop the coroutine for when the game's over
    public void stopEssenceGain()
    {
        StopCoroutine(playerEssenceGainFunc());
    }

    //can also be used to subtract points by passing in a negative number
    public void givePlayerPoints(int p)
    {
        playerPoints += p;
        if(playerPoints < 0) { playerPoints = 0; }
        updateScoreText();
    }

    //only accepts positive numbers that are less than the current essence count of the player
    public void subtractPlayerEssence(int p)
    {
        if(playerEssence < p) 
        { 
            Debug.Log("INVALID SUBTRACT VALUE" + p + "PASSED");
            return;
        }
        playerEssence -= p;
        updateEssenceVFX();
    }

    private void updateScoreText()
    {
        if (side == true)
        {
            playerScoreText.text = playerPoints.ToString();
        }
        else
        {
            playerScoreText.text = playerPoints.ToString();
        }
    }

    private void updateEssenceVFX()
    {
        if(essenceVFX.Length != 0)
        {
            int i;
            for (i = 0; i < playerEssence; i++)
            {
                essenceVFX[i].Play();
            }
            while (i <= 9)
            {
                essenceVFX[i].Stop();
                i++;
            }
        }
    }

    //gives player essence
    private IEnumerator playerEssenceGainFunc()
    {
        for( ; ; )
        {
            yield return new WaitForSeconds(playerEssenceGainCooldown);
            if(playerEssence < maxEssence)
            {
                playerEssence += playerEssenceGain;
                updateEssenceVFX();
            }
        }
    }

    //reset player score and essence counts
    public void resetNums()
    {
        playerEssence = 5;
        playerPoints = 0;
    }
}
