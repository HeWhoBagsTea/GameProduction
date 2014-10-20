using UnityEngine;
using System.Collections;

public class CodeGameController : MonoBehaviour {
	
	public GameObject tile;
	private UnitBase selectedUnit;
	private UnitBase previousSelectedUnit;
	
	public static int playersTurn = 1;
	private float BOX_X_POS = Screen.width*0.4f;
	private float BOX_Y_POS = Screen.height*.05f;
	private float BOX_WIDTH = Screen.width * 0.3f;
	private float BOX_HEIGHT = Screen.height * 0.1f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		BOX_X_POS = Screen.width * 0.4f;
		BOX_Y_POS = Screen.height * 0.05f;
		BOX_WIDTH = Screen.width * 0.225f;
		BOX_HEIGHT = Screen.height * 0.1f;

		if (previousSelectedUnit == null) {
			previousSelectedUnit = selectedUnit;
		} else if (selectedUnit != previousSelectedUnit) {
			previousSelectedUnit.deselect();
			previousSelectedUnit = selectedUnit;
		}
		
		if (selectedUnit != null && !selectedUnit.isDone) {
			selectedUnit.selected();
		}
		
		//if (selectedUnit != null && selectedUnit.controller != playersTurn) {
		//	selectedUnit = null;
		//}
		
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();
			
			if(Physics.Raycast(ray, out hit)) {
				GameObject hitObject = hit.collider.gameObject;
				
				switch(hitObject.tag) {
				case "Tile":
					CodeTileStandard tempTile = hitObject.GetComponent<CodeTileStandard>();
					Material tempTileMat = tempTile.transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ().material;
					string materialName = tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" ("));
					Debug.Log(materialName);
					
					
					if(tempTile.unitOnTile == null && selectedUnit != null && materialName.Equals("defaultMat")) {
						clearHighlights();
						selectedUnit = null;
					}
					else if(tempTile.unitOnTile == null && selectedUnit != null && materialName.Equals("MovementSpace")) {
						selectedUnit.moveUnit(tempTile);
						clearHighlights();
						selectedUnit = null;
					}
					else if(tempTile.unitOnTile != null && selectedUnit != null && materialName.Equals("AttackSpace")) {
						selectedUnit.attackUnit(tempTile.unitOnTile);
						clearHighlights();
						selectedUnit = null;
					} 
					else if(tempTile.unitOnTile != null && (selectedUnit != null || selectedUnit == null)) {
						clearHighlights();
						selectedUnit = tempTile.unitOnTile;
						if(selectedUnit.controller != playersTurn) {
							selectedUnit = null;
						}
					}
					
					
					break;
				case "Unit":
					UnitBase tempUnit = hitObject.GetComponent<UnitBase>();
					Material tempUnitSpaceMat = tempUnit.currentSpace.transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ().material;
					string tempUnitSpaceMatName = tempUnitSpaceMat.name.Substring(0, tempUnitSpaceMat.name.IndexOf(" ("));;
					clearHighlights();
					if(tempUnit.controller == playersTurn) {
						selectedUnit = tempUnit;
					}
					else if(selectedUnit != null && tempUnitSpaceMatName.Equals("AttackSpace")) {
						selectedUnit.attackUnit(tempUnit);
						clearHighlights();
						selectedUnit = null;
					}
					
					break;
				}
				
			}
		}
		
		if (Input.GetMouseButtonDown (1)) {
			clearHighlights();
			selectedUnit = null;
		}
	}
	
	public void clearHighlights() {
		foreach(CodeTileStandard i in FindObjectsOfType(typeof(CodeTileStandard))) {
			i.deselect();
		}
	}

	public void OnGUI() 
	{
		GUI.skin.box.alignment = TextAnchor.UpperCenter;
		GUI.color = new Vector4(0.23f, 0.75f, 0.54f, 1);
		if (selectedUnit != null) {
			GUI.Box (new Rect (BOX_X_POS, BOX_Y_POS*0f, BOX_WIDTH, BOX_HEIGHT), 
			         "Unit Stats:");
			GUI.Box(new Rect (BOX_X_POS, BOX_Y_POS, BOX_WIDTH, BOX_HEIGHT),
			        "HP:" + selectedUnit.currentHP + "/"+selectedUnit.maxHP +
			        " AttackRange: " + selectedUnit.minAttackRange + "-"+ selectedUnit.maxAttackRange);
			GUI.Box(new Rect (BOX_X_POS, BOX_Y_POS*2f, BOX_WIDTH, BOX_HEIGHT),
			        "Movement: " + selectedUnit.movement +
			        " Attack Power: " + selectedUnit.attackPow);
			GUI.Box(new Rect (BOX_X_POS, BOX_Y_POS*3f, BOX_WIDTH, BOX_HEIGHT),
			        "Unit Type: " + selectedUnit.unitType);
		}
	}
}
