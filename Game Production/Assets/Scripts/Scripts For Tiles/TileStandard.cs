﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileStandard : MonoBehaviour {
	
	public Material[] controlRingColors;
	public Material defualtMat;
	public UnitBase unitOnTile;
	public Player controller = null;
	public bool hasStructure = false;
	
	public int moveCost;
	public int originalMoveCost = 1;

	public virtual void init() {

	}

	void Start () {
		init ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject i in players) {
			if(i.GetComponent<Player>() != null && getControlRingMatName() == i.GetComponent<Player>().getPlayerColor()) {
				this.controller = i.GetComponent<Player>();
			}
		}
		
		moveCost = originalMoveCost;
	}

	//Called when tile is pressed
	void OnMouseUpAsButton() {
		Debug.Log (getTerrainMatName ());

		if (this.unitOnTile == null && getTerrainMatName ().Equals ("defaultMat") && !hasStructure) {
			NewGameController.deselectAllUnits();
		}
		else if (this.unitOnTile != null && this.unitOnTile.controller != NewGameController.currentPlayer && getTerrainMatName ().Equals ("defaultMat") && !hasStructure) {
			NewGameController.deselectAllUnits();
		}
		else {
			if(NewGameController.selectedUnit != null) {
				if (this.unitOnTile != null && NewGameController.currentPlayer == this.unitOnTile.controller) {
					this.unitOnTile.selected ();
				} 
				else if(this.unitOnTile == null && NewGameController.selectedUnit != null && getTerrainMatName().Equals("MovementSpace")) {
					NewGameController.selectedUnit.moveUnit(this);
				}
				else if(this.unitOnTile != null && NewGameController.selectedUnit != null && getTerrainMatName().Equals("AttackSpace")) {
					NewGameController.selectedUnit.attackUnit(this.unitOnTile);
				}
			}
			else if(NewGameController.selectedUnit == null) {
				if (this.unitOnTile != null && NewGameController.currentPlayer == this.unitOnTile.controller) {
					this.unitOnTile.selected ();
				} 
				else if(this.unitOnTile == null && this.hasStructure) {
					NewGameController.deselectAllUnits();
					//Have structure do things
				}
			}
		}
	}
	
	void Update () {
		if (unitOnTile != null && (NewGameController.currentPlayer != unitOnTile.controller)) {
			moveCost = 10000;
		}
		else {
			moveCost = originalMoveCost;
		}
	}

	public string getControlRingMatName() {
		string ringName = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material.name;
		ringName = ringName.Substring (0, ringName.IndexOf (" ("));
		return ringName;
	}

	public string getTerrainMatName() {
		string terrainName = transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ().material.name;
		terrainName = terrainName.Substring (0, terrainName.IndexOf (" ("));
		return terrainName;
	}
	
	public void deselect() {
		MeshRenderer planeRenderer = transform.FindChild ("Terrain").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = defualtMat;
	}
	
	public void setControl(Player player) {
		MeshRenderer planeRenderer = transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ();
		planeRenderer.material = player.playerColor;
		controller = player;
	}
	
}