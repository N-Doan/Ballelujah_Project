using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthNomad : BaseCharAbility
{
    [SerializeField]
    private GameObject earthNomadBlockerPref;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void activateAbility(bool side)
    {


        if (!side)
        {
            if (!p2AbilityActivated)
            {
                p2AbilityActivated = true;
                List<GameObject> openSpots = new List<GameObject>(GlobalVariableStorage.Instance.p1OccupiedSpots);
                if (openSpots.Count > 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int rand = Random.Range(0, openSpots.Count);
                        Instantiate(earthNomadBlockerPref, GlobalVariableStorage.Instance.p1OccupiedSpots[rand].transform);
                        openSpots.RemoveAt(rand);
                        //GlobalVariableStorage.Instance.p1OccupiedSpots[Random.Range(0,GlobalVariableStorage.Instance.p2OccupiedSpots.Count)];
                    }
                }
                else
                {
                    for (int i = 0; i < openSpots.Count; i++)
                    {
                        //int rand = Random.Range(0, GlobalVariableStorage.Instance.p2OccupiedSpots.Count);
                        Instantiate(earthNomadBlockerPref, GlobalVariableStorage.Instance.p1OccupiedSpots[i].transform);
                        //openSpots.RemoveAt(rand);
                        //GlobalVariableStorage.Instance.p1OccupiedSpots[Random.Range(0,GlobalVariableStorage.Instance.p2OccupiedSpots.Count)];
                    }
                }
                FMODUnity.RuntimeManager.PlayOneShot("event:/Earth Nomad test");
                characterAbliitesUI.updateSlidersP2();
                StartCoroutine(abilityCooldown(side));

            }
        }
        else
        {
            if (!p1AbilityActivated)
            {
                p1AbilityActivated = true;
                List<GameObject> openSpots = new List<GameObject>(GlobalVariableStorage.Instance.p2OccupiedSpots);
                if (openSpots.Count > 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int rand = Random.Range(0, openSpots.Count);
                        Instantiate(earthNomadBlockerPref, GlobalVariableStorage.Instance.p2OccupiedSpots[Random.Range(0, GlobalVariableStorage.Instance.p2OccupiedSpots.Count)].transform);
                        //GlobalVariableStorage.Instance.p1OccupiedSpots[Random.Range(0,GlobalVariableStorage.Instance.p2OccupiedSpots.Count)];
                        openSpots.RemoveAt(rand);
                    }
                }
                else
                {
                    for (int i = 0; i < openSpots.Count; i++)
                    {
                        //int rand = Random.Range(0, GlobalVariableStorage.Instance.p2OccupiedSpots.Count);
                        Instantiate(earthNomadBlockerPref, GlobalVariableStorage.Instance.p2OccupiedSpots[i].transform);
                        //openSpots.RemoveAt(rand);
                        //GlobalVariableStorage.Instance.p1OccupiedSpots[Random.Range(0,GlobalVariableStorage.Instance.p2OccupiedSpots.Count)];
                    }
                }
                FMODUnity.RuntimeManager.PlayOneShot("event:/Earth Nomad test");
                characterAbliitesUI.updateSlidersP1();
                StartCoroutine(abilityCooldown(side));
            }
        }
            
        }
    }
