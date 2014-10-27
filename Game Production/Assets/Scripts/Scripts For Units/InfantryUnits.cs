using UnityEngine;
using System.Collections;

public class InfantryUnits : UnitBase {
	private bool isAlive = false;
	private Vector3 actionPos;
	private Vector3 buttonOffset = new Vector3(0.0f, 25.0f, 0);

//	void onGUI()
//	{
//		BUTTON_X_POS =  - (Screen.width / 8);
//		BUTTON_WIDTH = Screen.width/9;
//		BUTTON_HEIGHT = Screen.height/20;
//		BUTTON_SPACING = Screen.height/100 + Screen.height/20;
//	
//		if (isSelected) {
//			Rect attackButton = new Rect (BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
//			Rect moveButton = new Rect (BUTTON_X_POS, attackButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
//		
//			GUI.color = (!this.hasActioned) ? Color.white : Color.gray;
//			if (GUI.Button (attackButton, "Attack")) {
//					showAttack();
//			}
//		
//			GUI.color = (!this.hasMoved) ? Color.white : Color.gray;
//			if (GUI.Button (moveButton, "Move")) {
//					showMovement();
//			}
//		}
//	}


	void Update()
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			actionPos.x = Input.mousePosition.x;// + buttonOffset;
			actionPos.y = Screen.height - Input.mousePosition.y;
			isAlive = true;
		}


	}

	//public override 

	void OnGUI()  
	{
		if (isAlive && this.isSelected) 
		{
			//GUI.Box(new Rect(actionPos.x, actionPos.y, 120, 120), "TEST BOX");
			if(GUI.Button(new Rect(actionPos.x, actionPos.y, Screen.width / 18, Screen.height / 40), "Move"))
			{
				showMovement();
			}

			if(GUI.Button(new Rect(actionPos.x, actionPos.y + buttonOffset.y, Screen.width / 18, Screen.height / 40), "Attack"))
			{
				showAttack();
			}
		}

	}
}
