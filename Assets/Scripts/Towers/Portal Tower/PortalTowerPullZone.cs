using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTowerPullZone : MonoBehaviour
{
    [SerializeField]
    private float pullForce = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    //Applies attracting force to pinballs. Attracts pinballs towards the center of the portal tower
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Pinball"))
        {
            Rigidbody r = other.GetComponent<Rigidbody>();
            Vector3 direction = gameObject.transform.position - other.gameObject.transform.position;
            r.AddForce(direction * pullForce, ForceMode.VelocityChange);
        }
    }
}
