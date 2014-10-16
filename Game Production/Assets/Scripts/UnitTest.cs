using UnityEngine;
using System.Collections;

public class UnitTest : UnitBase {
	
	override public void init() {
		this.movement = 4;
		Debug.Log ("Test Unit");
		posOffset = new Vector3 (0, .5f, 0);
	}
}
