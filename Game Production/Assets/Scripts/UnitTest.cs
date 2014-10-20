using UnityEngine;
using System.Collections;

public class UnitTest : UnitBase {
	
	override public void init() {
		this.movement = 2;
		this.maxAttackRange = 1;
		this.minAttackRange = 1;
		Debug.Log ("Test Unit");
		posOffset = new Vector3 (0, .5f, 0);
	}
}
