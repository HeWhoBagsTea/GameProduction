using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CodeTileStandard : MonoBehaviour {

	public Material[] controlRingColors;
	public Material[] tileHighlight;	

	public UnitBase unitOnTile;

	public int moveCost;
	public int originalMoveCost = 1;

	private int controller = -1;

	// Use this for initialization
	void Start () {
		string temp = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material.name;
		for (int i = 0; i < 3; i++) {
			if(temp.Substring(0,temp.IndexOf(" (")) == controlRingColors[i].name) {
				controller = i;
			}
		}

		moveCost = originalMoveCost;
	}

	void Update () {
		if (unitOnTile == null) {
			moveCost = originalMoveCost;
		}
		if (unitOnTile != null && (CodeGameController.playersTurn != unitOnTile.controller)) {
			moveCost = 10000;
		}
	}

	public void selected(int highLight) {
		MeshRenderer planeRenderer = transform.FindChild ("TerrainPlains").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = tileHighlight[highLight];
	}

	public void highlightWithin(int radius) {
		if (radius > 0) {
			Collider[] hitColliders = Physics.OverlapSphere (transform.position, 2);
			List<CodeTileStandard> tiles = new List<CodeTileStandard>();

			foreach(Collider i in hitColliders) {
				if(i.GetComponent<CodeTileStandard>() != null) {
					tiles.Add (i.GetComponent<CodeTileStandard>());
				}
			}

			for(int i = 0; i < tiles.Count; i++) {
				if(radius - tiles[i].moveCost >= 0) {
					if(tiles[i].unitOnTile == null) {
						tiles[i].selected(2);
					}
					tiles[i].highlightWithin(radius - tiles[i].moveCost);
				}
			}
		}
	}

	public void deselect() {
		MeshRenderer planeRenderer = transform.FindChild ("TerrainPlains").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = tileHighlight[0];
	}

	public void setControl(int player) {
		MeshRenderer planeRenderer = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = controlRingColors [player];
		controller = player;
	}

}