using UnityEngine;
using System.Collections;

public class BallistaUnit : UnitBase {
	
	override public void init() {
		this.OriginalMinAttackRange = 2;
		this.OriginalMaxAttackRange = 3;
		this.OriginalMovement = 2;
		this.OriginalAttackPow = 8;
		this.hasBeenUpgraded = false;
		this.UpkeepCost = 4;

		
		this.minAttackRange = OriginalMinAttackRange;
		this.maxAttackRange = OriginalMaxAttackRange;
		this.movement = OriginalMovement;
		this.attackPow = OriginalAttackPow;

		this.HPmax = 15;
		this.HPcurr = this.HPmax;
		this.foodCost = 0;
		this.lumberCost = 5;
		this.unitType = "Siege";
		this.unitClass = "Ballista";
		//Debug.Log ("Ballista Unit");
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
