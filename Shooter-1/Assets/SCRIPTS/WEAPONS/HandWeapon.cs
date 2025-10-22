using UnityEngine;

public class HandWeapon : MonoBehaviour
{
  public Weapons currentWeapon;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            currentWeapon.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
        }

    }

}
