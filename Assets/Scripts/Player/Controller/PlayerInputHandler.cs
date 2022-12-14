using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
  
    private PlayerController controlIndex;
    private PlayerInput playerInput;


    private bool isPause;
    private void Awake()
    {
        
        playerInput = GetComponent<PlayerInput>();
        var playerID = FindObjectsOfType<PlayerController>();
        var index = playerInput.playerIndex;
        controlIndex = playerID.FirstOrDefault(p => p.GetPlayerIndex() == index);
    }

    private void Start()
    {
        isPause = false;
    }

    public void OnLeftFlipperUp(CallbackContext context)
    {
       if(controlIndex!=null&&context.performed)
        {
            controlIndex.LeftUp();
        }
    }

    public void OnLeftFlipperDown(CallbackContext context)
    {
        if (controlIndex != null && context.performed)
        {
            controlIndex.LeftDown();
        }
    }

    public void OnRightFlipperUp(CallbackContext context)
    {
        if (controlIndex != null && context.performed)
        {
            controlIndex.RightUp();
        }
    }

    public void OnRightFlipperDown(CallbackContext context)
    {
        if (controlIndex != null && context.performed)
        {
            controlIndex.RightDown();
        }
    }

    public void OnPlaceTower(CallbackContext context)
    {
        if (controlIndex != null && context.started)
        {
            controlIndex.placeOnRight(context.ReadValue<Vector2>());
        }
        if(controlIndex!=null&&context.performed)
        {
            controlIndex.placeTowers(context.ReadValue<Vector2>());
        }

        if(context.canceled)
        {
            controlIndex.isChange = false;
        }
    }

    public void OnSelect(CallbackContext context)
    {
        if (controlIndex != null && context.performed)
        {
            controlIndex.select();
        }
    }
    public void OnTowerRotationLeft(CallbackContext context)
    {
        if (controlIndex != null)
        {
            if (context.performed)
            {
                controlIndex.towerRotationLeft();
            }
        }
    }

    public void OnTowerRotationRight(CallbackContext context)
    {
        if (controlIndex != null)
        {
            if(context.performed)
            {
                controlIndex.towerRotationRight();
            }
        }
    }

    public void OnDelete(CallbackContext context)
    {
        if (controlIndex != null && context.performed)
        {
            controlIndex.towerDeletion();
        }
    }


    //placing tower
    public void OnBumpTower(CallbackContext context)
    {
        if (controlIndex != null)
        {
            controlIndex.placeBumperTower();
        }
    }

    public void OnPortalTower (CallbackContext context)
    {
        if (controlIndex != null)
        {
            controlIndex.placePortalTower();
        }
    }

    public void OnWindTower(CallbackContext context)
    {
        if (controlIndex != null)
        {
            controlIndex.placeWindTower();
        }
    }

    public void OnCannonTower(CallbackContext context)
    {
        if (controlIndex != null)
        {
            controlIndex.placeCannonTower();
        }
    }

    public void OnTowerUpgrade(CallbackContext context)
    {
        if (controlIndex != null && context.started)
        {
            controlIndex.towerUpgrade();
        }
    }

    //to-do: change this to call level manager ---> PauseMenu
    //instead of directly calling
    public void OnPause(CallbackContext context)
    {
        
        if(controlIndex !=null && context.performed)
        {
            controlIndex.PasueOrPlayGame();
        }
    }

    public void OnCharacterAbility(CallbackContext context)
    {
        if (controlIndex != null && context.performed)
        {
            controlIndex.activateCharAbility();
        }
    }
}
