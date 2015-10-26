using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YouWinController : MonoBehaviour
{

    public Text finalScoreLabel;
    public GoalController goalControllerScript;
    public GameObject deathTrigger;

    // Use this for initialization
    void Awake()
    {
        deathTrigger = GameObject.FindWithTag("DeathTrigger");
        GameObject goalController = GameObject.FindWithTag("GoalController"); //create reference for Player gameobject, and assign the variable via FindWithTag at start
        if (goalController != null) // if the playerObject gameObject-reference is not null - assigning the reference via FindWithTag at first frame -
        {
            goalControllerScript = goalController.GetComponent<GoalController>();// - set the PlayerController-reference (called playerControllerScript) to the <script component> of the Player gameobject (via the gameObject-reference) to have access the instance of the PlayerController script
        }
        if (goalController == null) //for exception handling - to have the console debug the absense of a player controller script in order for this entire code, the code in the GameController to work
        {
            Debug.Log("Cannot find Goal Controller script for Health End State in GameController");
        }
    }

    void Start()
    {

        this.finalScoreLabel.text = "Score: " + goalControllerScript.finalScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Menu Button Event Handler
    public void OnStartButtonClick()
    {
        Destroy(goalControllerScript.gameObject);
        Destroy(deathTrigger);
        Application.LoadLevel("Menu");
    }
}