using UnityEngine;

public class Pistol : Weapons
{
    public override void Shoot()
    {
        if (canShoot && actualAmmo > 0 && !isReloading)
        {
            actualAmmo--;
            Debug.Log("Pistol shot! Remaining ammo: " + actualAmmo);
            // Implement shooting logic here (e.g., raycasting, effects)

            RaycastHit hit;
            if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, shootRange))
            {
                Debug.Log("Hit: " + hit.collider.name);
                // Apply damage to the enemy
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.takeDamage(damage);
                }
            }
        }
        else if (actualAmmo <= 0)
        {
            Debug.Log("Out of ammo! Reload needed.");
            Reload();
        }
    }

    public override void Reload()
    {
        if (!isReloading && actualAmmo < maxAmmo && inventoryAmmo > 0)
        {
            isReloading = true;
            Debug.Log("Reloading...");
            // Implement reloading logic here (e.g., wait for reload time)
            inventoryAmmo -= (maxAmmo - actualAmmo);
            inventoryAmmo = Mathf.Clamp(inventoryAmmo, 0, inventoryAmmo);
            actualAmmo = maxAmmo;
            isReloading = false;
           
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.parent.position, transform.parent.forward * shootRange);
    }
}
