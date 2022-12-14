using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject keyboard;
    [SerializeField]
    private GameObject gamepad;

    private void Start()
    {
        //keyboard.SetActive(false);
        //gamepad.SetActive(false);
    }

    public void setKeyboard()
    {
        keyboard.SetActive(true);
        gamepad.SetActive(false);
        GetComponent<CursorIndicatorHandler>().setActiveIndicators(false);
    }

    public void setGamePad()
    {
        gamepad.SetActive(true);
        keyboard.SetActive(false);
        GetComponent<CursorIndicatorHandler>().setActiveIndicators(true);
    }
}
