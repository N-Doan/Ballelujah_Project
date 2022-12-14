using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{

    [SerializeField]
    private string theTag;

    private Collider c;

    private void Start()
    {
        c = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == theTag)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), c);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == theTag)
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<MeshCollider>(), c);
        }
    }

}
