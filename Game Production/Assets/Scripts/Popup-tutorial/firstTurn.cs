using UnityEngine;
using System.Collections;

public class firstTurn : MonoBehaviour {

	//private bool hasEntered = false;
	private bool stayActive = false;

	//void OnMouseEnter() {
	//	hasEntered = true;
	//}
	//
	//void OnMouseExit() {
	//	hasEntered = false;
	//}


	// Use this for initialization
	void OnGUI() {
		//if (hasEntered) {
		//	if(!NewGameController.currentPlayer.isLookingAtPopup && NewGameController.currentPlayer.fistTimeSelectUnit) {
		//		NewGameController.currentPlayer.isLookingAtPopup = true;
		//		stayActive = true;
		//	}
		//}
		//
		//if (stayActive && NewGameController.currentPlayer.fistTimeSelectUnit) {
		//	GUI.Box(new Rect(10, 10, 500, 300), "Green to move red");
		//	if(GUI.Button(new Rect(20, 400, 100, 20), "Close"))
		//	{
		//		NewGameController.currentPlayer.fistTimeSelectUnit = false;
		//	}
		//}
	
		if (NewGameController.currentPlayer.isTurn && NewGameController.currentPlayer.isStartingTurn && !NewGameController.currentPlayer.isLookingAtPopup) {
			stayActive = true;
			NewGameController.currentPlayer.isLookingAtPopup = true;
		}

		if (stayActive && NewGameController.currentPlayer.isStartingTurn && NewGameController.currentPlayer.isTurn) {
			GUI.Box(new Rect(10, 10, 500, 300), "Green to move red");
         	if(GUI.Button(new Rect(20, 400, 100, 20), "Close"))
         	{
				NewGameController.currentPlayer.isStartingTurn = false;
				NewGameController.currentPlayer.isLookingAtPopup = false;
			}


		}
	}
}
