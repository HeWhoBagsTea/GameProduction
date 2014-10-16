using UnityEngine;
using System.Collections;

public class UnitBase : MonoBehaviour {
	public CodeTileStandard currentSpace;
	public Material[] unitHighlights;

	public int controller = -1;
	public int movement = 1;

	public bool hasMoved = false;
	public bool hasActioned = false;
	public bool isDone = false;

	public Vector3 posOffset;

	public virtual void init () {
		Debug.Log ("has unit");
		posOffset = new Vector3 (0 , .5f, 0);
	}
	
	void Start() {
		init ();

		currentSpace = getClosestTile ();
		currentSpace.unitOnTile = this;
		this.transform.position = currentSpace.transform.position + posOffset;

		Material tempMat = transform.GetComponent<MeshRenderer> ().material;

		for (int i = 0; i < unitHighlights.Length; i++) {
			if(tempMat.name.Substring(0, tempMat.name.IndexOf(" (")).Equals(unitHighlights[i].name)) {
				controller = i;
			}
		}
	}

	void Update() {
		if (hasMoved && hasActioned) {
			isDone = true;
		}

		if (isDone) {
			renderer.material = unitHighlights[controller + 2];
		}
	}

	public void resolveTurn() {
		renderer.material = unitHighlights[controller + 2];
		hasMoved = false;
		hasActioned = false;
		isDone = false;
	}

	public void showMovement() {
		if (!hasMoved) {
			currentSpace.highlightWithin (this.movement);
			currentSpace.selected (1);
		}
	}

	public void moveUnit(CodeTileStandard moveLocation) {
		currentSpace.unitOnTile = null;
		currentSpace = moveLocation;
		transform.position = currentSpace.transform.position + posOffset;
		currentSpace.unitOnTile = this;
		hasMoved = true;
	}

	public CodeTileStandard getClosestTile() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		GameObject closest = null;
		foreach(GameObject i in tiles) {
			if(!closest) {
				closest = i;
			}

			if(Vector3.Distance(this.transform.position, i.transform.position) <= Vector3.Distance(this.transform.position, closest.transform.position)) {
				closest = i;
			}
		}

		return closest.GetComponent<CodeTileStandard>();
	}
}
