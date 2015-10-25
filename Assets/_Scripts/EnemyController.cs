using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    // PUBLIC INSTANCE VARIABLES
    public float speed = 100f;
    //public Transform sightStart;
    public Transform sightEnd;

    public GameObject shot;
    public Transform shotSpawn; //this variable is a refernece of the game object Shot Spawn but the variable type only references its transform component
    public float fireRate; // = 0.25 --> shoots 4 times a second --> 1/0.25
    private float nextFire; // = 0 

    // PRIVATE INSTANCE VARIABLES
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private Animator _animator;

    private bool _isGrounded = false;
    private bool _isGroundAhead = true;

    // Use this for initialization
    void Start()
    {
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Time.time > nextFire) //shoot every 0.5 sec by - game time > nextFire = 0
        {
            nextFire = Time.time + fireRate; // then update nextFire = gametime (now 0.2) + fireRate (0.25) --> then when game time is 0.27 > nextFire (0.26) and fire button is held = shoot Bolt prefab via the reference shot gameObject 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);//instantiate the game object shot per frame at a held key press, set at a vector3 position, at a set quaternion euler (rotation)
            shot.GetComponent<AudioSource>().playOnAwake = true;//upon instantiating the (shot), if the audio isn't playing on awake (on the very first frame), play this audio clip
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // check if enemy is grounded
        if (this._isGrounded)
        {
            this._animator.SetInteger("AnimState", 1); // play walking animation -
            this._rigidbody2D.velocity = new Vector2(this._transform.localScale.x, 0) * this.speed; // and have enemy's velocity go forward by speed

            this._isGroundAhead = Physics2D.Linecast(_transform.position, this.sightEnd.position, 1 << LayerMask.NameToLayer("Ground")); // linecast between enemy's transform and the ground's - returns a boolean value
            Debug.DrawLine(_transform.position, this.sightEnd.position);

            if (this._isGroundAhead == false) // when the line cast is past the end position of the ground layer == false
            {
                Debug.Log("Enemy Turn");
                this._flip(); // flip enemy local scale (invert sprite and gameobject's direction)
            }

        }
        else //if enemy is not grounded -
        {
            this._animator.SetInteger("AnimState", 0); // play idle animation
        }
    }

    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = false;
        }
    }
    // PRIVATE METHODS
    private void _flip()
    {
        if (this._transform.localScale.x == 1)
        {
            this._transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            this._transform.localScale = new Vector3(1f, 1f, 1f); // reset to normal scale
        }
    }
}
