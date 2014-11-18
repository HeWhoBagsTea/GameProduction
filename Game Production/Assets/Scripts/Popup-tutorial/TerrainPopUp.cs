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
		if (hasEntered) {
			if(NewGameController.currentPlayer.isTurn && !NewGameController.currentPlayer.isLookingAtPopup && NewGameController.currentPlayer.specialTerrain){ //&& NewGameController.selectedUnit != null && NewGameController.selectedUnit.controller == NewGameController.currentPlayer) {
				stayActive = true;
				NewGameController.currentPlayer.isLookingAtPopup = true;
			}
		}

		if (stayActive && NewGameController.currentPlayer.specialTerrain && NewGameController.selectedUnit == null && NewGameController.selectedUnit.controller == NewGameController.currentPlayer) {
			GUI.Box(new Rect(10, 10, 500, 300), "Green to move red 3");
			if(GUI.Button(new Rect(20, 400, 100, 20), "Close"))
			{
				NewGameController.currentPlayer.specialTerrain = false;
				NewGameController.currentPlayer.isLookingAtPopup = false;
			}
		}
	}
}
