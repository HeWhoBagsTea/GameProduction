using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleAI : MonoBehaviour {

	public Player controller;
	public int priority;
	private UnitBase myUnit;
	
	void Start () {
		this.myUnit = this.gameObject.GetComponent<UnitBase> ();
	}

	void Update () {
		bool previous = false;

		if(this.priority != 0) {
			foreach(SimpleAI a in FindObjectsOfType(typeof(SimpleAI)))
			{
				if(a.priority == (this.priority - 1)){
					previous = true;
				}
			}

			if(!previous) {
				this.priority--;
			}
		}


		if(NewGameController.currentPlayer != this.controller || this.myUnit.isDone) {
			return;
		}


		if (NewGameController.AImovePriority == this.priority) {

			if(tryAttack()) {
				Debug.Log("hit");
				Attack(getUnitInAttackRange());
				this.myUnit.isDone = true;
				StartCoroutine(waitForNext());
				return;
			}
			else{
				if(tryCapture()){
					StartCoroutine(waitForNext());
					return;
				}
				else {
					Debug.Log("Moving");
					if(tryMove()){
						Move (getMoveTarget());
					}
					if(tryAttack())
					{
						Attack(getUnitInAttackRange());
					}
					this.myUnit.isDone = true;
					StartCoroutine(waitForNext());
					return;
				}
			}
		}
	}

	bool tryAttack()
	{
		return getUnitInAttackRange() != null;
	}

	bool tryMove()
	{
		return getMoveTarget () != null;
	}

	bool tryCapture()
	{
		myUnit.captureTile (myUnit.currentSpace);
		return myUnit.isDone;
	}

	void Move(TileStandard location)
	{
		this.myUnit.moveUnit (location);
	}

	void Attack(UnitBase target) 
	{
		this.myUnit.attackUnit (target);
	}

	TileStandard getMoveTarget()
	{
		this.myUnit.showMovement ();

		List<TileStandard> moveSpaces = new List<TileStandard> ();
		TileStandard moveTarget = null;
		foreach(TileStandard t in FindObjectsOfType(typeof(TileStandard)))
		{
			if(t.GetComponent<TileStandard>() != null && t.GetComponent<TileStandard>().getTerrainMatName().Equals("MovementSpace") && t.GetComponent<TileStandard>().unitOnTile == null && t.GetComponent<TileStandard>().controller != this.controller)
			{
				moveSpaces.Add(t);
			}
		}

		if(moveSpaces.Count > 0)
		{
			moveTarget = moveSpaces[0];
		}

		return moveTarget;
	}

	UnitBase getUnitInAttackRange()
	{
		this.myUnit.showAttack ();

		List<TileStandard> realAtkRange = new List<TileStandard> ();
		List<UnitBase> unitsInAttackRange = new List<UnitBase> ();
		UnitBase unitToAtk = null;
		foreach(TileStandard t in FindObjectsOfType(typeof(TileStandard)))
		{
			if(t.GetComponent<TileStandard>() != null && t.GetComponent<TileStandard>().getTerrainMatName().Equals("AttackSpace"))
			{
				realAtkRange.Add(t);
			}
		}

		foreach(TileStandard t in realAtkRange) 
		{
			if(t.unitOnTile != null) 
			{
				unitsInAttackRange.Add(t.unitOnTile);
			}
		}

		if (unitsInAttackRange.Count > 0) {
			unitToAtk = unitsInAttackRange[0];
			foreach(UnitBase u in unitsInAttackRange) 
			{
				if(u.HPcurr < unitToAtk.HPcurr)
				{
					unitToAtk = u;
				}
			}
		}

		NewGameController.clearHighlights ();

		return unitToAtk;
	}

	IEnumerator waitForNext()
	{
		yield return new WaitForSeconds (.75f);
		NewGameController.AImovePriority++;
	}


}
