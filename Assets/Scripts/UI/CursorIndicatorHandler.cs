using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorIndicatorHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject[] xBoxIndicators;

    [SerializeField]
    private GameObject[] keyboardIndicators;
    [SerializeField]
    private GameObject extraEssence;

    //TRUE = CONTROLLER FALSE = KEYBOARD
    public void setActiveIndicators(bool inputType)
    {
        if (inputType)
        {
            foreach(GameObject g in xBoxIndicators)
            {
                g.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject g in keyboardIndicators)
            {
                g.SetActive(true);
            }
        }
    }

    public void setExtraEssence(bool toggle)
    {
        extraEssence.SetActive(toggle);
    }
}
