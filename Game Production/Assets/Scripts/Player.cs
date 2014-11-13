using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{

	public int numberOfUnits = 1;
	public int playerID;
	public Material playerColor;
	public bool hasLost = false;

	public int FoodPool = 10;
	public int LumberPool = 10;
	public int OrePool = 10;

	public Vector4 color;

	void Start() {
		numberOfUnits = getNumberOfControlledUnits ();
	}

	void Update() {
		numberOfUnits = getNumberOfControlledUnits ();

		if (numberOfUnits == 0) {
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
			}
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
