using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EarthNomadTowerBlocker : MonoBehaviour
{
    [SerializeField]
    private int blockerHealth = 4;
    [SerializeField]
    private Collider blockerCollider;

    //Ridley
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private VisualEffect poof;

    private float force = 6.0f;

    MonoBehaviour[] scripts;
    Collider[] cols;

    // Start is called before the first frame update
    void Start()
    {
        //Ridley
        //cloud.SetActive(false);
        //poof = cloud.GetComponentInChildren<VisualEffect>();

        Transform parent = gameObject.GetComponentInParent<Transform>();
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x / parent.lossyScale.x, gameObject.transform.localScale.y / parent.lossyScale.y, gameObject.transform.localScale.z / parent.lossyScale.z);
        scripts = gameObject.GetComponentsInParent<MonoBehaviour>();
        cols = gameObject.GetComponentsInParent<Collider>();
        foreach(MonoBehaviour s in scripts)
        {
            s.enabled = false;
        }
        foreach(Collider c in cols)
        {
            c.enabled = false;
        }
        blockerCollider.enabled = true;
    }

    private void reenableScripts()
    {
        foreach (MonoBehaviour s in scripts)
        {
            s.enabled = true;
        }
        foreach (Collider c in cols)
        {
            c.enabled = true;
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pinball"))
        {
            Vector3 norm = collision.contacts[0].normal;
            Vector3 dir = this.transform.position - norm;//collision.gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);

            //Ridley's code, kiss my ass
            poof.Play();

            blockerHealth -= 1;
            if(blockerHealth <= 0)
            {
                reenableScripts();
            }
        }
    }
}
