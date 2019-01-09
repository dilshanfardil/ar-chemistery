using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour {
	public GameObject hydrogenMole;
    public GameObject hydrogenNormal;
    public GameObject oxygenMole;
    public GameObject oxygenNormal;
    public GameObject h2oMole;
    public GameObject h2oNormal;
    // Use this for initialization

    // public void changeView(int modelToChanage){
    // 	Application.LoadLevel(modelToChanage);
    // }

    void start(){
        hydrogenMole.SetActive(false);
        oxygenMole.SetActive(false);
       // h2oMole.SetActive(false);
        Debug.Log("Warning .. BUtton");
	}

	public void ButtonOneClicked () {			

			if(!hydrogenMole.activeSelf)
			{
                if (hydrogenNormal.activeSelf)
                {
                    hydrogenMole.SetActive(true);
                    hydrogenNormal.SetActive(false);
                    Text txt = transform.Find("Text").GetComponent<Text>();
                    txt.text = "Click to see atom";
                }
                
			}else{                
                hydrogenMole.SetActive(false);
                hydrogenNormal.SetActive(true);
				Text txt = transform.Find("Text").GetComponent<Text>();
				txt.text="Click to see electrones";
			}
            

            if (!h2oMole.activeSelf)
            {
                if (h2oNormal.activeSelf && !oxygenMole.activeSelf)
                {
                    Debug.LogWarning("1");
                    h2oMole.SetActive(true);
                    h2oNormal.SetActive(false);
                    oxygenMole.SetActive(true);
                    oxygenNormal.SetActive(false);
                    Text txt = transform.Find("Text").GetComponent<Text>();
                    txt.text = "Click to see atom";
                }
                else
                {
                    Debug.LogWarning("2");
                    if (!oxygenMole.activeSelf)
                    {
                        oxygenMole.SetActive(true);
                        oxygenNormal.SetActive(false);
                        Text txt = transform.Find("Text").GetComponent<Text>();
                        txt.text = "Click to see atom";
                    }
                    else
                    {
                        oxygenMole.SetActive(false);
                        oxygenNormal.SetActive(true);
                        Text txt = transform.Find("Text").GetComponent<Text>();
                        txt.text = "Click to see electrones";
                    }
                }
                
            }
            else
            {
                if (!h2oNormal.activeSelf)
                {
                    Debug.LogWarning("3");                                
                    h2oMole.SetActive(false);
                    h2oNormal.SetActive(true);
                    oxygenMole.SetActive(false);
                    oxygenNormal.SetActive(false);
                    Text txt = transform.Find("Text").GetComponent<Text>();
                    txt.text = "Click to see electrones";
                }
                else
                {
                    Debug.LogWarning("4");
                }
                
            }
        Debug.LogWarning("Warning .. BUtton");
    }

	public void textChange(string text){
			// Object1.SetActive(false);
            // Object2.SetActive(true);
			Debug.Log("Warning .. BUtton");
			Text txt = transform.Find("Text").GetComponent<Text>();
			txt.text=text;
	}
}
