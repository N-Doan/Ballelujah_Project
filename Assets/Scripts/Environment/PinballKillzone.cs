using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinballKillzone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pinball"))
        {
            other.GetComponent<Pinball>().resetPinball();
        }
    }
}
