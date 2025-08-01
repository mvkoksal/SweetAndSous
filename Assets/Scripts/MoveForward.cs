using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody projectileRb;
    public float speed = 10.0f;
    private float xRange = 26;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        projectileRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        projectileRb.linearVelocity = Vector3.right * speed;

        // Destroy gameObject if it goes out of bounds
        if ((transform.position.x > xRange) || (transform.position.x < -xRange))
        {
            Destroy(gameObject);
        }
    }
}
