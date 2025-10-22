using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount;
    public int rateDamage;
    public float attackRange;

    public GameObject attackPoint;
    public LayerMask playerLayers;


    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            Debug.Log("Zombie hit: " + hit.collider.name);
            
            if (hit.collider.CompareTag("Player"))
            {
              MainCharHealth mainCharHealth = hit.collider.GetComponent<MainCharHealth>();
                if (mainCharHealth != null)
                {
                    mainCharHealth.takeDamage(damageAmount);
                }
            }



        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * attackRange);
    }
}
