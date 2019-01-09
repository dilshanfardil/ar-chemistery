using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToAttomView : MonoBehaviour {
	public GameObject Object1;
    public GameObject Object2;

	// Use this for initialization
	void start(){
		Object1.SetActive(false);
	}

	public void ButtonOneClicked () {
		Debug.Log("Warning .. BUtton");
        Object1.SetActive(false);
        Object2.SetActive(true);
			 
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
