using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{

	public int numberOfUnits = 1;
	public int playerID;
	public Material playerColor;
	public bool hasLost = false;

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
}
