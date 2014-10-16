using UnityEngine;
using System.Collections;

public class MageInfo : UnitBase {
	
	override public void init() {
		this.maxHP = 12;
		this.currentHP = this.maxHP;
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
