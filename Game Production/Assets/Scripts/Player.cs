using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{

	public int numberOfUnits = 1;
	public int playerID;
	public Material playerColor;
	public bool hasLost = false;
	public bool hasCommand = false;

	public int FoodPool = 10;
	public int LumberPool = 2;
	public int OrePool = 2;

	public bool isTurn = false;
	public bool isStartingTurn = true;
	public bool fistTimeSelectUnit = true;
	public bool specialTerrain = true;
	public bool isLookingAtPopup = false;

	public ArrayList ListOfUnits; 


	public Vector4 color;

	void Start() {
		ListOfUnits = new ArrayList();
		numberOfUnits = getNumberOfControlledUnits ();

	}

	void Update() {
		numberOfUnits = getNumberOfControlledUnits ();
		//Debug.Log(this.ListOfUnits.Count);

		bool commander = false;
		foreach(CommanderUnit a in FindObjectsOfType(typeof(CommanderUnit)))
		{
			if(a.controller == this){
				commander = true;
			}
		}

		if(!commander) {
			hasLost = true;
		}
	}

	public string getPlayerColor() {
		return playerColor.name;
	}

	public int getPlayerID() {
		return playerID;
	}

	public Vector4 getColor() {
		return this.color;
	}
	
	public int getNumberOfControlledUnits() {
		int numUnits = 0;

		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		foreach(GameObject i in units) {
			if(i.GetComponent<UnitBase>() != null && i.GetComponent<UnitBase>().controller == this) {
				numUnits++;
				ListOfUnits.Add(i);
			}
			//if(i.GetComponent<UnitBase>().unitClass.Equals("Commander") && ListOfUnits.Count == 0){
			//	Debug.Log(i.GetComponent<UnitBase>().unitClass.ToString());
			//	ListOfUnits.Add(i);
			//}
		}
		return numUnits;
	}

	public int getNumberOfControlledTerrain() {
		int tilesControlled = 0;

		GameObject[] Territory = GameObject.FindGameObjectsWithTag ("Tile");
		foreach (GameObject i in Territory) {
			if (i.GetComponent<TileStandard> () != null && i.GetComponent<TileStandard> ().controller == this) {
				tilesControlled++;
			}		
		}
		return tilesControlled;
	}
}
