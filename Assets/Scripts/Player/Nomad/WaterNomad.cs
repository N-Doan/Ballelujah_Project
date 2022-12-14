using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterNomad : BaseCharAbility
{
    //Columns that are used to check for towers on each players side
    [SerializeField]
    GameObject[] player1Cols;
    [SerializeField]
    GameObject[] player2Cols;
    //Used to determine length of that ability plays o nthe board
    [SerializeField]
    private float abilityDurCount = 5;
    //Tracks number of columns per side
    private int colsLength;

    //Plays on Start
    public void Start()
    {
        colsLength = player1Cols.Length;
    }
    //Used to call selectRandomRow method and pass on the side bool
    public override void activateAbility (bool side)
    {
        if (side)
        {
            if (!p1AbilityActivated)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/waterNomad");

                p1AbilityActivated = true;
                selectRandomRow(side);
                characterAbliitesUI.updateSlidersP1();
            }
        }
        else
        {
            if (!p2AbilityActivated)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/waterNomad");

                p2AbilityActivated = true;
                selectRandomRow(side);
                characterAbliitesUI.updateSlidersP2();
            }
        }

            
            
    }

    //Selects a random rows then passes the row selected and the side bool to the disableTowers method
    private void selectRandomRow(bool side)
    {
        int randomCol = Random.Range(0, colsLength-1);

        disableTowers(side, randomCol);
    }

    //Activates the selected col and calls the disableTowers method in it to disable all of the scripts on the towers in
    //that column
    private void disableTowers (bool side, int selectedCol)
    {
        if (side == false)
        {
            player1Cols[selectedCol].SetActive(true);
            player1Cols[selectedCol].GetComponent<WaterCol>().DisableTowers();
            StartCoroutine(abilityDuration(side, selectedCol));
        }
        else
        {
            player2Cols[selectedCol].SetActive(true);
            player2Cols[selectedCol].GetComponent<WaterCol>().DisableTowers();
            StartCoroutine(abilityDuration(side, selectedCol));
        }
    }

    //Coroutine to disable the effect after a specified duration
    IEnumerator abilityDuration (bool side, int selectedCol)
    {
        yield return new WaitForSeconds(abilityDurCount);
        if (side == false)
        {
            player1Cols[selectedCol].GetComponent<WaterCol>().EnableTowers();
        }
        else
        {
            player2Cols[selectedCol].GetComponent<WaterCol>().EnableTowers();
        }
        StartCoroutine(abilityCooldown(side));
        StopCoroutine(abilityDuration(side, selectedCol));
    }
}
