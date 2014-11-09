using UnityEngine;
using System.Collections;

public class TilePlains : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 1;
		this.TerrainName = "Plain";
		this.ResourceType = "Food";
		this.ResourceValue = 2; //Random.Range (0, 5);
	}

	override protected void tempModsUpdate()
	{
	}
}