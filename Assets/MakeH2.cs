using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeH2 : MonoBehaviour {
    public GameObject H;
    public GameObject HQuad;
    public GameObject otherH;
    public GameObject otherHQuad;

    public static float shakeRadius = 8f;
    public static float pullRadius = 1.5f;
    public float pullForce = .10f;
    private List<Collider> onlyShake;
    private string otherParent;

    // Use this for initialization
    void Start()
    {
        otherParent = otherH.transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        onlyShake = Physics.OverlapSphere(H.transform.position, shakeRadius).ToList();
        List<Collider> forPull = Physics.OverlapSphere(H.transform.position, pullRadius).ToList();
        foreach (Collider p in forPull)
        {
            onlyShake.Remove(onlyShake.First(x => x.name == p.name));
        }

        foreach (Collider collider in onlyShake)
        {
            forPull.Remove(collider);
        }

        foreach (Collider collider in forPull)
        {
            if (collider.name == otherH.name && otherParent==collider.transform.parent.name)
            {
                Vector3 forceDirection = H.transform.position - collider.transform.position;
                Rigidbody crb = collider.GetComponent<Rigidbody>();
                crb.isKinematic = false;
                crb.velocity = (forceDirection * pullForce * 10);
                crb.AddForce(forceDirection * pullForce);
                H.GetComponent<Rigidbody>().isKinematic = true;
            }
            

        }

        
    }
}
