using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//singleton for storing useful variables that're accessed across scripts
public class GlobalVariableStorage : MonoBehaviour
{
    public static GlobalVariableStorage Instance { get; private set; }

    public Transform[] p1AdjacentBuildSpots;
    public Transform[] p2AdjacentBuildSpots;

    [System.NonSerialized]
    public List<GameObject> p1OccupiedSpots = new List<GameObject>();

    [System.NonSerialized]
    public List<GameObject> p2OccupiedSpots = new List<GameObject>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
