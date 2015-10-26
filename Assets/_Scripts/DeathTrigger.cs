using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other) // detect collider of another object
    {
        if (other.gameObject.CompareTag("Player")) // if that object has th tag "Player" -
        {
            Application.LoadLevel(Application.loadedLevel); // reload the current scene
        }
    }
}
