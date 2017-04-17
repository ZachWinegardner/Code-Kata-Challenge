using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class ItemAttributes : MonoBehaviour {
	public static GameObject SpawnedItem;
	public static int ItemLevel; 
	public static int TypeofItem; 
	public static float AttackSpeed; 
	public static float DamagePerHit; 
	public float ChancetoHit; 
	public float DamagePerSec; 
	public static float BuyCost; 
	public static float Sellcost; 
	private float ChanceRand; 
	private float BoostRand;
	private int BoostResultName;  
	public static float SBoost = 0f; 
	public static float HBoost = 0f;
	public static float IBoost = 0f;
	private GameObject Player; 

	void Start () {
		Player = GameObject.Find ("Player");
		SpawnedItem = gameObject; 
		SBoost = 0f; 
		HBoost = 0f; 
		IBoost = 0f;
		//Establish Stats-------------------
		ItemLevel = Random.Range (1, 11); 
		TypeofItem = Random.Range (1, 7);
		AttackSpeed = Random.Range (0.5f, 2.0f); 
		DamagePerHit = Random.Range (5, 10) * ItemLevel; 
		BuyCost = ((ItemLevel + DamagePerHit) + (ItemLevel * 1f)) * 100f; 
		Sellcost = (BuyCost * Random.Range (2, 6)) / 10f; 
		DamagePerSec = Random.value / AttackSpeed; 

		//Set Color of Item Sprite------------------------------
		SpriteRenderer SR = GetComponent<SpriteRenderer>(); 
		SR.enabled = true; 
		Vector4 ClassColor;
		if (TypeofItem <=3)
			ClassColor = new Vector4 (0f, 0f, 1f, 1f); 
		else 
			ClassColor = new Vector4 (1f, 0f, 0f, 1f);
		SR.color = ClassColor; 

		//Determine Attr Boost-----------------------------------
		// Calculates chances, calcs boost to random attr, sets index to send to naming array function
		// BoostResultName is an int varaible that refers to the index of the naming array that will be set in the ModNames function
		// Each value corresponds to "of Strength" , "of Health" ect, in the attr mod names array
		BoostRand = Random.value; 
		//20% chance of boosting
		if (BoostRand > (0.8)) {
			// Booster determines which attr to boost, BoostValue calcs the amount of boost
			int Booster = Random.Range (1, 4); 
			float BoostValue = Random.Range (1, 6) + ItemLevel / 2; 

			if (Booster == 1) {
				BoostResultName = 0; 
				SBoost = BoostValue;
			}

			if (Booster == 2) { 
				BoostResultName = 1; 
				HBoost = BoostValue;
			}

			if (Booster == 3) {
				BoostResultName = 2; 
				IBoost = BoostValue;
			} 
			 
		} else {
			BoostResultName = 3;
			SBoost = 0f; 
			HBoost = 0f; 
			IBoost = 0f; 
		}
		
		//Basic Names Array--------------------------------------------------------------
		// int variable picks random 0-3 index and is used in the "Identify Type" block that names each type of item
		int index = Random.Range (0, 4); 
		string[] HeadNames = {"Helm", "Hat", "Dunce Cap", "Ear Muffs"  }; 
		string[] ChestNames = {"Cuirass", "Bat Suit", "Tunic", "Mithril"  };
		string[] FeetNames = {"Boots", "Sandals", "Air Jordans", "Stockings" };
		string[] MeleeNames = {"Cutlass", "Rolled up Newspaper", "Mace", "Lead Pipe" };
		string[] RangeNames = {"Crossbow", "Slingshot", "Rubberband Gun", "Javelin" };
		string[] MagicNames = {"Elder Wand", "Totem", "Casting Glove", "Staff" };
		
		// Identify Type------------------------
		// checks whay the type is, then sets the name by looking at the propper array and then the random index
		if (TypeofItem == 1) {
			this.name = HeadNames[index]; 
			SendMessage ("SetTypeUI", 1); 
		}
		
		if (TypeofItem == 2) {
			this.name = ChestNames[index];
			SendMessage ("SetTypeUI", 2);
		}

		if (TypeofItem == 3) {
			this.name = FeetNames[index]; 
			SendMessage ("SetTypeUI", 3);
		}

		if (TypeofItem == 4) {
			this.name = MeleeNames[index];
			SendMessage ("SetTypeUI", 4);
		}

		if (TypeofItem == 5) {
			this.name = RangeNames[index];
			SendMessage ("SetTypeUI", 5);
		}

		if (TypeofItem == 6) {
			this.name = MagicNames[index];
			SendMessage ("SetTypeUI", 6);
		}
		
		// Name Level Mods ------------------------------------------
		//Checks level status and sends index to naming array function along with previously determined Boost outcome
		if (ItemLevel <= 3)
			ModLevelName (5, BoostResultName); 

		if (ItemLevel > 3 && ItemLevel < 7 && TypeofItem >= 4)
			ModLevelName (0, BoostResultName);
		
		if (ItemLevel > 6 && ItemLevel < 10 && TypeofItem >= 4)
			ModLevelName (1, BoostResultName);

		if (ItemLevel > 3 && ItemLevel < 7 && TypeofItem <= 3)
			ModLevelName (2, BoostResultName);
		
		if (ItemLevel > 6 && ItemLevel < 10 && TypeofItem <= 3)
			ModLevelName (3, BoostResultName);

		if (ItemLevel == 10)
			ModLevelName (4, BoostResultName);

		//Let the Generator script know to start destroying the previous object
		ItemGeneratorScript.FirstGen = true; 
		gameObject.tag = "LatestSpawned";
		Player.SendMessage ("SetAttr");
		SendMessage ("SetUIText");
	}

	void Update () {
		//Strike , Check Hit --------------------------------
		if (Input.GetKeyDown (KeyCode.F) && TypeofItem >=4) {
			//Choose rand 0 to 1
			ChanceRand = Random.value; 
			ChancetoHit = ChanceRand + (ItemLevel * 0.01f);
			// check if its above .2 (true 80% of time plus the chance to hit value) 
			//Coroutines enable and disable text with a half sec interval between
			if (ChancetoHit > 0.2f) {
				StartCoroutine (FlashHit (Hit:true)); 
			} else
				StartCoroutine (FlashHit (Hit:false));  
		}
	}

	void ModLevelName (int index1, int index2){
		
		//Mod Names Array
		// Reads index from block that calls function and sets corresponding string from array
		string [] ModNames = {"Mighty ", "Epic ", "Reinforced ", "MasterForged ", "Legendary ", ""};
		string[] BoostNames = { " of Strength", " of Healing", " of Intellect", ""}; 
		this.name = ModNames[index1] + this.name + BoostNames[index2];  
	}

	//Flashes Hit or Miss when F is pressed and the Chance to hit is evaluated
	IEnumerator FlashHit (bool Hit){
		if (Hit == true) {
			Text HitText = GameObject.Find ("HitText").GetComponent<Text> (); 
			HitText.enabled = true; 
			yield return new WaitForSeconds (0.3f); 
			HitText.enabled = false; 
		}
		else {
			Text MissText = GameObject.Find ("MissText").GetComponent<Text> (); 
			MissText.enabled = true; 
			yield return new WaitForSeconds (0.4f); 
			MissText.enabled = false; 
		}
		yield return null; 
	}	
}
