﻿using UnityEngine;
using System.Collections;

public class RangeInfo : UnitBase {
	
	override public void init() {
		this.maxHP = 12;
		this.currentHP = this.maxHP;
		this.minAttackRange = 2;
		this.maxAttackRange = 5;
		this.movement = 5;
		this.attackPow = 6;
		this.foodCost = 4;
		this.lumberCost = 0;
		this.unitType = "Infantry";
		Debug.Log ("Range Unit");
	}
}