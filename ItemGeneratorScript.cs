using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ItemGeneratorScript : MonoBehaviour {
	 
	public GameObject Item;
	public static bool FirstGen= false;
	public float Strength;
	public float Health; 
	public float Intellect; 
	public Text SText;
	public Text HText; 
	public Text IText; 

	void Start(){
		SetText (); 
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) { 
			GameObject NewItem = Instantiate (Item, Item.transform.position, Quaternion.identity) as GameObject; 
			NewItem.AddComponent <ItemAttributes>();
			if (FirstGen ==true)
				Destroy (GameObject.FindWithTag ("LatestSpawned")); 
		}

		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit (); 
	}

	void SetAttr (){
		Strength =  ItemAttributes.SBoost + 10f;
		Health =  ItemAttributes.HBoost + 10f; 
		Intellect =  ItemAttributes.IBoost + 10f; 
		SetText (); 
	}

	void SetText (){
		SText.text = "Strength: " + Strength.ToString (); 
		HText.text = "Health: " + Health.ToString (); 
		IText.text = "Intellect: " + Intellect.ToString (); 
	}
		
}
