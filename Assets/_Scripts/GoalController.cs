using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

    public PlayerController playerControllerScript; // reference for the Player Controller script of type PlayerController class
    public int finalScore;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
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
    void Update()
    {
        this.finalScore = playerControllerScript.score;

    }
}
