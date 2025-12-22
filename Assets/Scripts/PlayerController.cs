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

    private bool hasWeapon = false;
    public GameObject projectile;

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
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Instantiate(projectile, new Vector3(transform.position.x+1.6f, transform.position.y+1f, transform.position.z), Quaternion.Euler(-35f, transform.eulerAngles.y, transform.eulerAngles.z));
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

        // Pick up a weapon and set the hasWeapon variable.
        if(collision.gameObject.CompareTag("Weapon"))
        {
            Destroy(collision.gameObject);
            hasWeapon = true;
            StartCoroutine(WeaponCountdownRoutine());
        }

        // Hit by a projectile
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            health -= collision.gameObject.damage;
        }
    }

    private void MovePlayer()
    {
        // Get horizontal input and add horizontal movement
        float horizontalInput = Input.GetAxis("Horizontal");
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

