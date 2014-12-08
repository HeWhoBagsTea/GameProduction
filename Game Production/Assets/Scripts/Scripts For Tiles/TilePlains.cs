using UnityEngine;
using System.Collections;

public class TilePlains : TileStandard {

	public int customResourceVal = 0;

	override public void init()
	{
		this.originalMoveCost = 1;
		this.TerrainName = "Plain";
		this.ResourceType = "Food";


		ResourceValue = (customResourceVal == 0) ? Random.Range (2, 4) : customResourceVal;

	}

	override protected void tempModsUpdate()
	{
		if (this.unitOnTile.unitClass.Equals("Warrior") || this.unitOnTile.unitClass.Equals("Mage")){
			//Debug.Log("is called");
			this.unitOnTile.transform.position = new Vector3(this.unitOnTile.transform.position.x, 1.0f, this.unitOnTile.transform.position.z);
		}
	}
	
	override public void SelectTarget(TileStandard target)
	{
		NewGameController.selectedBuilding.SelectTarget (target);
	}
}