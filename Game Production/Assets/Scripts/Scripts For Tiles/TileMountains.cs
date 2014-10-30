using UnityEngine;
using System.Collections;

public class TileMountains : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 3;
	}
	//Should units be able to shoot through mountain tiles?
}
