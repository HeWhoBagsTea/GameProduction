using UnityEngine;
using System.Collections;

public class firstTurn : MonoBehaviour {

	private bool stayActive = false;

	void OnGUI() {
		if (NewGameController.currentPlayer.isTurn && NewGameController.currentPlayer.isStartingTurn && !NewGameController.currentPlayer.isLookingAtPopup) {
			stayActive = true;
			NewGameController.currentPlayer.isLookingAtPopup = true;
		}

		if (stayActive && NewGameController.currentPlayer.isStartingTurn && NewGameController.currentPlayer.isTurn) {
			GUI.skin.box.wordWrap = true;
			GUI.Box(new Rect(25, 200, 500, 300), "Welcome to Daren's Siege! The obejective is simple defeat all of your enemy's pieces. Best of luck");
			if(GUI.Button(new Rect(390, 450, 125, 35), "Close"))
         	{
				NewGameController.currentPlayer.isStartingTurn = false;
				NewGameController.currentPlayer.isLookingAtPopup = false;
			}


		}
	}
}
