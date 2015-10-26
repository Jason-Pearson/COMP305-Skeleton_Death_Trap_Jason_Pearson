using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

    public Text finalScoreLabel;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Menu Button Event Handler
    public void OnStartButtonClick()
    {
        Application.LoadLevel("Menu");
    }
}
