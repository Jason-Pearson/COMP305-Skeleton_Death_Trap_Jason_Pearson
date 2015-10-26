using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YouWinController : MonoBehaviour
{

    public Text finalScoreLabel;
    public PlayerController playerControllerScript;
    public GameObject player; //create reference for Player gameobject, and assign the variable via FindWithTag at start

    // Use this for initialization
    void Awake()
    {
        if (player != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            playerControllerScript = player.GetComponent<PlayerController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (player == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find DeathTrigger script for final score referencing to GameOver - finalScore Label");
        }
    }

    void Start()
    {
        this.finalScoreLabel.text = "Score: " + playerControllerScript.score;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Menu Button Event Handler
    public void OnStartButtonClick()
    {
        Destroy(playerControllerScript.gameObject);
        Application.LoadLevel("Menu");
    }
}