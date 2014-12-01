using UnityEngine;
using System.Collections;

public class MeleeUnit : UnitBase {

	public int customHP = 0;

	override public void init() {
		this.HPmax = 12;
		if (customHP == 0) {
			this.HPcurr = this.HPmax;
		}
		else{
			this.HPcurr = this.customHP;
		}


		this.OriginalMinAttackRange = 1;
		this.OriginalMaxAttackRange = 1;
		this.OriginalMovement = 4;
		this.OriginalAttackPow = 3;
		this.hasBeenUpgraded = false;
		this.UpkeepCost = 3;

		this.minAttackRange = OriginalMinAttackRange;
		this.maxAttackRange = OriginalMaxAttackRange;
		this.movement = OriginalMovement;
		this.attackPow = OriginalAttackPow;

		this.foodCost = 2;
		this.lumberCost = 0;
		this.oreCost = 1;
		this.unitType = "Infantry";
		this.unitClass = "Melee";
		Debug.Log ("Melee Unit");
	}

	override protected void buffMe() {
		if (this.currentSpace.TerrainName.Equals ("Forest")) {
			//this.hasTempBuff = true;
		} else if (this.currentSpace.TerrainName.Equals ("Hills")) {	
			this.attackPow = this.OriginalAttackPow + 1;
			//this.hasTempBuff = true;
		} else {
			this.minAttackRange = OriginalMinAttackRange;
			this.maxAttackRange = OriginalMaxAttackRange;
			this.movement = OriginalMovement;
			this.attackPow = OriginalAttackPow;
		}
	}
}
