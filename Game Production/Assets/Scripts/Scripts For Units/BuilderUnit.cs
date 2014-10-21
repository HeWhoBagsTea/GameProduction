using UnityEngine;
using System.Collections;

public class BuilderUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 8;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 1;
		this.maxAttackRange = 1;
		this.movement = 6;
		this.attackPow = 2;
		this.foodCost = 1;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Builder Unit");
	}
}

