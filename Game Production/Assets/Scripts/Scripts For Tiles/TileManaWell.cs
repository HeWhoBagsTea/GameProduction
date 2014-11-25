using UnityEngine;
using System.Collections;

public class TileManaWell : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 1;
		this.TerrainName = "ManaWell";
	}
	
	override protected void tempModsUpdate()
	{
		//Debug.Log("is called");
	}
}
