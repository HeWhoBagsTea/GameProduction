using UnityEngine;
using System.Collections;

public class TilePlains : TileStandard {

	public int customResourceVal = 0;

	override public void init()
	{
		this.originalMoveCost = 1;
		this.TerrainName = "Plain";
		this.ResourceType = "Food";


		ResourceValue = (customResourceVal == 0) ? Random.Range (0, 3) : customResourceVal;

	}

	override protected void tempModsUpdate()
	{
	}
}