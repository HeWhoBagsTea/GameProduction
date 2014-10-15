using UnityEngine;
using System.Collections;

public class UnitBase : MonoBehaviour {
	public CodeTileStandard currentSpace;
	public int movement = 1;

	public virtual void init () {
		Debug.Log ("has unit");
	}
	
	void Start() {
//		currentSpace = getClosestTile ();
//		currentSpace.unitOnTile = this;
//		this.transform.position = currentSpace.transform.position;
		//this.init ();
		//currentSpace = hitColliders [0].GetComponentsInParent<CodeTileStandard> ();
		//currentSpace.unitOnTile = this;
		//this.transform.position = currentSpace.transform.position;
	}

	public void showMovement() {
		//currentSpace.highlightMovementOfUnit (this.movement);
	}

//	public CodeTileStandard getClosestTile() {
//		CodeTileStandard[] tiles = GameObject.FindGameObjectsWithTag("Tile")
//	}
}
