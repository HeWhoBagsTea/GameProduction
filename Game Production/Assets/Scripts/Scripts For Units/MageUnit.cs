using UnityEngine;
using System.Collections;

public class MageUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 6;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 1;
		this.maxAttackRange = 3;
		this.movement = 2;
		this.attackPow = 6;
		this.foodCost = 3;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Mage Unit");
	}
}
