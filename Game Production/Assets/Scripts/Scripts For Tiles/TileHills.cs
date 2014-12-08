using UnityEngine;
using System.Collections;

public class TileHills : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 2;
		this.TerrainName = "Hills";
		this.ResourceType = "";
	}
	//Should units be able to shoot through mountain tiles?
	
	override protected void tempModsUpdate()
	{
		//Debug.Log("is called");
		this.unitOnTile.transform.position = new Vector3(this.unitOnTile.transform.position.x, 1.5f, this.unitOnTile.transform.position.z);
		
	}
	override public void SelectTarget(TileStandard target)
	{
		NewGameController.selectedBuilding.SelectTarget (target);
	}
}
