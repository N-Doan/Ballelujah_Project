using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipperTutorialCol : MonoBehaviour
{
    [SerializeField]
    private TutorialManager tutorial;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pinball") && GetComponent<flipperScript>().isUp)
        {
            tutorial.onFlipperHit();
        }
    }
}
