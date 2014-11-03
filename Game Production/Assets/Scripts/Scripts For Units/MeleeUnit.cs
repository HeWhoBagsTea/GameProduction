using UnityEngine;
using System.Collections;

public class MeleeUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 12;
		this.HPcurr = this.HPmax;
		this.minAttackRange = 1;
		this.maxAttackRange = 1;
		this.attackPow = 3;
		this.movement = 3;
		this.foodCost = 2;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Melee Unit");
	}
}
