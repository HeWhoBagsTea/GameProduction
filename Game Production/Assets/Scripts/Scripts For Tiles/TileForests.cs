using UnityEngine;
using System.Collections;

public class TileForests : TileStandard {

	override public void init()
	{
		this.originalMoveCost = 2;
	}
	//Siege units should be able to move through forests but not attack while in them.


}
