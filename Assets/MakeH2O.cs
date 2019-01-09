using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MakeH2O : MonoBehaviour {
    public GameObject h1;
    public GameObject h1Quad;
    public GameObject h11;
    public GameObject h11Quad;
    public GameObject o;
    public GameObject h2o;
    
    public static float shakeRadius = 8f;
    public static float pullRadius = 1.5f;
    public float pullForce = 10f;
    public static List<Collider> onlyShake;


    // Use this for initialization
    void Start()
    {
        h2o.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        onlyShake = Physics.OverlapSphere(o.transform.position, shakeRadius).ToList();
        List<Collider> forPull = Physics.OverlapSphere(o.transform.position, pullRadius).ToList();
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
            if ((collider.name == h1.name || collider.name == h11.name) && o.activeSelf)
            {
                Debug.LogWarning("is in pull " + collider.name+ o.activeInHierarchy);

                Vector3 forceDirection = o.transform.position - collider.transform.position;
                collider.GetComponent<Rigidbody>().isKinematic = false;
                collider.GetComponent<Rigidbody>().velocity = (forceDirection * pullForce * 10);
                collider.GetComponent<Rigidbody>().AddForce(forceDirection * pullForce);

                o.GetComponent<Rigidbody>().isKinematic = true;
            }       

        }
        
    }
}
