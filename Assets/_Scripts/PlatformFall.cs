using UnityEngine;
using System.Collections;

public class PlatformFall : MonoBehaviour {

    public float fallDelay = 5f; // delay of one second before calling Fall method


    private Rigidbody2D rb2d; // reference to hold rigidbody of object attached to script

    //Initialize components for the script as it loads
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>(); // get rigidbody component
    }

    void OnCollisionEnter2D(Collision2D other) // check on collision enter by other object (player)
    {
        if (other.gameObject.CompareTag("Player")) // if the other object has the tag "Player"
        {
            Invoke("Fall", fallDelay); // calls the method "Fall" after a delay of one second
        }
    }

    void Fall()
    {
        rb2d.freezeRotation = false;
        rb2d.isKinematic = false; // false = enable gravity on rigidbody
    }
}
