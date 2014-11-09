using UnityEngine;
using System.Collections;

public class MageUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 6;
		this.HPcurr = this.HPmax;

		this.OriginalMinAttackRange = 1;
		this.OriginalMaxAttackRange = 3;
		this.OriginalMovement = 2;
		this.OriginalAttackPow = 6;
		this.hasBeenUpgraded = false;
		this.UpkeepCost = 2;
		

		this.minAttackRange = OriginalMinAttackRange;
		this.maxAttackRange = OriginalMaxAttackRange;
		this.movement = OriginalMovement;
		this.attackPow = OriginalAttackPow;

		this.foodCost = 3;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		this.unitClass = "Mage";
		Debug.Log ("Mage Unit");
	}
}
