using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CodeTileStandard : MonoBehaviour {

	public Material[] controlRingColors;
	public Material defualtMat;

	public UnitBase unitOnTile;

	public int moveCost;
	public int originalMoveCost = 1;

	private int controller = -1;

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

	public void deselect() {
		MeshRenderer planeRenderer = transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = defualtMat;
	}

	public void setControl(int player) {
		MeshRenderer planeRenderer = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = controlRingColors [player];
		controller = player;
	}

}