using UnityEngine;
using System.Collections;

public class TileMountains : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 3;
		this.TerrainName = "Mountain";
		this.ResourceType = "Ore";
		this.ResourceValue = Random.Range (0, 5);
	}
	//Should units be able to shoot through mountain tiles?

	override protected void tempModsUpdate()
	{
	}

}
