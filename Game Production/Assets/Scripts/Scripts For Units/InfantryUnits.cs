using UnityEngine;
using System.Collections;

public class InfantryUnits : UnitBase {

	void onGUI()
	{
		BUTTON_X_POS = Screen.width - (Screen.width / 8);
		BUTTON_WIDTH = Screen.width/9;
		BUTTON_HEIGHT = Screen.height/20;
		BUTTON_SPACING = Screen.height/100 + Screen.height/20;

		if (isSelected) {
			Rect attackButton = new Rect (BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
			Rect moveButton = new Rect (BUTTON_X_POS, attackButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		
			GUI.color = (!this.hasActioned) ? Color.white : Color.gray;
			if (GUI.Button (attackButton, "Attack")) {
					showAttack();
			}
		
			GUI.color = (!this.hasMoved) ? Color.white : Color.gray;
			if (GUI.Button (moveButton, "Move")) {
					showMovement();
			}
		}
	}
}
