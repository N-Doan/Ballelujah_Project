using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPullZone : MonoBehaviour
{

    [SerializeField]
    float pushForce = 0.05f;

    public float getPushForce()
    {
        return pushForce;
    }

    public void setPushForce(float p)
    {
        pushForce = p;
    }
    //Pulls pinballs up towards the portals. Very small force
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pinball"))
        {
            Rigidbody r = other.GetComponent<Rigidbody>();
            r.AddForce(this.transform.rotation * Vector3.left * pushForce, ForceMode.VelocityChange);

        }


    }
}
