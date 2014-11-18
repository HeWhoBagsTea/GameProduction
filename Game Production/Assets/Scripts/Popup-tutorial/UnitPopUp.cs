using UnityEngine;
using System.Collections;

public class UnitPopUp : MonoBehaviour 
{
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
			if(NewGameController.currentPlayer.isTurn && !NewGameController.currentPlayer.isLookingAtPopup && NewGameController.currentPlayer.fistTimeSelectUnit) {
				stayActive = true;
				NewGameController.currentPlayer.isLookingAtPopup = true;
			}
		}

		if (stayActive && NewGameController.currentPlayer.fistTimeSelectUnit && NewGameController.currentPlayer == this.gameObject.GetComponent<UnitBase>().controller) {
			GUI.Box(new Rect(10, 10, 500, 300), "Green to move red 2");
			if(GUI.Button(new Rect(20, 400, 100, 20), "Close"))
			{
				NewGameController.currentPlayer.fistTimeSelectUnit = false;
				NewGameController.currentPlayer.isLookingAtPopup = false;
			}
		}
	}
}
