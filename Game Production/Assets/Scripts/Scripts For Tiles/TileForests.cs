using UnityEngine;
using System.Collections;

public class TileForests : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 2;
		this.defensiveValue = 1;
		this.TerrainName = "Forest";
		this.ResourceType = "Lumber";
		this.ResourceValue = Random.Range (0, 5);
	}
	//Siege units should be able to move through forests but not attack while in them.

	override protected void tempModsUpdate()
	{
		//if (this.unitOnTile != null) {
		//	Debug.Log("is called");
		//	this.unitOnTile.transform.position = new Vector3(this.unitOnTile.transform.position.x, 1.5f, this.unitOnTile.transform.position.z);
		//		}
	}


}
