using UnityEngine;
using System.Collections;

public class CommanderUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 60;
		this.HPcurr = this.HPmax;

		this.OriginalMinAttackRange = 1;
		this.OriginalMaxAttackRange = 2;
		this.OriginalMovement = 5;
		this.OriginalAttackPow = 6;
		this.hasBeenUpgraded = false;
		this.UpkeepCost = 0;

		this.minAttackRange = OriginalMinAttackRange;
		this.maxAttackRange = OriginalMaxAttackRange;
		this.attackPow = OriginalAttackPow;
		this.movement = OriginalMovement;
		this.foodCost = 9999;
		this.lumberCost = 9999;
		this.oreCost = 9999;
		this.unitType = "Special";
		Debug.Log ("Commander Unit");
	}
	override protected void buffMe() {
		if (this.currentSpace.TerrainName.Equals ("Forest")) {
			//this.hasTempBuff = true;
		} else if (this.currentSpace.TerrainName.Equals ("Hills")) {	
			//this.attackPow = this.OriginalAttackPow + 1;
			//this.hasTempBuff = true;
		} else {
			this.minAttackRange = OriginalMinAttackRange;
			this.maxAttackRange = OriginalMaxAttackRange;
			this.movement = OriginalMovement;
			this.attackPow = OriginalAttackPow;
		}
	}
}
