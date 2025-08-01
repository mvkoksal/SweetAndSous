using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] fridges;
    public GameObject[] weapons;
    public float spawnRepeatRate = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnWeapon", 0f, spawnRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnWeapon()
    {
        int fridgeIndex = Random.Range(0, fridges.Length);
        Vector3 spawnPos = fridges[fridgeIndex].transform.position;
        int weaponIndex = Random.Range(0, weapons.Length);
        Instantiate(weapons[weaponIndex], spawnPos, weapons[weaponIndex].transform.rotation);
    }
}
