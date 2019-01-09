using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MakeIon : MonoBehaviour
{
    public GameObject NaElectron;
    public GameObject Na;
    public GameObject Cl;
    public GameObject ClElectron;

    public static float shakeRadius = 8f;
    public static float pullRadius = 1.5f;
    public float pullForce = .10f;
    private List<Collider> onlyShake;
    private string otherParent;

    // Use this for initialization
    void Start()
    {
        ClElectron.SetActive(false);
        otherParent = Na.transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        onlyShake = Physics.OverlapSphere(gameObject.transform.position, shakeRadius).ToList();
        List<Collider> forPull = Physics.OverlapSphere(gameObject.transform.position, pullRadius).ToList();
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
            if (collider.name == Na.name && otherParent == collider.transform.parent.name)
            {
                Debug.LogWarning("im colliding");
                Vector3 forceDirection = Cl.transform.position - collider.transform.position;
                GameObject clone = Instantiate(NaElectron, Cl.transform);
                Rigidbody crb = clone.GetComponent<Rigidbody>();
                crb.isKinematic = false;
                crb.velocity = (forceDirection * pullForce * 10);
                crb.AddForce(forceDirection * pullForce);
                NaElectron.SetActive(false);
            }
        }

    }
}
