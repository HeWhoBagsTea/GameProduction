using UnityEngine;
using System.Collections;

public class TileForests : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 2;
		this.TerrainName = "Forest";
		this.ResourceType = "Lumber";
		this.ResourceValue = Random.Range (0, 5);
	}
	//Siege units should be able to move through forests but not attack while in them.

	override protected void tempModsUpdate()
	{
	}


}
