using UnityEngine;
using System.Collections;

public class Player {

	public int numberOfUnits = 1;
	public int player = 1;
	public bool hasWon = false;

	public void setPlayer(int playerNum) {
		player = playerNum;
	}

	public int getNumberOfControlledUnits() {
		int numUnits = 0;

		GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
		foreach(GameObject i in units) {
			if(i.GetComponent<UnitBase>() != null && i.GetComponent<UnitBase>().controller == this.player) {
				numUnits++;			}
		}
		return numUnits;
	}
}
