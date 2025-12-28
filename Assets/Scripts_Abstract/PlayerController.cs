using System.Collections;
using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    // Static variables
    public static bool gameOver = false;

    // Public Booleans
    private float health = 100f;
    private bool isAlive = true;

    // Player Movement
    private float speed = 10.0f;
    private float maxSpeed = 8.0f;
    private float slowDown = 0.5f;
    private float slowDownFast = 5.0f;
    // Jump
    private float jumpForce = 13.0f;
    private bool isOnGround = true;
    // Input
    protected float horizontalInput;
    protected bool hasDownInput;
    protected bool jumping;
    protected bool facingRight;

    private float xBound = 15.0f;
    // Weapon Status
    public bool hasWeapon = false;
    private Weapon curWeapon;
    public float weaponDir;

    public Rigidbody playerRb;
    public Animator playerAnim;

    //private void FixedUpdate()
    // Transfer physics related stuff over later.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
    }

    public abstract void GetDownInput();

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            MovePlayer();
            ConstrainPlayerPosition();
            GetDownInput();

            //Shoot projectile if there is input
            if (hasDownInput && hasWeapon)
            {
                // ShootWeapon takes the player's position and direction as input
                curWeapon.ShootWeapon(transform.position, weaponDir);
                playerAnim.SetTrigger("Shoot_trig");
            }
        } else
        {
            //game over
            playerAnim.SetFloat("Speed_f", 0f);
        }


        if (health <= 0)
        {
            isAlive = false;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 2);
            gameOver = true;
            Debug.Log(gameObject.name + " died.");
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

    public abstract void GetMovementInput();
    public abstract void GetJumpInput();

    public void MovePlayer()
    {
        //Updates horizontalInput
        GetMovementInput();
        // Get the current direction of the player

        // Don't modify weaponDir if horizontalInput = 0.
        if (horizontalInput == 1 || horizontalInput == -1)
        {
            weaponDir = horizontalInput;
        }

        // Turn body towards left or right
        if (horizontalInput > 0 && !facingRight)
        {
            transform.rotation = Quaternion.Euler(0f, 120f, 0f);
            facingRight = true;
        }
        else if (horizontalInput < 0 && facingRight)
        {
            transform.rotation = Quaternion.Euler(0f, 240f, 0f);
            facingRight = false;
        }

        // Walking
        if (horizontalInput != 0)
        {
            playerAnim.SetFloat("Speed_f", 0.60f);
        }
        else
        {
            playerAnim.SetFloat("Speed_f", 0f);
        }
           
        playerRb.AddForce(horizontalInput * speed * Vector3.right);

        GetJumpInput();
        // Jump when up arrow is pressed, prevent double-jumping
        if (jumping && isOnGround)
        {
            playerAnim.SetTrigger("Jump_trig");
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
        Vector3 pos = playerRb.position;

        if (pos.x > xBound)
        {
            pos.x = xBound;
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }
        else if (pos.x < -xBound)
        {
            pos.x = -xBound;
            playerRb.linearVelocity = new Vector3(0, playerRb.linearVelocity.y, playerRb.linearVelocity.z);
        }

        playerRb.position = pos;
    }
}
