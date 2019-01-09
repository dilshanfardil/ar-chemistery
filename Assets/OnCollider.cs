using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OnCollider : MonoBehaviour {
    public GameObject molecule;
    public GameObject atomQuad;
    public GameObject moleculeQuad;
    public GameObject other1;
    public GameObject other2;


    private HashSet<GameAtom> originalAtomParents = new HashSet<GameAtom>();
    private HashSet<GameAtom> removeSet = new HashSet<GameAtom>();
    private HashSet<GameObject> childAtoms = new HashSet<GameObject>();
    private HashSet<string> otherParents = new HashSet<string>();
    public int numToChangeForm=1;
    private bool isH2O=false;
    private List<Collider> onlyShake;

    // Use this for initialization
    void Start()
    {
        moleculeQuad.SetActive(false);
        otherParents.Add(other1.transform.parent.name);
        otherParents.Add(other2.transform.parent.name);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameAtom original in originalAtomParents)
        {
            onlyShake = Physics.OverlapSphere(gameObject.transform.position,MakeH2O.shakeRadius).ToList();
            List<Collider> forPull = Physics.OverlapSphere(gameObject.transform.position,MakeH2O.pullRadius).ToList();
            foreach (Collider p in forPull)
            {
                onlyShake.Remove(onlyShake.First(x => x.name == p.name));
            }
            //Debug.LogWarning(onlyShake.Count);
            bool has = onlyShake.Any(x => x.transform.parent.name == original.Parent.name);

            if (has)
            {
                Debug.Log("I got it leaves ");
                Rigidbody rb = original.Atom.GetComponent<Rigidbody>();
                rb.AddForce(Vector3.zero);
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = true;
                original.Atom.transform.position = original.Parent.transform.position + new Vector3(0f, 0f, -0.3f);
                original.Atom.transform.parent = original.Parent.transform;
                original.Atom.SetActiveRecursively(true);                
                molecule.SetActive(false);
                moleculeQuad.SetActive(false);
                atomQuad.SetActive(true);
                gameObject.SetActive(true);
                foreach (Transform child in gameObject.transform)
                {
                    child.gameObject.SetActiveRecursively(true);                    
                }
                
                isH2O = false;
                removeSet.Add(original);
            }
        }
        foreach (GameAtom rem in removeSet)
        {
            childAtoms.Remove(rem.Atom);
            originalAtomParents.Remove(rem);
        }
        //if (originalAtomParents.Count == 0)
        //{
        //    gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //}
    }
    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            GameObject xObject = contact.otherCollider.gameObject;
            if (childAtoms.Contains(contact.otherCollider.gameObject) || !otherParents.Contains(xObject.transform.parent.name))
            {
                Debug.LogWarning(childAtoms.Contains(contact.otherCollider.gameObject));
                continue;
            }
            if(other1.name==xObject.name || other2.name == xObject.name)
            {
                Debug.LogWarning(collision.collider.name + "  has collided " + originalAtomParents.Count);
                childAtoms.Add(contact.otherCollider.gameObject);
                originalAtomParents.Add(new GameAtom(contact.otherCollider.gameObject, contact.otherCollider.transform.parent.gameObject));
                contact.otherCollider.transform.parent = gameObject.transform;
            }
            
        }

    }


    private void OnCollisionStay(Collision collision)
    {
        Debug.LogWarning("contact size is " + childAtoms.Count + " " + numToChangeForm + "  " + isH2O+" originals "+originalAtomParents.Count);
        if (originalAtomParents.Count == numToChangeForm && !isH2O)
        {
            Debug.LogWarning("contact size is " + collision.contacts.Length + " "+ numToChangeForm+"  " +isH2O);
            //Debug.LogWarning("contact size is " + collision.contacts.Length + "other attoms " + originalAtomParents.Count);
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActiveRecursively(false);               
                //Debug.LogWarning("deactivated " + child.gameObject.active+ "  "+child.gameObject.name);                
            }
            atomQuad.SetActive(false);
            molecule.SetActive(true);
            moleculeQuad.SetActive(true);

            isH2O = true;
        }
    }

    public class GameAtom
    {
        public GameAtom(GameObject _atom, GameObject _parent)
        {
            Atom = _atom;
            Parent = _parent;
        }
        public GameObject Atom { get; set; }

        public GameObject Parent { get; set; }
    }
}
