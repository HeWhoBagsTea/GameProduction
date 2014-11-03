using UnityEngine;
using System.Collections;

public class InfantryUnits : UnitBase
{
		private bool isAlive = false;
		private Vector3 actionPos;
		private Vector3 buttonOffset = new Vector3 (0.0f, 25.0f, 0);
		private Rect moveButton;
		private Rect attackButton;
	
		private void disableScripts ()
		{
				GameObject[] disabledTiles = GameObject.FindGameObjectsWithTag ("Tile");
				foreach (GameObject t in disabledTiles) {
						t.GetComponent<TileStandard> ().enabled = false;
				}
		}
	
		private void enableTiles ()
		{
				GameObject[] enabledTiles = GameObject.FindGameObjectsWithTag ("Tile");
				foreach (GameObject tiles in enabledTiles) {
						tiles.GetComponent<TileStandard> ().enabled = true;
				}
		}
	
		void Update ()
		{
			if (Input.GetMouseButtonDown (1)) {
						actionPos.y = Screen.height - Input.mousePosition.y;
						actionPos.x = Input.mousePosition.x;
						isAlive = true;
				}
		}

//public override 


		void OnGUI ()
		{
				moveButton = new Rect (actionPos.x, actionPos.y, Screen.width / 18, Screen.height / 40);
				attackButton = new Rect (actionPos.x, actionPos.y + buttonOffset.y, Screen.width / 18, Screen.height / 40);
	
				if (this.isSelected && isAlive) {
						disableScripts ();
		
						//GUI.color = (!this.hasActioned) ? Color.white : Color.gray;
						if (GUI.Button (moveButton, "Move") && GUIUtility.hotControl == 0) {
								showMovement ();
								GUIUtility.hotControl = 1;
								isAlive = false;
								enableTiles ();
								//Debug.Log(isAlive);
						} else if (GUI.Button (attackButton, "Attack") && GUIUtility.hotControl == 0) {
								showAttack ();
								GUIUtility.hotControl = 1;
								isAlive = false;
			
						} else if (GUIUtility.hotControl == 1) {
								GUIUtility.hotControl = 0;
								//isAlive = false;
						}
				}
	
		}
}

