using UnityEngine;
using System.Collections;

public class SpawnCoins : MonoBehaviour {

    public Transform[] coinSpawns; // an array transform components of the coinSpawn empty gameobjects
    public GameObject coin; // reference to hold coin gameObject

    // Use this for initialization
    void Start()
    {
        Spawn(); // at first frame, call Spawn method
    }

    void Spawn() // method that spawns coins on a loop the size of the transform array
    {
        for (int i = 0; i < coinSpawns.Length; i++)
        {
            int coinFlip = Random.Range(0, 2); // random number between 0 and 1 (2)
            if (coinFlip > 0) // if coinFlip equal 1 - 50/50 chance of spawning
            {
                Instantiate(coin, coinSpawns[i].position, Quaternion.identity); // instantiate coin gameObject, at the coinSpawn tranform of the current loop index
            }
        }
    }
}
