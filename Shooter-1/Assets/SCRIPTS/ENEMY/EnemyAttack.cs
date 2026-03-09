using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount;
    public int rateDamage;
    public float attackRange;
    private bool canAttack = true;

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
                if (mainCharHealth != null && canAttack ==true)
                {
                    mainCharHealth.takeDamage(damageAmount);
                    StartCoroutine(ResetAttack());
                }
            }



        }

    }
    private IEnumerator ResetAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(rateDamage);
        canAttack = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * attackRange);
    }
}
