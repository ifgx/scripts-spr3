using UnityEngine;
using System.Collections;

/**
 * An Item is a part of the level
 * It can be an ennemy or a potion
 */
public class Item {

	/**
	 * the type of the Item. ex : "fireDragonet", "health"
	 */
	private string type;
	
	/**
	 * The depth position in the map in seconds
	 * The real position will be calculated later, in function of the hero speed
	 */
	private int positionInSeconds;
	
	/**
	 * The width position in the map 
	 */
	private float positionInX;
	
	/**
	 * Contructor
	 * @param type the type
	 * @param positionInSeconds the position in seconds
	 * @param positionInX the width position
	 */
	public Item (string type, int positionInSeconds, float positionInX){
		this.type = type;
		this.positionInSeconds = positionInSeconds;
		this.positionInX = positionInX;
	}
	
	public string Type {
		get {
			return this.type;
		}
		set {
			type = value;
		}
	}
	
	public int PositionInSeconds {
		get {
			return this.positionInSeconds;
		}
		set {
			positionInSeconds = value;
		}
	}
	
	public float PositionInX {
		get {
			return this.positionInX;
		}
		set {
			positionInX = value;
		}
	}

}
