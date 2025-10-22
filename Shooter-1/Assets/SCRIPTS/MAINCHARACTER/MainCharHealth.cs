using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharHealth : MonoBehaviour
{
    public int maxHealth = 100; 
    public int currentHealth;
   
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + damage + ". Current health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Player has died.");
        SceneManager.LoadScene("Menu");
    }
}   
