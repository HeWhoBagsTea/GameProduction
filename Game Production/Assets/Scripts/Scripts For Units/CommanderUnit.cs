using UnityEngine;
using System.Collections;

public class CommanderUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 60;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 1;
		this.maxAttackRange = 2;
		this.attackPow = 6;
		this.movement = 5;
		this.foodCost = 0;
		this.lumberCost = 0;
		this.unitType = "Special";
		Debug.Log ("Commander Unit");
	}
}
