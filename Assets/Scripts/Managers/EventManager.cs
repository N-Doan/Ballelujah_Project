using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    [SerializeField]
    public bool isTutorial = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(obj: this);
        }
    }

    //EVENTS
    public event Action<int> EOnTowerTriggered;

    public event Action<int> EOnPortalTriggered;

    public event Action<int> EOnFlipperPressed;

    public event Action<int> EOnFlipperReleased;

    public event Action<int> EOnPinballCollision;

    public event Action<int> EOnTowerCreated;

    //METHODS
    public void OnTowerTriggerEnter(int instanceId)
    {
        EOnTowerTriggered?.Invoke(instanceId);
    }

    public void OnPortalTriggerEnter(int instanceId)
    {
        EOnPortalTriggered?.Invoke(instanceId);
    }

    public void OnFlipperUp(int instanceId)
    {
        EOnFlipperPressed?.Invoke(instanceId);
    }

    public void OnFlipperDown(int instanceId)
    {
        EOnFlipperReleased?.Invoke(instanceId);
    }

    public void OnPinballHit(int instanceId)
    {
        EOnPinballCollision?.Invoke(instanceId);
    }

    public void OnTowerBuilt(int instanceId)
    {
        EOnTowerCreated?.Invoke(instanceId);
    }
}
