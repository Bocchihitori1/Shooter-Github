using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
   public int actualAmmo;
    public int maxAmmo;
    public float shootRate;
    public float reloadTime;
    public bool isReloading = false;
    public float shootRange;
    public int damage;
    public bool canShoot;

    public int inventoryAmmo;
    public abstract void Shoot();
    public abstract void Reload();

}
