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
		this.lumberCost = 1;
		this.oreCost = 1;
		this.unitType = "Infantry";
		this.unitClass = "Mage";
		//Debug.Log ("Mage Unit");
	}

	override protected void buffMe() {
		if (this.currentSpace.TerrainName.Equals ("Forest")) {
			//this.hasTempBuff = true;
		} else if (this.currentSpace.TerrainName.Equals ("Hills")) {	
			//this.attackPow = this.OriginalAttackPow + 1;
			//this.hasTempBuff = true;
		} else if (this.currentSpace.TerrainName.Equals ("ManaWell")) {	
			Debug.Log("Mage on ManaWell");
			this.attackPow = this.OriginalAttackPow + 1;
			this.maxAttackRange = this.OriginalMaxAttackRange - 1;
			//this.hasTempBuff = true;
		} else {
			this.minAttackRange = OriginalMinAttackRange;
			this.maxAttackRange = OriginalMaxAttackRange;
			this.movement = OriginalMovement;
			this.attackPow = OriginalAttackPow;
		}
	}
}
