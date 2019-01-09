using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChangeForm : MonoBehaviour {
    private List<Collider> onlyShake;
    public GameObject camera;
    public GameObject newForm;
    public float shakeRadius;
    public float pullRadius;

    // Use this for initialization
    void Start () {
        newForm.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void FixedUpdate()
    {
        onlyShake = Physics.OverlapSphere(camera.transform.position, shakeRadius).ToList();
        List<Collider> forPull = Physics.OverlapSphere(camera.transform.position, pullRadius).ToList();
        foreach (Collider p in forPull)
        {
            onlyShake.Remove(onlyShake.First(x => x.name == p.name));
        }

        foreach (Collider collider in onlyShake)
        {
            forPull.Remove(collider);
            if (collider.name == gameObject.name)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(true);
                }
                newForm.SetActive(false);
            }
        }

        foreach (Collider collider in forPull)
        {
            if (collider.name == gameObject.name)
            {
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActive(false);
                }
                newForm.SetActive(true);
            }
        }
    }
}
