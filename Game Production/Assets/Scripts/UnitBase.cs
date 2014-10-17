using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitBase : MonoBehaviour {
	public CodeTileStandard currentSpace;
	public Material[] unitHighlights;
	public Material[] highlights;

	public int controller = -1;
	public int movement = 1;
	public int minAttackRange = 1;
	public int maxAttackRange = 1;
	public int attackPow = 1;
	public int maxHP = 1;
	public int currentHP = 1;
	public int foodCost = 0;
	public int lumberCost = 0;
	public string unitType = "";

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

	public void showMovement() {
		if (!hasMoved) {
			showMovementRangeHelper(movement, currentSpace);
			highlightCurrentSpace(highlights[1]);
		}
	}

	private void showMovementRangeHelper(int moveRange, CodeTileStandard tile) {
		if (moveRange > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (tile.transform.position, 2);
			List<CodeTileStandard> tiles = new List<CodeTileStandard>();
			
			foreach(Collider i in hitColliders) {
				if(i.GetComponent<CodeTileStandard>() != null) {
					tiles.Add (i.GetComponent<CodeTileStandard>());
				}
			}

			for(int i = 0; i < tiles.Count; i++) {
				if(moveRange - tiles[i].moveCost >= 0) {
					if(tiles[i].unitOnTile == null) {
						tiles[i].transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = highlights[2];
					}
					showMovementRangeHelper(moveRange - tiles[i].moveCost, tiles[i]);
				}
			}
		}
	}

	public void moveUnit(CodeTileStandard moveLocation) {
		currentSpace.unitOnTile = null;
		currentSpace = moveLocation;
		transform.position = currentSpace.transform.position + posOffset;
		currentSpace.unitOnTile = this;
		hasMoved = true;
	}

	public void resolveTurn() {
		renderer.material = unitHighlights[controller];
		hasMoved = false;
		hasActioned = false;
		isDone = false;
	}

	public void highlightCurrentSpace(Material highlight) {
		MeshRenderer currentSpaceTile = currentSpace.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer> ();
		currentSpaceTile.material = highlight;
	}

	private CodeTileStandard getClosestTile() {
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
