using UnityEngine;
using System.Collections;

public class CodeGameController : MonoBehaviour {

	public GameObject tilePlains;

	private bool firstClick = false;
	private UnitBase selectedUnit;

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
					Material tempTileMat = tempTile.transform.FindChild ("TerrainPlains").GetComponentInChildren<MeshRenderer> ().material;
					Debug.Log(tempTileMat.name);
					if(firstClick && tempTile.unitOnTile == null && tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" (")).Equals("MovementSpaces")) {
						selectedUnit.moveUnit(tempTile);
						clearHighlights();
						firstClick = false;
						selectedUnit = null;
					}

					else if(tempTile.unitOnTile != null && firstClick && tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" (")).Equals("defualtMat")) {
						clearHighlights();
						selectedUnit = tempTile.unitOnTile;
						selectedUnit.showMovement();
					}

					else if(tempTileMat.name.Substring(0, tempTileMat.name.IndexOf(" (")).Equals("defualtMat") && firstClick) {
						clearHighlights();
					}

					else if(tempTile.unitOnTile != null && !firstClick) {
						clearHighlights();
						selectedUnit = tempTile.unitOnTile;
						selectedUnit.showMovement();
;						firstClick = true;
					}
					break;
				case "Unit":
					UnitBase tempUnit = hitObject.GetComponent<UnitBase>();
					clearHighlights();
					selectedUnit = tempUnit;
					selectedUnit.showMovement();
					firstClick = true;
					break;
				}



				//CodeTileStandard hitTile = hitObject.GetComponent<CodeTileStandard>();
				//if(hitTile) {
				//	foreach(CodeTileStandard i in FindObjectsOfType(typeof(CodeTileStandard))) {
				//		i.deselect();
				//	}
				//
				//	hitTile.highlightWithin(2);
				//	hitTile.selected(1);
				//}
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			clearHighlights();
			selectedUnit = null;
			firstClick = false;
		}
	}

	public void clearHighlights() {
		foreach(CodeTileStandard i in FindObjectsOfType(typeof(CodeTileStandard))) {
			i.deselect();
		}
	}
}
