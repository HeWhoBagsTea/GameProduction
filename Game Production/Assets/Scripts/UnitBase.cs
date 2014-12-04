using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitBase : MonoBehaviour {

	public Player controller;
	public TileStandard currentSpace;
	public Material[] unitColors;
	public Material[] spaceHighlights;
	public AudioClip attackingSound;
	public AudioClip takeDamageSound;
	public GameObject damageParticleFX;
	public GUISkin mySkin;

	public bool isSelected = false;
	protected bool hasMoved = false;
	protected bool hasActioned = false;
	public bool isDone = false;

	private bool show = true;

	//Upkeep Stuff
	public int UpkeepCost = 0;
	public bool isFirstTurn = true;

	//Units Stats
	public int movement = 2;
	public int minAttackRange = 1;
	public int maxAttackRange = 1;
	public int attackPow = 1;

	public bool hasBeenUpgraded = false;

	//Original Stats
	public int OriginalMovement = 2;
	public int OriginalMinAttackRange = 1;
	public int OriginalMaxAttackRange = 1;
	public int OriginalAttackPow = 1;

	public int HPmax = 1;
	public int HPcurr = 1;
	public int foodCost = 0;
	public int lumberCost = 0;
	public int oreCost = 0;
	public string unitType = "";
	public string unitClass = "";

	public Vector3 posOffset;

	//Button Stuff;
//	private float BUTTON_X_POS = Screen.width - (Screen.width / 8);
	private float BUTTON_WIDTH = Screen.width/9;
	//private float BUTTON_HEIGHT = Screen.height/20;
	//private float BUTTON_SPACING = Screen.height/100 + Screen.height/20;
	
	//Unit Hp Stuff;
	private float HP_X_POS; //(Works just fine without this) = Screen.width * 0.45f;
	private float HP_Y_POS; //(Works just fine without this) = 0;
	private float HP_WIDTH = 100;
	private bool entered = false;
	
	//Unit Stat Stuff
	private float STAT_BOX_X_POS = 0;
	private float STAT_BOX_Y_POS = 10;
	private float STAT_BOX_WIDTH = Screen.width/3;
	private float STAT_BOX_HEIGHT = Screen.height/30;
	private float STAT_BOX_OFFSET = Screen.height/100 + Screen.height/30;
	
	//Tile Stat
	private float TILE_BOX_X_POS = 10;
	private float TILE_BOX_Y_POS = 10;

	//Text Sizing
	private int TextSize = (int)Screen.height/50;


	private float FoodxDmg = -100;
	private float FoodyDmg = -100;
	private float FoodoffsetDmg = 0;

	//Reused values
	//private float HP_HEIGHT = Screen.height/30;
	//private float TILE_BOX_HEIGHT = Screen.height/30;
	//private float TILE_BOX_OFFSET = Screen.height/100 + Screen.height/30;
	//private float TILE_BOX_WIDTH = Screen.width/9;

	private Vector4 foodColor= new Vector4 (0.88f, 0.68f, 0.01f, 1.0f);
	private Vector4 red = new Vector4( 1.0f, 0, 0, 1.0f);

	//use this to modify unit stats
	public virtual void init() {

	}

	// Use this for initialization
	void Start () {
		posOffset = new Vector3 (0 , .5f, 0);
		init ();

		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject i in players) {
			if(i.GetComponent<Player>() != null && getMaterialName() == i.GetComponent<Player>().getPlayerColor()) {
				this.controller = i.GetComponent<Player>();
			}
		}

		this.currentSpace = getClosestTile ();
		this.currentSpace.unitOnTile = this;
		this.transform.position = this.currentSpace.transform.position + this.posOffset;
	}

	//Called when mouse is over unit
	void OnMouseEnter()
	{
		entered = true;
	}

	//Called when mouse leaves unit
	void OnMouseExit()
	{
		entered = false;
	}
	
	//Called when unit is pressed
	void OnMouseUpAsButton() {
		if(!enabled)
		{
			return;
		}

		if(NewGameController.selectedUnit == this && !this.hasMoved && !this.hasActioned)
		{
			if(show)
			{
				this.showAttack();
				show = false;
			}
			else
			{
				this.showMovement();
				show = true;
			}
		}
		else if (NewGameController.currentPlayer == this.controller) {
			this.selected ();
		} else if (NewGameController.selectedUnit != null && this.currentSpace.canAttackUnitOnThis) {
			NewGameController.selectedUnit.attackUnit(this);
		}
	}

	protected virtual void buffMe() {

	}

	// Update is called once per frame
	void Update () {
		if(!enabled)
		{
			return;
		}


		buffMe ();
		if (this.hasMoved && this.hasActioned) {
			this.isDone = true;
		}
		else if (this.hasMoved || this.hasActioned) {
			this.renderer.material = this.unitColors[this.controller.playerID + (this.unitColors.Length/2) + 1] ;
			this.transform.FindChild("unit").renderer.material = this.unitColors[this.controller.playerID + (this.unitColors.Length / 2) + 1];
		}
		
		if(this.isDone) {
			this.hasMoved = true;
			this.hasActioned = true;
			this.renderer.material = this.unitColors[this.controller.playerID + (this.unitColors.Length / 3)];
			this.transform.FindChild("unit").renderer.material = this.unitColors[this.controller.playerID + (this.unitColors.Length / 3)];
		}
		
		if (this.HPcurr <= 0) {
			audio.PlayOneShot (takeDamageSound);
			GameObject temp;
			temp = Instantiate(damageParticleFX, this.transform.position, this.transform.rotation) as GameObject;
			temp.particleSystem.Play();

			Destroy(this.gameObject);
		}
	}

	void OnGUI() {
		STAT_BOX_X_POS = Screen.width * 0.5f - (STAT_BOX_WIDTH/2);
		
		HP_X_POS = Camera.main.WorldToScreenPoint (this.transform.position).x - (HP_WIDTH/2);
		HP_Y_POS = Screen.height - Camera.main.WorldToScreenPoint (this.transform.position).y - 40;
		GUI.skin.box.alignment = TextAnchor.UpperCenter;

		GUI.skin.box.fontSize = TextSize;
		GUI.skin.button.fontSize = TextSize;
		GUI.skin.label.fontSize = TextSize;
		
		GUI.color = new Vector4(0.23f, 0.75f, 0.54f, 1);

		if (isSelected) {
			GUI.Box (new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS, STAT_BOX_WIDTH, STAT_BOX_HEIGHT), 
			         "Unit Stats:", mySkin.GetStyle("Box"));
			GUI.Box(new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS + (STAT_BOX_OFFSET * 1), STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
			        "HP:" + this.HPcurr + "/"+this.HPmax +
			        " AttackRange: " + this.minAttackRange + "-"+ this.maxAttackRange, mySkin.GetStyle("Box"));
			GUI.Box(new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS + (STAT_BOX_OFFSET * 2), STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
			        "Movement: " + this.movement +
			        " Attack Power: " + this.attackPow, mySkin.GetStyle("Box"));
			GUI.Box(new Rect (STAT_BOX_X_POS, STAT_BOX_Y_POS + (STAT_BOX_OFFSET * 3), STAT_BOX_WIDTH, STAT_BOX_HEIGHT),
			        "Unit Type: " + this.unitClass +
			        " Upkeep Cost: " + this.UpkeepCost + " Defense Bonus: "+this.currentSpace.defensiveValue, mySkin.GetStyle("Box"));
		}


		if (entered) {
			//GUI.color = (this.controller == NewGameController.currentPlayer) ? new Vector4(0f, 0f, .6f, 1f) : Color.red;

			if(this.controller == NewGameController.currentPlayer){
				GUI.color = new Vector4(0.2f, 1.0f, 0.2f, 1.0f);
			}
			else {
				GUI.color =new Vector4(1.0f, 1.0f, 0.0f, 1.0f);
			}
			GUI.skin.box.fontStyle = FontStyle.Bold;
			GUI.Label (new Rect (HP_X_POS, HP_Y_POS - STAT_BOX_OFFSET, HP_WIDTH, STAT_BOX_HEIGHT),
			           "HP:" + this.HPcurr + "/" + this.HPmax, mySkin.GetStyle("Box"));


			TILE_BOX_X_POS = Camera.main.WorldToScreenPoint (this.transform.position).x - (BUTTON_WIDTH/2);
			TILE_BOX_Y_POS = Screen.height - Camera.main.WorldToScreenPoint (this.transform.position).y + 30;

			GUI.skin.box.fontStyle = FontStyle.Normal;
			GUI.color = Color.cyan;
			GUI.Label (new Rect (TILE_BOX_X_POS+(BUTTON_WIDTH/4), TILE_BOX_Y_POS, BUTTON_WIDTH/2, STAT_BOX_HEIGHT*2),
			         this.currentSpace.TerrainName + "\n" + this.currentSpace.ResourceType +  " " + this.currentSpace.ResourceValue, mySkin.GetStyle("Box"));
			
			//if(this.controller != null) {
			//	GUI.color = this.controller.getColor();
			//	GUI.Box (new Rect (TILE_BOX_X_POS, TILE_BOX_Y_POS + STAT_BOX_OFFSET, BUTTON_WIDTH, STAT_BOX_HEIGHT),
			//	         "Owner: " + this.controller.getPlayerID(), mySkin.GetStyle("Box"));
			//}
		}

		GUI.color = foodColor;
		GUI.skin.label.fontSize = 28;
		GUI.Label (new Rect (this.FoodxDmg, this.FoodyDmg - this.FoodoffsetDmg, 100, 35), "-1");

		//if (isSelected) {
		//	Rect attackButton = new Rect (BUTTON_X_POS, Screen.height - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		//	Rect moveButton = new Rect (BUTTON_X_POS, attackButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		//	Rect harvestButton = new Rect(BUTTON_X_POS, moveButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		//	Rect captureButton = new Rect(BUTTON_X_POS, harvestButton.position.y - BUTTON_SPACING, BUTTON_WIDTH, BUTTON_HEIGHT);
		//	
		//	
		//	GUI.color = (!this.hasActioned) ? Color.white : Color.gray;
		//	if (GUI.Button (attackButton, "Attack")) {
		//		showAttack();
		//	}
		//	
		//	GUI.color = (!this.hasMoved) ? Color.white : Color.gray;
		//	if (GUI.Button (moveButton, "Move")) {
		//		showMovement();
		//	}
		//	
		//	//GUI.color = (!this.hasActioned) ? Color.white : Color.gray;
		//	//if (GUI.Button (harvestButton, "Harvest")) {
		//	//	harvestTile(this.currentSpace);
		//	//}
		//	GUI.color = (!this.hasActioned && !this.hasMoved) ? Color.white : Color.gray;
		//	if (GUI.Button (harvestButton, "Capture")) {
		//		captureTile(this.currentSpace);
		//	}
		//}
	}

	public void attackUnit (UnitBase target) {
		int Damage = 0;
		if (!FullTutorial.TutorialActive) {
			Damage = this.attackPow;
			Damage -=target.currentSpace.defensiveValue;
			target.HPcurr -= Damage;
			this.hasActioned = true;
			audio.PlayOneShot (attackingSound);
			if (target.HPcurr > 0) {
				StartCoroutine (hurtSound (target, red));
				StartCoroutine (damageTaken (target,Damage));
			}
			if (this.hasMoved) {
				deselect ();
			} else {
				buffMe ();
				selected ();
			}
		} else {
			TutorialAttackUnit(target);
		}
	}
	public void TutorialAttackUnit (UnitBase target) {
		int Damage = 0;
		Damage = this.attackPow;
		Damage -=target.currentSpace.defensiveValue;
		target.HPcurr -= Damage;
		this.hasActioned = true;
		audio.PlayOneShot (attackingSound);
		
		if(target.HPcurr > 0) {
			StartCoroutine (hurtSound (target, red));
			StartCoroutine (damageTaken(target,Damage));
		}
		
		if (!FullTutorial.unitAttack) {
			FullTutorial.unitAttack = true;
			FullTutorial.TutorialActive = false;
			selected();
		}
	}

	public void moveUnit(TileStandard moveLocation) {
		this.currentSpace.unitOnTile = null;
		this.currentSpace = moveLocation;
		this.currentSpace.unitOnTile = this;
		this.transform.position = this.currentSpace.transform.position + this.posOffset;
		this.hasMoved = true;

		if(!FullTutorial.movedAUnit)
		{
			FullTutorial.movedAUnit = true;
		}

		if (this.hasActioned) {
			deselect ();
		} else {
			buffMe();
			selected();
		}
	}

	//Eat Well
	public void EatWell()
	{
		if (this.controller == NewGameController.currentPlayer) {
			if (!this.isFirstTurn) {
				this.controller.FoodPool -= this.UpkeepCost;
				if (this.controller.FoodPool < this.UpkeepCost) {
					StartCoroutine(hurtSound(this, foodColor));
					StartCoroutine(this.tooLittleFood());
					this.HPcurr--;
					this.controller.FoodPool = 0;
				}
			} else if (this.controller == NewGameController.currentPlayer) {
				this.isFirstTurn = false;
			}
		}
	}

	//Harvest Method
	public void harvestTile(TileStandard currentLocation){
		if (!this.hasActioned && this.currentSpace.ResourceValue >0) {
			this.currentSpace.hasBeenHarvested = true;
			this.currentSpace.ResourceValue--;
			if(this.currentSpace.ResourceType.Equals("Food"))
			{
				this.controller.FoodPool ++;
			}
			else if(this.currentSpace.ResourceType.Equals("Lumber"))
			{
				this.controller.LumberPool ++;
			}
			else if(this.currentSpace.ResourceType.Equals("Ore"))
			{
				this.controller.OrePool ++;
			}
			this.hasActioned = true;
			deselect ();
		}
	}
	//Capture
	public void captureTile(TileStandard currentLocation){
		if (!this.hasActioned && !this.hasMoved && this.currentSpace.controller != this.controller) {
			this.currentSpace.hasBeenHarvested = true;
			this.currentSpace.setControl (this.controller);
			this.hasMoved = true;
			this.hasActioned = true;

			if(FullTutorial.progress == 14){
				FullTutorial.firstCapture = true;
			}

			deselect ();
		}
	}

	public void startTurn() {
		this.EatWell();
	}

	public void resolveTurn() {
		this.hasMoved = false;
		this.hasActioned = false;

		this.isDone = false;
		//this.EatWell();
		this.renderer.material = this.unitColors [this.controller.playerID];
		if (this.transform.FindChild ("unit") != null) {
			this.transform.FindChild ("unit").renderer.material = this.unitColors [this.controller.playerID];
		}
		else {
			this.transform.renderer.material = this.unitColors[this.controller.playerID];
		}
		deselect ();
	}

	public void selected () {
		NewGameController.deselectAllUnits ();
		NewGameController.selectedUnit = this;
		highlightCurrentSpace (spaceHighlights[1]);
		isSelected = true;

		if (!hasActioned) {
			showAttack();
		}

		if (!hasMoved) {
			showMovement();
		}
	}

	public void deselect () {
		if (NewGameController.selectedUnit == this) {
			NewGameController.selectedUnit = null;
		}
		NewGameController.clearHighlights ();
		this.show = true;
		isSelected = false;
	}

	public string getMaterialName() {
		string matName = renderer.material.name;
		matName = matName.Substring (0, matName.IndexOf (" ("));

		return matName;
	}

	public void giveControl(Material mat) {
		this.renderer.material = mat;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject i in players) {
			if(i.GetComponent<Player>() != null && getMaterialName() == i.GetComponent<Player>().getPlayerColor()) {
				this.controller = i.GetComponent<Player>();
			}
		}
	}

	private void highlightCurrentSpace(Material highlight) {
		MeshRenderer currentSpaceTile = currentSpace.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer> ();
		currentSpaceTile.material = highlight;
	}

	public void showAttack() {
		if (!hasActioned) {
			NewGameController.clearHighlights();
			showAttackHelper(this.maxAttackRange, this.currentSpace, this.spaceHighlights[3]);
			showAttackHelper(this.minAttackRange - 1, this.currentSpace, this.spaceHighlights[0]);
			highlightCurrentSpace(this.spaceHighlights[1]);
		}
	}
	
	private void showAttackHelper(int attackRange, TileStandard tile, Material mat) {

		if (attackRange > 0) {
			Collider[] hitCollider = Physics.OverlapSphere(tile.transform.position, 4);
			List<TileStandard> tiles = new List<TileStandard>();
			
			foreach(Collider i in hitCollider) {
				if(i.GetComponent<TileStandard>() != null){
					tiles.Add(i.GetComponent<TileStandard>());
				}
			}
			
			foreach(TileStandard i in tiles) {
				if((i.unitOnTile != null && i.unitOnTile.controller != this.controller) || i.unitOnTile == null) {
					i.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = mat;
					if(mat == this.spaceHighlights[3]) {
						i.canAttackUnitOnThis = true;
					} 
					else {
						i.canAttackUnitOnThis = false;
					}
				}
			}

			foreach(TileStandard i in tiles) {
				showAttackHelper(attackRange - 1, i, mat);
			}

		}
	}

	public void showMovement() {
		if (!hasMoved) {
			NewGameController.clearHighlights();
			showMovementRangeHelper(this.movement, this.currentSpace);
			highlightCurrentSpace(this.spaceHighlights[1]);
		}
	}
	
	private void showMovementRangeHelper(int moveRange, TileStandard tile) {
		if (moveRange > 0) {
			Collider[] hitCollider = Physics.OverlapSphere(tile.transform.position, 4);
			List<TileStandard> tiles = new List<TileStandard>();

			foreach(Collider i in hitCollider) {
				if(i.GetComponent<TileStandard>() != null) {// && !i.GetComponent<TileStandard>().canMoveTo){
					tiles.Add(i.GetComponent<TileStandard>());
				}
			}

			foreach(TileStandard i in tiles) {
				if(!i.unitOnTile && (moveRange - i.moveCost) >= 0) {
					i.transform.FindChild("Terrain").GetComponentInChildren<MeshRenderer>().material = this.spaceHighlights[2];
					i.canMoveTo = true;
				}
			}

			foreach(TileStandard i in tiles) {
				showMovementRangeHelper(moveRange - i.moveCost, i);
			}
		}
	}
	
	private TileStandard getClosestTile() {
		GameObject[] tiles = GameObject.FindGameObjectsWithTag ("Tile");
		GameObject closest = null;
		foreach(GameObject i in tiles) {
			if(!closest) {
				closest = i;
			}
			
			if(Vector3.Distance(this.transform.position, i.transform.position) <= Vector3.Distance(this.transform.position, closest.transform.position)) {
				closest = i;
			}
		}
		
		return closest.GetComponent<TileStandard>();
	}

	private IEnumerator tooLittleFood()
	{
		this.FoodxDmg = Camera.main.WorldToScreenPoint (this.transform.position).x - 20;
		this.FoodyDmg = Screen.height - Camera.main.WorldToScreenPoint (this.transform.position).y - 40;

		this.FoodoffsetDmg = 0;

		for(int i = 0; i < 50; i++){
			yield return new WaitForSeconds (.005f);
			this.FoodoffsetDmg += 1;
		}

		this.FoodxDmg = -100;
		this.FoodyDmg = -100;
	}

	private IEnumerator damageTaken(UnitBase target, int DamageDealt) {
		NewGameController.attackingUnitPow = DamageDealt;

		NewGameController.xPos = Camera.main.WorldToScreenPoint (target.transform.position).x - 20;
		NewGameController.yPos = Screen.height - Camera.main.WorldToScreenPoint (target.transform.position).y - 40;


		NewGameController.yOffset = 0;

		for(int i = 0; i < 50; i++){
			yield return new WaitForSeconds (.005f);
			NewGameController.yOffset += 1;
		}

   		NewGameController.xPos = -100;
    	NewGameController.yPos = -100;

	}

	private IEnumerator hurtSound(UnitBase target, Color particleColor){
		yield return new WaitForSeconds (.25f);
		audio.PlayOneShot (takeDamageSound);
		GameObject temp;
		temp = Instantiate(damageParticleFX, target.transform.position, this.transform.rotation) as GameObject;
		temp.particleSystem.startColor = particleColor;
		temp.particleSystem.Play();
	}
}
