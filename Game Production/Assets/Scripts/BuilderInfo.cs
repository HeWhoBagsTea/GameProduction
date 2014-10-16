using UnityEngine;
using System.Collections;

public class BuilderInfo : UnitBase {
	
	override public void init() {
		this.maxHP = 8;
		this.currentHP = this.maxHP;
		this.minAttackRange = 1;
		this.maxAttackRange = 1;
		this.movement = 6;
		this.attackPow = 2;
		this.foodCost = 1;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Builder Unit");
	}
}

