using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float damage;
    public float fireRate;
    public GameObject projectile;
    private GameObject currentFridge;

    public SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void ShootWeapon(Vector3 playerPos, float weaponDir);

    public void SetSpawnManager(SpawnManager spawnManager)
    {
        this.spawnManager = spawnManager;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fridge"))
        {
            // Store a reference to the fridge the weapon is in
            currentFridge = other.gameObject;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Add the fridge to the list of empty fridges, since it just got emptied
            spawnManager.fridges.Add(currentFridge);

            // Update state in player
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.EquipWeapon(this);
            player.playerAnim.SetInteger("WeaponType_int", 1);

            // The weapon is picked up.
            Destroy(gameObject);
        }
    }
}
