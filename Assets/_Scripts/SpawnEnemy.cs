using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

	public Transform[] enemySpawns; // an array transform components of the enemySpawn empty gameobjects
    public GameObject enemy; // reference to hold enemy gameObject

    // Use this for initialization
    void Start()
    {
        Spawn(); // at first frame, call Spawn method
    }

    void Spawn() // method that spawns coins on a loop the size of the transform array
    {
            int index = Random.Range(0, 4);        
            Instantiate(enemy, enemySpawns[index].position, Quaternion.identity); // instantiate coin gameObject, at the coinSpawn tranform of the current loop index
    }
}
