using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetter : MonoBehaviour {

	public Text NameText; 
	public Text LevelText; 
	public Text DPHText; 
	public Text SpeedText; 
	public Text BuyText; 
	public Text SellText; 
	public Text TypeText; 
	public Text SText; 
	public Text HText; 
	public Text IText; 

	private int Level; 
	private int Type; 
	private float Speed; 
	private float DPH; 
	private float Buy;
	private float Sell;
	private GameObject Item; 

	public Color Default; 
	public Color GreenText;
	public Color RedText;
	public Color PurpleText;

	void SetUIText (){

		Level = ItemAttributes.ItemLevel;
		Type = ItemAttributes.TypeofItem;  
		Buy = ItemAttributes.BuyCost; 
		Sell = ItemAttributes.Sellcost;
		Item = ItemAttributes.SpawnedItem; 

		// Sets color of UI text according to level 
		NameText.text = Item.name.ToString (); 
		if (Level <= 3)
			NameText.color = Default;  
		if (Level > 3 && Level < 7)
			NameText.color = GreenText; 
		if (Level >= 7 && Level < 10)
			NameText.color = RedText; 
		if (Level ==10)
			NameText.color = PurpleText; 

		// Sets UI text of the generatied item's variables
		LevelText.text = Level.ToString (); 
		BuyText.text = "Worth: " + Buy.ToString ("F"); 
		SellText.text = "Sell to Merchant: " + Sell.ToString ("F"); 
	
	}

	void SetTypeUI (int index){
		
		Type = ItemAttributes.TypeofItem; 
		Speed = ItemAttributes.AttackSpeed;
		DPH = ItemAttributes.DamagePerHit;
		//Changes value name depending on armor or weapon
		//The block that checks the type in ItemAttributes sends a message to run this function with an argument that holds the propper index
		string[] TypeName = {"", "Head", "Chest", "Feet", "Melee", "Ranged", "Magic"}; 
		if (Type > 3) {
		SpeedText.text = "Speed: " + Speed.ToString ("F");
		DPHText.text = "Damage: " + DPH.ToString ();
		TypeText.text = TypeName [index] + " Weapon"; 
		}
		else {
		DPHText.text = "Armor: " + DPH.ToString ();
		SpeedText.text = "Agility: " + Speed.ToString ("F");
		TypeText.text = TypeName [index] + " Armor"; 
		}
	}
}
