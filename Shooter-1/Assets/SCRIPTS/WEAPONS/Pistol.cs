using System.Collections;
using UnityEngine;

public class Pistol : Weapons
{
    public ParticleSystem sparks;
    public Animator pistolAnimator;
    public MainCharacter movement;
    public void Start()
    {
        movement = FindAnyObjectByType<MainCharacter>();
    }

    public void AnimationMovement()
    {
        pistolAnimator.SetBool("walking", movement.isWalking);
        pistolAnimator.SetBool("running", movement.isRunning);
        pistolAnimator.SetBool("reload", isReloading);
    }

    public void Update()
    {
        AnimationMovement();
    }
    public override void Shoot()
    {
        if (canShoot && actualAmmo > 0 && !isReloading)
        {
            sparks.Play();
            pistolAnimator.SetTrigger("shoot");
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
            StartCoroutine(Reloading());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.parent.position, transform.parent.forward * shootRange);
    }

    public IEnumerator Reloading()
    {
        if (inventoryAmmo>0 && actualAmmo < maxAmmo )
        {
            canShoot = false;
            isReloading = true;
            Debug.Log("Reloading...");
            yield return new WaitForSeconds(reloadTime);
            int ammoNeeded = maxAmmo - actualAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, inventoryAmmo);
            actualAmmo += ammoToReload;
            inventoryAmmo -= ammoToReload;
            isReloading = false;
            canShoot = true;
            Debug.Log("Reloaded. Current ammo: " + actualAmmo);
        }
        else
        {
            Debug.Log("No ammo in inventory to reload.");
        }
       
    }
}
