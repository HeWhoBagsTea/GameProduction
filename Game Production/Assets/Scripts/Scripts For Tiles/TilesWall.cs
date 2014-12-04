using UnityEngine;
using System.Collections;

public class TilesWall : TileStandard {
	
	override public void init()
	{
		this.originalMoveCost = 1;
		this.defensiveValue = 2;
		this.TerrainName = "Wall";
	}
	
	override protected void tempModsUpdate()
	{
		//if (this.unitOnTile != null && this.controller.playerID != NewGameController.currentPlayer.playerID && this.getControlRingMatName() != "Neutral") {
		//	this.originalMoveCost = 2;
		//	} else {
		//	this.originalMoveCost = 1;
		//	}
		//Debug.Log("is called");
		//this.unitOnTile.transform.position = new Vector3(this.unitOnTile.transform.position.x, 3.0f, this.unitOnTile.transform.position.z);
		
	}
	
}
