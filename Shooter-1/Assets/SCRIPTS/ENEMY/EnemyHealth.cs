using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  
    public int maxHealth = 100;
    public int currentHealth;

    
    void Start()
    {
        currentHealth = maxHealth;
        
    }

   public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }   

    void Die()
    {
        Debug.Log("Enemy died!");
        // Aquí puedes agregar lógica adicional, como reproducir una animación de muerte, soltar loot, etc.
        Destroy(gameObject);
    }
    void Update()
    {
        
    }
}
