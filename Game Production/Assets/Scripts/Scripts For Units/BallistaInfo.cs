using UnityEngine;
using System.Collections;

public class BallistaInfo : UnitBase {
	
	override public void init() {
		this.HPmax = 15;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 3;
		this.maxAttackRange = 6;
		this.movement = 3;
		this.attackPow = 8;
		this.foodCost = 0;
		this.lumberCost = 10;
		this.unitType = "Siege";
		Debug.Log ("Ballista Unit");
	}
}
