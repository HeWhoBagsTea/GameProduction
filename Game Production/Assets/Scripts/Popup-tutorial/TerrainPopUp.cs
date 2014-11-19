using UnityEngine;
using System.Collections;

public class TerrainPopUp : MonoBehaviour {

	private bool hasEntered = false;
	private bool stayActive = false;
	
	void OnMouseEnter() {
		hasEntered = true;
	}
	
	void OnMouseExit() {
		hasEntered = false;
	}		


	
	void OnGUI() {
		GUI.skin.button.fontSize = Screen.height/50;

		if (hasEntered && NewGameController.currentPlayer.isTurn && NewGameController.currentPlayer.specialTerrain && !NewGameController.currentPlayer.isLookingAtPopup) {
			if(NewGameController.selectedUnit.controller == NewGameController.currentPlayer) {
				stayActive = true;
				NewGameController.currentPlayer.isLookingAtPopup = true;
			}
		}
		
		if (stayActive && NewGameController.currentPlayer.specialTerrain) {
			GUI.skin.box.wordWrap = true;
			GUI.Box(new Rect(25, 200, 500, 300), "Terrain can impede a unit's movement, but can also grant them temporary buffs or debuffs uniwue to the unit.b");

			if(GUI.Button(new Rect(390, 450, 125, 35), "Close"))
			{
				NewGameController.currentPlayer.specialTerrain = false;
				NewGameController.currentPlayer.isLookingAtPopup = false;
			}
		}
		
		if (stayActive && NewGameController.selectedUnit == null) {
			stayActive = false;
			NewGameController.currentPlayer.isLookingAtPopup = false;
		}

	}
}


