using UnityEngine;
using System.Collections;

public class CommanderInfo : UnitBase {
	
	override public void init() {
		this.maxHP = 60;
		this.currentHP = this.maxHP;
		this.minAttackRange = 1;
		this.maxAttackRange = 2;
		this.attackPow = 6;
		this.movement = 5;
		this.foodCost = 0;
		this.lumberCost = 0;
		this.unitType = "Special";
		Debug.Log ("Commander Unit");
	}
}
