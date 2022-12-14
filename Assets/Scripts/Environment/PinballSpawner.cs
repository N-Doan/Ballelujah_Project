using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] p1SpawnLocs;

    [SerializeField]
    private Transform[] p2SpawnLocs;

    [SerializeField]
    private GameObject pinballPrefab;

    [SerializeField]
    private LevelManager levelManager;

    //instantiates a pinball on a side based off the side int passed
    //Sets location to a random transform in the spawnLocs array
    public void spawnPinball(bool side)
    {
        //PLAYER 1
        if(side == true)
        {
            GameObject p = Instantiate(pinballPrefab, gameObject.transform, false);
            Transform p1SpawnLoc = p1SpawnLocs[Random.Range(0, p1SpawnLocs.Length)];
            p.GetComponent<Pinball>().setRespawnLocations(p1SpawnLocs);
            p.transform.position = p1SpawnLoc.position;
            levelManager.incPlayerPinballs(1, true);
            p.GetComponent<Pinball>().setSide(true);

        }
        //PLAYER 2
        else if(side == false)
        {
            GameObject p = Instantiate(pinballPrefab, gameObject.transform, false);
            Transform p2SpawnLoc = p2SpawnLocs[Random.Range(0, p2SpawnLocs.Length)];
            p.GetComponent<Pinball>().setRespawnLocations(p2SpawnLocs);
            p.transform.position = p2SpawnLoc.position;
            levelManager.incPlayerPinballs(1, false);
            p.GetComponent<Pinball>().setSide(false);
        }
        else
        {
            Debug.LogError("INVALID SIDE PASSED");
        }
    }

}
