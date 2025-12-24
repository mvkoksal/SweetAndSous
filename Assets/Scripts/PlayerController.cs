using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Booleans
    public float health = 100f;
    public bool isAlive = true;

    // Player Movement
    private float speed = 10.0f;
    private float maxSpeed = 8.0f;
    private float slowDown = 0.5f;
    private float slowDownFast = 5.0f;
    // Jump
    private float jumpForce = 13.0f;
    private bool isOnGround = true;
    private float gravityModifier = 1.7f;

    private float xBound = 17.0f;

    public bool hasWeapon = false;
    private Weapon curWeapon;
    private Rigidbody playerRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    //private void FixedUpdate()
    // Transfer physics related stuff over later.

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstrainPlayerPosition();

        //Shoot projectile if there is input
        if (Input.GetKeyDown(KeyCode.DownArrow) && hasWeapon)
        {
            // ShootWeapon takes the player's position
            curWeapon.ShootWeapon(transform.position);
        }

        if(health <= 0)
        {
            isAlive = false;
            Debug.Log(gameObject.tag + " died.");
        }
    }

    IEnumerator WeaponCountdownRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        hasWeapon = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Set the isOnGround variable to prevent double jumping
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        // Hit by a projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            health -= collision.gameObject.GetComponent<Projectile>().damage;
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        hasWeapon = true;
        curWeapon = weapon;
        StartCoroutine(WeaponCountdownRoutine());
    }
            
    private void MovePlayer()
    {
        // Get horizontal input and add horizontal movement
        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalInput = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalInput = 1f;
        }

        playerRb.AddForce(horizontalInput * speed * Vector3.right);

        bool jumping = Input.GetKey(KeyCode.UpArrow);

        // Jump when up arrow is pressed, prevent double-jumping
        if (jumping && isOnGround)
        {
            playerRb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            isOnGround = false;
        }

        // Slow down the ball when no horizontal or vertical force is applied. (AND don't slow down when going down from a jump).
        if (horizontalInput == 0.0f && !jumping && isOnGround)
        {
            playerRb.linearDamping = slowDownFast;
        }
        else
        {
            playerRb.linearDamping = slowDown;
        }

        // Set a max possible speed
        if (playerRb.linearVelocity.x > maxSpeed)
        {
            playerRb.linearVelocity = new Vector3(maxSpeed, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }
        if (playerRb.linearVelocity.x < -maxSpeed)
        {
            playerRb.linearVelocity = new Vector3(-maxSpeed, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }
    }
    
    private void ConstrainPlayerPosition()
    {
        //// Stop movement on the z axis
        //Vector3 vel = playerRb.linearVelocity;
        //vel.z = 0.0f;
        //playerRb.linearVelocity = vel;

        //Vector3 pos = playerRb.position;
        //pos.z = -0.6f;
        //playerRb.position = pos;

        // Set boundaries on the x axis
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
    }
}

