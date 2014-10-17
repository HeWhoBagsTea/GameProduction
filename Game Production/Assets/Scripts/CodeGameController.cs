using UnityEngine;
using System.Collections;

public class CodeGameController : MonoBehaviour {

	public GameObject tile;
	private UnitBase selectedUnit;

	public static int playersTurn = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit = new RaycastHit();

			if(Physics.Raycast(ray, out hit)) {
				GameObject hitObject = hit.collider.gameObject;

				switch(hitObject.tag) {
				case "Tile":
					CodeTileStandard tempTile = hitObject.GetComponent<CodeTileStandard>();
					Material tempTileMat = tempTile.transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ().material;
					Debug.Log(tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" (")));

					if(tempTile.unitOnTile != null && (selectedUnit != null || selectedUnit == null)) {
						clearHighlights();
						selectedUnit = tempTile.unitOnTile;

						if(selectedUnit.controller == playersTurn) {
							selectedUnit.showMovement();
						}
						else {
							selectedUnit = null;
						}
					}
					else if(tempTile.unitOnTile == null && selectedUnit != null && tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" (")).Equals("defaultMat")) {
						clearHighlights();
						selectedUnit = null;
					}
					else if(tempTile.unitOnTile == null && selectedUnit != null && tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" (")).Equals("MovementSpaces")) {
						selectedUnit.moveUnit(tempTile);
						clearHighlights();
						selectedUnit = null;
					}


					break;
				case "Unit":
					UnitBase tempUnit = hitObject.GetComponent<UnitBase>();
					clearHighlights();
					selectedUnit = tempUnit;
					if(selectedUnit.controller == playersTurn) {
						selectedUnit.showMovement();
					}
					else {
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
		GUI.color = new Vector4(0.23f, 0.75f, 0.54f, 1);
		if (selectedUnit != null) {
			GUI.Box (new Rect ((Screen.width*0.4f), 0, 200, 100), 
			         "Unit Stats:");	
			GUI.Box(new Rect ((Screen.width*0.40f), 25, 200, 25),
			        "HP:" + selectedUnit.currentHP + "/"+selectedUnit.maxHP +
			        " AttackRange: " + selectedUnit.minAttackRange + "-"+ selectedUnit.maxAttackRange);
			GUI.Box(new Rect ((Screen.width*0.40f), 50, 200, 25),
			        "Movement: " + selectedUnit.movement +
			        " Attack Power: " + selectedUnit.attackPow);
			GUI.Box(new Rect ((Screen.width*0.40f), 75, 200, 25),
			        "Unit Type: " + selectedUnit.unitType);
		}
	}
}
