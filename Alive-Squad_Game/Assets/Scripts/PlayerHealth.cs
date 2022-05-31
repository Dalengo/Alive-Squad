using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBarJoueur;
    public HealthBar healthBarMulti;

    void Start()
    {
        currentHealth = maxHealth;
        healthBarJoueur.SetMaxHealth(maxHealth);
        healthBarMulti.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBarJoueur.SetHealth(currentHealth);
        healthBarMulti.SetHealth(currentHealth);
    }
}
