using UnityEngine;
using System.Collections;

public class OnagerInfo : UnitBase {
	
	override public void init() {
		this.maxHP = 15;
		this.currentHP = this.maxHP;
		this.minAttackRange = 3;
		this.maxAttackRange = 6;
		this.movement = 3;
		this.attackPow = 6;
		this.foodCost = 0;
		this.lumberCost = 10;
		this.unitType = "Siege";
		Debug.Log ("Onager Unit");
	}
}
