using UnityEngine;

public class WeaponCookie : Weapon
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void ShootWeapon(Vector3 playerPos, float weaponDir)
    {
        //Instantiate projectile
        GameObject proj = Instantiate(projectile, new Vector3(playerPos.x + (weaponDir * 1.6f), playerPos.y + 1f, playerPos.z), Quaternion.Euler(-35f, 0f, 0f));
        proj.GetComponent<ProjCookie>().projDir = weaponDir;
    }
}
