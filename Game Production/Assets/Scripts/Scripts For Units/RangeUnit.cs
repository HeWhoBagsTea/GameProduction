using UnityEngine;
using System.Collections;

public class RangeUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 8;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 2;
		this.maxAttackRange = 3;
		this.movement = 4;
		this.attackPow = 3;
		this.foodCost = 4;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Range Unit");
	}
}
