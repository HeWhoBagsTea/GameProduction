using UnityEngine;
using System.Collections;

public class UnitBase : MonoBehaviour {
	public CodeTileStandard currentSpace;
	public int movement = 1;

	public virtual void init () {
		Debug.Log ("has unit");
	}
	
	void Start() {
		currentSpace = getClosestTile ();
		currentSpace.unitOnTile = this;
		this.transform.position = currentSpace.transform.position;
		init ();
	}

	public void showMovement() {
		currentSpace.highlightWithin (this.movement);
		currentSpace.selected (1);
	}

	public void moveUnit(CodeTileStandard moveLocation) {
		currentSpace.unitOnTile = null;
		currentSpace = moveLocation;
		transform.position = currentSpace.transform.position;
		currentSpace.unitOnTile = this;
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
