using UnityEngine;
using System.Collections;

public class MageUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 12;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 1;
		this.maxAttackRange = 4;
		this.movement = 4;
		this.attackPow = 6;
		this.foodCost = 3;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Mage Unit");
	}
}
