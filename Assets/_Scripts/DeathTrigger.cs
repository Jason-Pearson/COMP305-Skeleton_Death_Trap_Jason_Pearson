using UnityEngine;
using System.Collections;

public class DeathTrigger : MonoBehaviour {
   
    public PlayerController playerControllerScript; // reference for the Player Controller script of type PlayerController class
    public int finalScore;

    private AudioSource[] audiosources;
    private AudioSource _playerFall;
    private AudioSource _background;
	// Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
	void Start () {
        this.audiosources = gameObject.GetComponents<AudioSource>();
        this._playerFall = this.audiosources[0];
        this._background = this.audiosources[1];

        GameObject playerObject = GameObject.FindWithTag("Player"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (playerObject != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            playerControllerScript = playerObject.GetComponent<PlayerController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (playerObject == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find PlayerController script for Health End State in GameController");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (playerControllerScript != null)
        {
	    if (playerControllerScript.hits == 0)
        {
            this.finalScore = playerControllerScript.score;
            this._background.Stop();
            this._playerFall.Play();
            Application.LoadLevel("GameOver");
        }
        }
	}
    void OnTriggerEnter2D(Collider2D other) // detect collider of another object
    {
        if (other.gameObject.CompareTag("Player")) // if that object has th tag "Player" -
        {
            this.finalScore = playerControllerScript.score;
            this._background.Stop();
            this._playerFall.Play();
            Application.LoadLevel("GameOver");
        }
    }
}
