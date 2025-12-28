using UnityEngine;
using UnityEngine.Rendering;

public class ProjCookie : Projectile
{
    private Rigidbody projectileRb;
    public float speed = 10.0f;
    private float xRange = 26;
    public float projDir = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        damage = 10;
        projectileRb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        // Move forward
        projectileRb.linearVelocity = (new Vector3(projDir, 0, 0)) * speed;

        // Destroy gameObject if it goes out of bounds
        if ((transform.position.x > xRange) || (transform.position.x < -xRange))
        {
            Destroy(gameObject);
        }
    }
}
