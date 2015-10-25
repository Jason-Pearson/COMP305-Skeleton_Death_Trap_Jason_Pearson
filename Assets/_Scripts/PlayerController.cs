using UnityEngine;
using System.Collections;

//VELOCITYRANGE PUBLIC CLASS - Max and Min Velocity for Player Shown in Inspector
[System.Serializable]
public class VelocityRange
{
    // PUBLIC INSTANCE VARIABLES
    public float vMin, vMax;

    // CONSTRUCTOR ++++++++++++++++++++++++++++++++
    public VelocityRange(float vMin, float vMax)
    {
        this.vMin = vMin;
        this.vMax = vMax;
    }
}
public class PlayerController : MonoBehaviour {

    //PUBLIC INSTANCE VARIABLES
	public float speed = 50f;
	public float jump = 500f;
	public VelocityRange velocityRange = new VelocityRange (300f, 1000f); // reference of type VelocityRange, holds min and max velocity value

	//PRIVATE INSTANCE VARIABLES
	private AudioSource[] _audioSources;
    private AudioSource _playerHit;
    //private PhysicsMaterial2D _material;
	private Rigidbody2D _rigidbody2D;
	private Transform _transform;
	private Animator _animator;

	private float _movingValue = 0;
	private bool _isFacingRight = true;
    private bool _isGrounded = true;

    // Use this for initialization
    void Start()
    {
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        //this._material = gameObject.GetComponent<BoxCollider2D>().sharedMaterial;

        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._playerHit = this._audioSources[0];

    }

    void FixedUpdate()
    {
        float forceX = 0f;
        float forceY = 0f;

        float absVelX = Mathf.Abs(this._rigidbody2D.velocity.x);
        float absVelY = Mathf.Abs(this._rigidbody2D.velocity.y);

        this._movingValue = Input.GetAxis("Horizontal"); // gives moving variable a value of -1 to 1

        if (this._movingValue != 0)
        { // player is moving
            //check if pxlayer is grounded
            if (this._isGrounded)
            {
                this._animator.SetInteger("AnimState", 1);
                if (this._movingValue > 0)
                {
                    // move right
                    if (absVelX < this.velocityRange.vMax)
                    {
                        forceX = this.speed;
                        this._isFacingRight = true;
                        this._flip();
                    }
                }
                else if (this._movingValue < 0)
                {
                    // move left
                    if (absVelX < this.velocityRange.vMax)
                    {
                        forceX = -this.speed;
                        this._isFacingRight = false;
                        this._flip();
                    }
                }
            }


        }
        else
        {
            // set our idle animation here
            this._animator.SetInteger("AnimState", 0);
        }

        // check if player is jumping
        if ((Input.GetKey("up") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            // check if player is grounded
            if (this._isGrounded)
            {
                this._animator.SetInteger("AnimState", 2);
                if (absVelY < this.velocityRange.vMax)
                {
                    forceY = this.jump;
                    this._isGrounded = false;
                }
            }
        }

        // add force to the player to push him
        this._rigidbody2D.AddForce(new Vector2(forceX, forceY));
    }

    //Collision Handling
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Coin"))
        {
            //Add Score +100
            Debug.Log("Coin");
            otherCollider.gameObject.GetComponents<AudioSource>();
            Destroy(otherCollider.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            this._playerHit.Play();
            /*if(this._isGrounded == false)
            {
                this._material.bounciness = 0.5f;
            }*/
            //Enact Physics Material to Bounce off Enemy?
            //Life Decrease
        }
    }

    void OnCollisionStay2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Platform"))
        {
            this._isGrounded = true;
            //this._material.bounciness = 0;
        }
    }

    // PRIVATE METHODS
    private void _flip()
    {
        if (this._isFacingRight)
        {
            this._transform.localScale = new Vector3(1f, 1f, 1f); // reset to normal scale
        }
        else
        {
            this._transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}
