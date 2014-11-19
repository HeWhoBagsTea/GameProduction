using UnityEngine;
using System.Collections;

public class UnitPopUp : MonoBehaviour 
{
	private bool hasEntered = false;
	private bool stayActive = false;

	void OnMouseUpAsButton()
	{
		hasEntered = true;
	}

	void OnGUI() {
		if (hasEntered) {
			if(NewGameController.currentPlayer.isTurn && !NewGameController.currentPlayer.isLookingAtPopup && NewGameController.currentPlayer.fistTimeSelectUnit) {
				stayActive = true;
				NewGameController.currentPlayer.isLookingAtPopup = true;
			}
		}

		if (stayActive && NewGameController.currentPlayer.fistTimeSelectUnit && NewGameController.currentPlayer == this.gameObject.GetComponent<UnitBase>().controller) {
			GUI.skin.box.wordWrap = true;
			GUI.Box(new Rect(25, 200, 500, 300), "These are your units, your main source to victory. Their stats can be found in the top middle of your screen. Green highlights are their move range, and Orange are their attack range.");
			if(GUI.Button(new Rect(390, 450, 125, 35), "Close"))
			{
				NewGameController.currentPlayer.fistTimeSelectUnit = false;
				NewGameController.currentPlayer.isLookingAtPopup = false;
			}
		}

		hasEntered = false;
	}
}
