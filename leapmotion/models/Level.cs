using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * @author Adrien D
 * @version 1.0
 */

/**
 * A model class containing all level information
 */
public class Level {

	private string name;

	private string musicPath;

	private string map;

	private List<Item> itemList;

	private bool tutorial;

	/**
	 * Constructor
	 * Builds a level from a level json path
	 */
	public Level (string name, string musicPath, string map, List<Item> itemList, bool tutorial){
		this.name = name;
		this.musicPath = musicPath;
		this.map = map;
		this.itemList = itemList;
		this.tutorial = tutorial;
	}

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}

	public string MusicPath {
		get {
			return musicPath;
		}
		set {
			musicPath = value;
		}
	}

	public string Map {
		get {
			return map;
		}
		set {
			map = value;
		}
	}

	public List<Item> ItemList {
		get {
			return itemList;
		}
		set {
			itemList = value;
		}
	}

	public bool Tutorial {
		get {
			return tutorial;
		}
		set {
			tutorial = value;
		}
	}


}
