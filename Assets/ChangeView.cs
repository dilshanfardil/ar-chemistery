using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeView : MonoBehaviour {
	public GameObject Object1;
    public GameObject Object2;
	
	// Use this for initialization
	
	public void changeView(int modelToChanage){
		Application.LoadLevel(modelToChanage);
	}

	void start(){
		Object1.SetActive(false);
	}

	public void ButtonOneClicked () {
             Object1.SetActive(false);
             Object2.SetActive(true);
			 Debug.Log("Warning .. BUtton");
    }
}
