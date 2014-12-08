using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleAI : MonoBehaviour {
	
	public Player controller;
	public int priority;
	public int HighestPri;
	public int UnitToBuild = 0;
	private bool once = true;
	private UnitBase myUnit;
	private UnitBuilding myBarracks;
	private Vector3 myRotation;
	
	void Start () {
		this.myUnit = this.gameObject.GetComponent<UnitBase> ();
		myRotation = new Vector3 (0, 270, 0);

	}
	
	void Update () {
		bool previous = false;
		int i = -1;
		if(this.priority != 0) {
			foreach(SimpleAI a in FindObjectsOfType(typeof(SimpleAI)))
			{
				i++;
				if(a.priority == (this.priority - 1)){
					previous = true;
				}
				if(a.GetComponent<UnitBuilding>())
				{
					this.myBarracks = a.GetComponent<UnitBuilding>();
				}
			}
			if(i > HighestPri)
			{
				HighestPri = i;
			}
			//Debug.Log(HighestPri);
			if(!previous) {
				this.priority--;
			}
		}
		if(!this.myUnit.isDone)
		{
			once = true;
		}
		
		if(NewGameController.AImovePriority == this.priority && this.myUnit.isDone && once)
		{
			once = false;
			StartCoroutine(waitForNext());
		}
		if(NewGameController.currentPlayer != this.controller || this.myUnit.isDone) {
			return;
		}
		
		
		if (NewGameController.AImovePriority == this.priority) {
			//Debug.Log (this);
			
			if(tryAttack()) {
				//Debug.Log("hit");
				Attack(getUnitInAttackRange());
				this.myUnit.isDone = true;
				//StartCoroutine(waitForNext());
				return;
			}
			else{
				if(tryCapture()){
					//StartCoroutine(waitForNext());
					return;
				}
				else {
					//Debug.Log("Moving");
					if(tryMove()){
						Move (getMoveTarget());
					}
					if(tryAttack())
					{
						Attack(getUnitInAttackRange());
					}
					this.myUnit.isDone = true;
					//StartCoroutine(waitForNext());
					return;
				}
			}
		}



		if(NewGameController.AImovePriority == this.myBarracks.Priority)
		{
			BuildNextInBuildList(this.myBarracks.units);
			return;
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
			if(t.GetComponent<TileStandard>() != null && t.GetComponent<TileStandard>().getTerrainMatName().Equals("MovementSpace") )
			{
				if(t.GetComponent<TileStandard>().unitOnTile == null)
				{
					moveSpaces.Add(t);
				}
			}
		}
		
		if(moveSpaces.Count > 0)
		{
			moveTarget = moveSpaces[0];
		}
		foreach (TileStandard t in moveSpaces) {
			tileIsTactical(t,moveTarget);
		}
		foreach (TileStandard K in moveSpaces) {
			if(K.Priority >= moveTarget.Priority){
				moveTarget = K;
			}
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
				if(u.unitClass.Equals("Commander") && !(unitToAtk.HPcurr <= this.myUnit.attackPow)){
					unitToAtk = u;
				}
			}
		}
		
		NewGameController.clearHighlights ();
		return unitToAtk;
	}
	
	IEnumerator waitForNext()
	{
		yield return new WaitForSeconds (1.0f);
		NewGameController.AImovePriority++;
	}
	
	//Negative Modification<First Avaliable<DefensiveValue<ResourceValue<Isnot Owned by Self with higer Resource<Positive Modification
	public void tileIsTactical(TileStandard target, TileStandard previousTarget)
	{
		target.Priority = 0;
		bool isFirstAvaliable = true;
		if (getTerrainValue(target) > getTerrainValue(previousTarget) && target.getControlRingMatName() != this.controller.playerColor.ToString()) {
			target.Priority += getTerrainValue(target);
			isFirstAvaliable = false;
		} 
		if (GivesBuff (target) > 0) {
			target.Priority += 3; 
			isFirstAvaliable = false;
		} 
		if (target.defensiveValue > previousTarget.defensiveValue) {
			target.Priority += 3;
			isFirstAvaliable = false;
		}
		if ( target.GetComponent<TileStandard>().getControlRingMatName() != this.controller.playerColor.ToString()) {
			target.Priority += 5;
			isFirstAvaliable = false;
		} 
		if(isFirstAvaliable){
			target.Priority = 1;
		}
	}
	public int getTerrainValue(TileStandard target)	{
		int value = 0;
		if (target.ResourceType == "Food") {
			value = 4 * target.ResourceValue;
		} else if (target.ResourceType == "Ore") {
			value = 3 * target.ResourceValue;
		} else if (target.ResourceType == "Lumber") {
			value = 2 * target.ResourceValue;
		} else {
			value = 1;
		}
		return value;
	}
	public int GivesBuff(TileStandard target){
		int providesBuff = 0;
		if (this.myUnit.unitClass == "Archer") {
			if (target.TerrainName == "Mountain") {
				providesBuff = 1;
			}
		}
		if (this.myUnit.unitClass == "Melee") {
			if(target.TerrainName == "Hills"){
				providesBuff = 1;
			}
		}
		if (this.myUnit.unitClass == "Mage") {
			if(target.TerrainName == "ManaWell"){
				providesBuff = 1;
			}
		}
		if(target.TerrainName == "Wall"){
			providesBuff = 2;
		}
		return providesBuff;
	}
	
	//Build Method
	public void BuildNextInBuildList(GameObject[] unitList)
	{
		//Finds the current HighestPriority in the running game.

		//First Check if you have the avaliable resources to build
		if (CheckResources (unitList[UnitToBuild].GetComponent<UnitBase>())) {
			//If you can build check the spaces around barracks, take the first one you can build on
			this.myBarracks.BuildTarget = findBuildtarget();
			//using the highestPri +=1, assign the new unit's priority value
			GameObject prefab = unitList[UnitToBuild];
			UnitToBuild ++;
			//when nextbuild unit is greater than the list's size reset to 0
			if(UnitToBuild >2)
			{
				UnitToBuild = 0;
			}
			GameObject instantiate = Instantiate (
				prefab, (this.myBarracks.BuildTarget.transform.position),
				this.myBarracks.transform.rotation) as GameObject;	
			instantiate.GetComponent<SimpleAI>().priority = HighestPri++;
			instantiate.GetComponent<SimpleAI>().controller = myBarracks.controller;
			instantiate.transform.rotation = Quaternion.Euler (myRotation);
			instantiate.GetComponent<UnitBase> ().isDone = true;
			this.myBarracks.BuildTarget = null;
			
			//breaks the method everything after doesn't run
			instantiate.GetComponent<UnitBase> ().giveControl (transform.FindChild ("ControlRing").GetComponentInChildren<MeshRenderer> ().material);
		

		}

	}
	
	public bool CheckResources(UnitBase unitInQuestion)
	{
		//Needs to check if you can build the next unit in your build list
		bool meetCost = false;
		if (this.controller.FoodPool >= unitInQuestion.foodCost && 
		    this.controller.LumberPool >= unitInQuestion.lumberCost && 
		    this.controller.OrePool >= unitInQuestion.oreCost) {
			meetCost = true;
			this.controller.FoodPool -= unitInQuestion.foodCost;
			this.controller.LumberPool -= unitInQuestion.lumberCost;
			this.controller.OrePool -= unitInQuestion.oreCost;
		}
		return meetCost;
		// Will cycle through each unit and build when resources are avaliable, will not move to the next unit unless the unit is built.
	}
	public TileStandard findBuildtarget()
	{
		return this.myBarracks.AIbuildSpaces();
	}
}

