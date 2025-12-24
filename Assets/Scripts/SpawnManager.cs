using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> fridges;
    public List<GameObject> weapons;
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
        if (fridges.Count != 0)
        {
            int fridgeIndex = Random.Range(0, fridges.Count);
            Vector3 fridgePos = fridges[fridgeIndex].transform.position;
            Vector3 spawnPos = new Vector3(fridgePos.x, fridgePos.y + 1f, fridgePos.z - 0.5f);

            fridges.RemoveAt(fridgeIndex);

            int weaponIndex = Random.Range(0, weapons.Count);
            GameObject newWeapon = Instantiate(weapons[weaponIndex], spawnPos, weapons[weaponIndex].transform.rotation);
            // give the instantiated weapon a reference to this spawnManager
            newWeapon.GetComponent<Weapon>().SetSpawnManager(this);
            
        }
    }
}
