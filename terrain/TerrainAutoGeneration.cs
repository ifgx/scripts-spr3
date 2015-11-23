using UnityEngine;
using System.Collections;

/**
 * Autogeneration the terrain while the hero runs on it
 */
public class TerrainAutoGeneration : MonoBehaviour {


	private bool terrainCreated = false;
	private float middleTerrainZ;
	private float terrainLength;
	private float terrainPosZ;

	// Use this for initialization
	/**
	 * Initialization
	 */
	void Start () {
		Terrain terrain = this.gameObject.GetComponent<Terrain>();
		terrainPosZ = terrain.transform.position.z;
		terrainLength = terrain.terrainData.size [2];
		middleTerrainZ = terrainPosZ + terrainLength/2.0f;
		//Debug.Log (middleTerrainZ);
	}
	
	
	/**
	 * Update is called once per frame
	 */
	void Update () {
		Hero hero = GameModel.HerosInGame [0];

		if (!terrainCreated) {

			if (hero.GetPosition().z > middleTerrainZ) {

				//Debug.Log ("#### MIDDLE #### " + middleTerrainZ);
				Instantiate(Resources.Load("prefabs/Terrain") as GameObject, new Vector3 (-100, -2, terrainPosZ + terrainLength), Quaternion.identity);
				terrainCreated = true;
			}
		}else if (hero.GetPosition().z > terrainPosZ + terrainLength){
			Destroy(this.gameObject);
		}
	}
}
