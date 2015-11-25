using UnityEngine;
using System.Collections;


/**
* @author HugoLS
* @version 1.0
**/
public abstract class Hero : Unit {

	protected float xpQuantity;
	protected float xpQuantityNextLevel;
	protected float xpQuantityLastLevel;
	protected float level;
	protected string handAttack;
	protected float powerQuantity;
	protected float maxPowerQuantity;
	protected float hpRefresh;
	protected float powerRefresh;
	protected bool defending;
	protected float blockingPercent;
	protected float range;
	protected bool specialCapacityUnlocked;
	protected bool specialCapacity;
	protected float lastCapacityUsed;
	public bool runBlocked;
	protected float lastRegenPower = 0.0f;
	public bool isInvicible = false;
	protected float invicibleTime = 0.0f;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		checkLevel();
		//adaptStatAccordingToLevel();
		if(specialCapacityUnlocked)
		{
			SpecialCapacitySpell();
		}
		if(!runBlocked){
			Run(Time.deltaTime);
		}

		if(isInvicible)
		{
			if(invicibleTime < Time.time)
			{
				isInvicible = false;
			}
		}
	}

	/**
	* FR:
	* Constructeur de la classe Hero
	* Les paramètres hp, damage, movementSpeed, attackType et name sont transmis lors de la construction à la classe mère (Unit) de Héro.
	* @see Unit
	*
	* EN:
	* Hero class constructor
	* Parameters hp, damage, movementSpeed, attackType and name are sent to the mother class (Unit) during the build of an Hero
	* @see Unit
	*
	* @param xpQuantity
	*					FR:
	*					Quantité d'expérience courante du Héro à créer
	*					EN:
	*					Current quantity of experience of the Hero
	* @param blockingPercent
	*					FR:
	*					Taux de blocage du personnage. Une valeur de 100 correspond à un taux de blocage de 100% des dégâts d'une attaque
	*					La valeur doit donc être comprise entre 0 et 100.
	*					EN:
	*					Block rate of the character. A value of 100 match to a block damage rate of 100% for an attack.
	*					The value has to be between 0 and 100.
	* @param handAttack
	*					FR:
	*					Cette variable détermine la main "forte" du joueur. Si il est droitier, alors la valeur de handAttack sera droite.
	*					EN:
	*					This variable determine the strong hand of a player. If he is right-handed, then the value should be "right"
	* @param powerQuantity
	*					FR:
	*					Quantité de pouvoir (fureur ou mana) courante du héro. Cette valeur est obligatoirement inférieur ou égale à la quantité maximum définie
	*					EN:
	*					Current quantity of power (furor or mana) of the Hero. This value must be equal or inferior to the maximum quantity defined.
	* @param hpRefresh
	*					FR:
	*					Taux de récupération des points de vie du héro.
	*					EN:
	*					Health Point refresh rate of the Hero.
	* @param powerRefresh
	*					FR:
	*					Taux de récupération des points de puissance du héro.
	*					EN:
	*					Power refresh rate of the Hero.
	* @param defending
	*					FR:
	*					Booléen permettant de définir si le héro est actuellement en train de se défendre.
	*					EN:
	*					Boolean which permit to define if the hero is defending or not.
	* @param hp
	*					FR:
	*					Quantité de point de vie du héro
	*					EN:
	*					Health Point quantity of the hero
	* @param damage
	*					FR:
	*					Nombre de dégât que le héro inflige en attaquant
	*					EN:
	*					Damage done when the hero attack
	* @param movementSpeed
	*					FR:
	*					Vitesse de déplacement du héros. La vitesse doit être calqué sur le modèle humain, soit 1 = 1m/s parcouru. 1,3 correspond à environ 5km/h
	*					EN:
	*					Movement speed of the Hero. The speed must be according to the human model,so 1 = 1m/s walked. 1,3m/s is according to 5km/h.
	* @param attackType
	*					FR:
	*					Permet de déterminer si le héros attaque à distance ou au corps à corps
	*					EN:
	*					Set if the Hero attack remotely or hand to hand
	* @param name
	*					FR:
	*					Nom du joueur
	*					EN:
	*					Name of the player
	* @version 1.0
	**/
	public Hero(float range, float xpQuantity,float blockingPercent, string handAttack, float powerQuantity, float hpRefresh, float powerRefresh, float hp, float damage, float movementSpeed, string attackType, string name)
		:base(hp, damage, movementSpeed, attackType, name){
		XpQuantity = xpQuantity;
		HandAttack = handAttack;
		PowerQuantity = powerQuantity;
		MaxPowerQuantity = powerQuantity;
		HpRefresh = hpRefresh;
		PowerRefresh = powerRefresh;
		BlockingPercent = blockingPercent;
		this.range = range;
		XpQuantityNextLevel = 100;
		XpQuantityLastLevel = 0;
		specialCapacityUnlocked = false;
		specialCapacity = false;
		runBlocked = false;
		defending = false;
	}

	/**
	* FR:
	* Getter/Setter de xpQuantity
	* EN:
	* Getter/Setter of xpQuantity
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float XpQuantity {
		get {
			return this.xpQuantity;
		}
		set {
			xpQuantity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de giveXP
	* EN:
	* Getter/Setter of giveXP
	* @return 
	* FR:
	*	Retourne un void pour le getter et void pour le setter
	* EN:
	*	Return an void for the getter and void for the setter
	* @version 1.0
	**/
	public void GiveXP(float XP) {
		XpQuantity += XP;
	}

	/**
	* FR:
	*	Actions à effectuer lorsque le héro à tué un PNJ
	* EN:
	*	Actions to do when the hero has killed a NPC
	* @version 1.0
	**/
	public virtual void HasKilled(float XP)
	{
		GiveXP(XP);
	}

	/**
	* FR:
	* Getter/Setter de xpQuantityNextLevel
	* EN:
	* Getter/Setter of xpQuantityNextLevel
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float XpQuantityNextLevel {
		get {
			return this.xpQuantityNextLevel;
		}
		set {
			xpQuantityNextLevel = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de xpQuantityLastLevel
	* EN:
	* Getter/Setter of xpQuantityLastLevel
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float XpQuantityLastLevel {
		get {
			return this.xpQuantityLastLevel;
		}
		set {
			xpQuantityLastLevel = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de level
	* EN:
	* Getter/Setter of level
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float Level {
		get {
			return this.level;
		}
		set {
			level = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de specialCapacityUnlocked
	* EN:
	* Getter/Setter of specialCapacityUnlocked
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	public bool SpecialCapacityUnlocked {
		get {
			return this.specialCapacityUnlocked;
		}
		set {
			specialCapacityUnlocked = value;
		}
	}


	/**
	* FR:
	* Getter/Setter de specialCapacity
	* EN:
	* Getter/Setter of specialCapacity
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	public bool SpecialCapacity {
		get {
			return this.specialCapacity;
		}
		set {
			specialCapacity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de runBlocked
	* EN:
	* Getter/Setter of runBlocked
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	public bool RunBlocked {
		get {
			return this.runBlocked;
		}
		set {
			runBlocked = value;
		}
	}
	

	/**
	* FR:
	* Getter/Setter de range
	* EN:
	* Getter/Setter of range
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float Range {
		get {
			return this.range;
		}
		set {
			range = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de lastCapacityUsed
	* EN:
	* Getter/Setter of lastCapacityUsed
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return a float for the getter and void for the setter
	* @version 1.0
	**/
	public float LastCapacityUsed {
		get {
			return this.lastCapacityUsed;
		}
		set {
			lastCapacityUsed = value;
		}
	}

	
	/**
	* FR:
	* Getter/Setter de blockingPercent
	* EN:
	* Getter/Setter of blockingPercent
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float BlockingPercent {
		get {
			return this.blockingPercent;
		}
		set {
			blockingPercent = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de handAttack
	* EN:
	* Getter/Setter of handAttack
	* @return 
	* FR:
	*	Retourne une string pour le getter et void pour le setter
	* EN:
	*	Return a string for the getter and void for the setter
	* @version 1.0
	**/
	public string HandAttack {
		get {
			return this.handAttack;
		}
		set {
			handAttack = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de powerQuantity
	* EN:
	* Getter/Setter of powerQuantity
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float PowerQuantity {
		get {
			return this.powerQuantity;
		}
		set {
			powerQuantity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de maxPowerQuantity
	* EN:
	* Getter/Setter of maxPowerQuantity
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float MaxPowerQuantity {
		get {
			return this.maxPowerQuantity;
		}
		set {
			maxPowerQuantity = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de hpRefresh
	* EN:
	* Getter/Setter of hpRefresh
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float HpRefresh {
		get {
			return this.hpRefresh;
		}
		set {
			hpRefresh = value;
		}
	}

	/**
	* FR:
	* Getter/Setter de powerRefresh
	* EN:
	* Getter/Setter of powerRefresh
	* @return 
	* FR:
	*	Retourne un float pour le getter et void pour le setter
	* EN:
	*	Return an float for the getter and void for the setter
	* @version 1.0
	**/
	public float PowerRefresh {
		get {
			return this.powerRefresh;
		}
		set {
			powerRefresh = value;
		}
	}

	/**
	* FR:
	* Cette fonction permet de calculer et d'infliger des dégâts suite à une attaque. Les dégâts infligés dépendent de la défense ou non du héro, mais aussi de son taux de blocage de dégât.
	* EN:
	* This function permit to process and inflict damages after an ennemy attack. Damages depend if the hero is defending or not but they depend of the blocking rate.
	* @param damage
	* FR:
	*	Quantité de dégâts brute que doit infliger la fonction au héro
	* EN:
	*	Damage quantity take by the hero
	* @return Return void
	* @version 1.0
	**/
	public virtual void LostHP(float damage)
	{
		float damageToLost = 0.0f;
		if(Defending)
		{
			damageToLost = damage - (blockingPercent*damage/100.0f);
		}
		else
		{
			damageToLost = damage;
		}

		base.LostHP(damageToLost);
	}

	/**
	* FR:
	* Cette fonction permet de vérifier le niveau actuel du héro en fonction de son expérience
	* EN:
	* This function permit to check the actual level of the hero according to his xp
	* @return Return void
	* @version 1.0
	**/
	public void checkLevel()
	{
		if(xpQuantity > xpQuantityNextLevel)
		{
			level += 1;
			xpQuantityNextLevel = 100 * Mathf.Pow(2,level);
			xpQuantityLastLevel = 100 * Mathf.Pow(2,level-1);
		}

		if(xpQuantity < xpQuantityLastLevel)
		{
			if(level > 1)
			{
				level -= 1;
				xpQuantityNextLevel = 100 * Mathf.Pow(2,level-1);
			}
			else
			{
				xpQuantityLastLevel = 0;
			}
		}
	}

	/**
	* FR:
	* Getter/Setter de defending
	* EN:
	* Getter/Setter of defending
	* @return 
	* FR:
	*	Retourne un bool pour le getter et void pour le setter
	* EN:
	*	Return a bool for the getter and void for the setter
	* @version 1.0
	**/
	protected bool Defending {
		get {
			return this.defending;
		}
		set {
			defending = value;
		}
	}

	/**
	* {@inheritDoc}
	**/
	public override void Run(float deltaTime)
	{
		transform.Translate(base.MovementSpeed * Vector3.forward * deltaTime, Space.World);
	}

	/**
	* FR:
	* Cette fonction permet de passer le héro en mode défensif ou en mode non défensif en fonction du paramètre d'entrée
	* EN:
	* Permits to switch the hero between defensive mode and no defensive mode in function of the input
	* @param mode
	* FR:
	*	Ce paramètre doit être égale à "off" pour sortir du mode défense, et à "on" pour passer en mode défense
	* EN:
	* 	This paramater have to be equal to "off" to leave the defense mode and equal to "on" to enter in defense mode
	* @return Return void
	* @version 1.0
	**/
	public void DefenseMode(string mode){
		if (mode == "off") {
			Defending = false;
		} else {
			Defending = true;
		}
	}

	/**
	* FR:
	* Cette fonction permet au héro d'adapter ses statistiques en fonction de son niveau
	* EN:
	* Permits to the hero to adapt his stats according to his level
	* @return Return void
	* @version 1.0
	**/
	public virtual void adaptStatAccordingToLevel()
	{

	}

	/**
	* FR:
	* Cette fonction permet au héro de déclencher sa capacité spéciale
	* EN:
	* Permits to the hero to start his special capacity
	* @return Return void
	* @version 1.0
	**/
	public virtual void SpecialCapacitySpell()
	{

	}

	/**
	* FR:
	* Cette fonction permet au héro de régénérer sa source de pouvoir
	* EN:
	* Permits to the hero to refresh is power source
	* @return Return void
	* @version 1.0
	**/
	public void RegenPower()
	{
		if(PowerQuantity + PowerRefresh < MaxPowerQuantity)
		{
			PowerQuantity += PowerRefresh;
		}
		else
		{
			PowerQuantity = MaxPowerQuantity;
		}
		lastRegenPower = Time.time;
	}


	/**
	* FR:
	* Cette fonction permet de retirer le héro du gameController
	* EN:
	* Permits to remove the hero from the Game Controler
	* @return Return void
	* @version 1.0
	**/
	public void OnDestroy(){
		GameModel.HerosInGame.Remove (this);
	}

	/**
	* FR:
	* Cette fonction permet au héro de déclencher des collisions
	* EN:
	* Permits to the hero to start collisions
	* @return Return void
	* @version 1.0
	**/
	void OnTriggerEnter(Collider hit)
	{
		Debug.LogWarning("COLLISION HERO "+hit);
		if(hit.gameObject.tag == "ennemy_weapon")
		{
			NPC ennemy = hit.GetComponentInParent<NPC>();
			if (!Defending && !isInvicible){
				LostHP(ennemy.Damage);
				if (!Defending){
					PlayBloodAnimation();
				}
			}
		}
		else if(hit.gameObject.tag == "ennemy_projectile")
		{
			NPC ennemy = hit.GetComponentInParent<NPC>();
			LostHP(ennemy.Damage);
			if (!Defending){
				PlayBloodAnimation();
			}
			Destroy(hit.gameObject);
			//Debug.LogWarning("COLLISION HERO -> destroy "+hit);
		}
		else if(hit.gameObject.tag == "trigger_aggro")
		{
			NPC ennemy = hit.GetComponentInParent<NPC>();
			ennemy.DistanceUnderAggroDist = true;
			Destroy(hit.gameObject);
		}
		else if(hit.gameObject.tag == "trigger_attack")
		{
			NPC ennemy = hit.GetComponentInParent<NPC>();
			ennemy.DistanceUnderAttackDist = true;
			Destroy(hit.gameObject);
		}
	}

	/**
	* FR:
	* Cette fonction permet de démarrer l'animation de sang lorsque le héro est blessé
	* EN:
	* Permits to start the blood animation when the hero is hurted
	* @return Return void
	* @version 1.0
	**/
	public void PlayBloodAnimation(){
		//Debug.Log ("BLOODY");
		Animator anim = Camera.main.GetComponent<Animator>();
		anim.SetTrigger ("bloody");
		anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
	}

	/**
	* FR:
	* Cette fonction permet au héro de devenir invincible
	* EN:
	* Permits to the hero to be invincible
	* @return Return void
	* @version 1.0
	**/
	public void makeInvicible(float time)
	{
		invicibleTime = Time.time + time;
		isInvicible = true;
	}

	/**
	* FR:
	* Cette fonction permet de d'effectuer certaines choses avant qu'une attaque se produise.
	* EN:
	* Permits to do some things before an attack
	* @return Return void
	* @version 1.0
	**/
	public virtual void PreAttack()
	{
		
	}

	/**
	* FR:
	* Cette fonction permet de d'effectuer certaines choses après qu'une attaque se produise.
	* EN:
	* Permits to do some things after an attack
	* @return Return void
	* @version 1.0
	**/
	public virtual void PostAttack()
	{
		
	}
}
