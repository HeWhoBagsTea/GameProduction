using UnityEngine;
using System.Collections;

public class TileManaWell : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 1;
		this.TerrainName = "Mana Well";
	}
	//Should units be able to shoot through mountain tiles?
	
	override protected void tempModsUpdate()
	{
		//Debug.Log("is called");
	}
}
