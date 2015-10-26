using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    public Text scoreLabel; // reference for score label - to display score in the game scene
    public int score = 0; // holds the score (starting at 0) to be added upon and updated to the score label

    public int hits = 20; // holds the health for the Player - starting at 100
    public Text hitCounter; // visual counter for the Player health via this label

	//PRIVATE INSTANCE VARIABLES
	private AudioSource[] _audioSources;
    private AudioSource _playerHit;
    private AudioSource _playerJump;
    private AudioSource _coinPickup;
    private AudioSource _playerFall;

    //private PhysicsMaterial2D _material;
	private Rigidbody2D _rigidbody2D;
	private Transform _transform;
	private Animator _animator;

	private float _movingValue = 0;
	private bool _isFacingRight = true;
    private bool _isGrounded = false;

    // Use this for initialization
    void Start()
    {
        this._rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        //this._material = gameObject.GetComponent<BoxCollider2D>().sharedMaterial;

        this._SetScore();
        this._HitCount();
        this._audioSources = gameObject.GetComponents<AudioSource>();
        this._playerHit = this._audioSources[0];
        this._playerJump = this._audioSources[1];
        this._coinPickup = this._audioSources[2];
        this._playerFall = this._audioSources[3];
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
            if (this._isGrounded)
            {
                // set our idle animation here
                this._animator.SetInteger("AnimState", 0);
            }
        }

        // check if player is jumping
        if ((Input.GetKey("up") || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
        {
            // check if player is grounded
            if (this._isGrounded)
            {
                this._animator.SetInteger("AnimState", 2);
                this._animator.speed = 0.8f;
                if (absVelY < this.velocityRange.vMax)
                {
                    this._playerJump.Play();
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
            this.score += 1000;
            _SetScore();
            Debug.Log("Coin");
            this._coinPickup.Play();
            Destroy(otherCollider.gameObject);
        }
        if (otherCollider.gameObject.CompareTag("Bullet"))
        {
            this.hits--;
            _HitCount(); 
            Debug.Log("Hit");
            this._playerHit.Play();
            Destroy(otherCollider.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Enemy"))
        {
            this._playerHit.Play();
            //Enact Physics Material to Bounce off Enemy?
            /*if(this._isGrounded == false)
            {
                this._material.bounciness = 0.5f;
            }*/
            //Amount of Hits Decreased
            this.hits--;
            _HitCount();
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
    public void _SetScore() // method to update to the current score upon killing enemies or picking up Gold and Silver coins
    {
        this.scoreLabel.text = "Score: " + score; // label equals this string statement - score is concatenated to string for display
    }
    public void _HitCount() // method to update to the current score upon killing enemies or picking up Gold and Silver coins
    {
        this.hitCounter.text = "Hit: " + hits; // label equals this string statement - score is concatenated to string for display
    }
    //End Game Method - called in Game Controller script - after player health = 0
    /*public void _EndGameHealthState()
    {
        Destroy(gameObject); // destroy player gameobject

        //Disable these Labels that are for Gameplay
        this.scoreLabel.enabled = false;
        this.enemyKilled.enabled = false;
        this.healthCounter.enabled = false;
        //Enable these labels for the Game Over screen
        this.finalScoreLabel.enabled = true;
        this.gameOverLabel.enabled = true;
        this.finalKillCount.enabled = true;
        this.restartLabel.enabled = true;
        this.finalScoreLabel.text = "Score: " + this.score; // set final score to this label
        this.finalKillCount.text = "Kills: " + this.killCount; // set final kill count to this label
        this.restartLabel.text = "Press 'R' to Restart"; // set restart label to this string statement, to notify play how to restart

    }*/

}
