using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDebugDataOnCollision : MonoBehaviour
{
    [SerializeField]
    private float rayDrawDistance = 0.1f;
    [SerializeField]
    private float rayLifeTime = 4f;
    [SerializeField]
    private bool showDebugMessages = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (showDebugMessages)
        {
            Debug.Log("IMPACT AT " + collision.contacts[0].point);
        }

        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal * rayDrawDistance, Color.red, rayLifeTime);
    }

}
