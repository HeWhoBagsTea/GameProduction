using UnityEngine;
using System.Collections;

public class MeleeInfo : UnitBase {
	
	override public void init() {
		this.maxHP = 12;
		this.currentHP = this.maxHP;
		this.minAttackRange = 1;
		this.maxAttackRange = 1;
		this.attackPow = 6;
		this.movement = 6;
		this.foodCost = 2;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Melee Unit");
	}
}
