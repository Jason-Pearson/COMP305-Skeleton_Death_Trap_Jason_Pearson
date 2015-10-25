using UnityEngine;
using System.Collections;

public class ShotMover : MonoBehaviour {

	//PUBLIC INSTANCE VARIABLES
    public float speed; // set the speed per unit to move the gameobject

    public SpriteRenderer shot; //reference for the 2d spriterenderer of the sprite image in the 2D sprite object you are using to represent a shot
    public GameObject bulletBill; //(1) gameobject reference for the player blast and enemy laser - in order to -

	// Use this for initialization
    void Start()
    {
        Destroy(bulletBill, 3);
    }
    void Update()
    {
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position; // every frame, make a Vector2 variable holding the current position of the gameobject attached to this script (blast or laser)
        currentPosition.y += this.speed; // increment the new position by the speed
        gameObject.GetComponent<Transform>().position = currentPosition; // then assign the new position to the gameobject's transform
    }
    
}
