using UnityEngine;
using System.Collections;

public class RangeUnit : UnitBase {
	
	override public void init() {
		this.HPmax = 8;
		this.HPcurr = this.HPmax;
		
		this.OriginalMinAttackRange = 2;
		this.OriginalMaxAttackRange = 3;
		this.OriginalMovement = 3;
		this.OriginalAttackPow = 3;
		this.hasBeenUpgraded = false;
		this.UpkeepCost = 2;

		this.minAttackRange = OriginalMinAttackRange;
		this.maxAttackRange = OriginalMaxAttackRange;
		this.movement = OriginalMovement;
		this.attackPow = OriginalAttackPow;

		this.foodCost = 4;
		this.lumberCost = 1;
		this.oreCost = 0;
		this.unitType = "Infantry";
		this.unitClass = "Archer";
		Debug.Log ("Range Unit");
	}

	override protected void buffMe() {
			if (this.currentSpace.TerrainName.Equals ("Forest")) {
					this.minAttackRange = this.OriginalMinAttackRange - 1;
					this.maxAttackRange = this.OriginalMaxAttackRange - 1;
					//this.hasTempBuff = true;
			} else if (this.currentSpace.TerrainName.Equals ("Mountain")) {
					this.minAttackRange = this.OriginalMinAttackRange;		
					this.maxAttackRange = this.OriginalMaxAttackRange + 1;
					//this.hasTempBuff = true;
			} else {
					this.minAttackRange = OriginalMinAttackRange;
					this.maxAttackRange = OriginalMaxAttackRange;
					this.movement = OriginalMovement;
					this.attackPow = OriginalAttackPow;
			}
	}
}
