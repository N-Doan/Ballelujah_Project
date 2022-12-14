using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SlingShot : MonoBehaviour
{
    [SerializeField]
    private float force = 5;
    //TRUE = LEFT FALSE = RIGHT
    [SerializeField]
    public bool isLeft;

    [SerializeField]
    private Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pinball")
        {
            // ? =if : = else
            if (isLeft ? collision.gameObject.transform.position.z - collision.contacts[0].point.z <= 0.005 : collision.gameObject.transform.position.z - collision.contacts[0].point.z >= 0.005)
            {
                animator.SetTrigger("Bump");

                Vector3 norm = collision.contacts[0].normal;
                Vector3 dir = this.transform.position - norm;//collision.gameObject.transform.position;
                                                             //collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);
                                                             //dir = dir.normalized;

                dir = new Vector3(dir.x + Random.Range(-0.1f, 0.1f), dir.y + Random.Range(-0.1f, 0.1f), dir.z + Random.Range(-0.1f, 0.1f));

                collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                collision.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                //Debug.Log(dir * force);
                collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);
                //collision.gameObject.GetComponent<Rigidbody>().velocity = collision.gameObject.GetComponent<Rigidbody>().velocity * -1;
            }
        }
    }
}
