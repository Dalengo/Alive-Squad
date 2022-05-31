using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private bool _isDead = false;

    public bool isDead
    {
        get => _isDead;
        protected set => _isDead = value;
    }

    [SerializeField]
    private float maxHealth = 100f;
    
    [SyncVar]
    private float currentHealth;

    [SyncVar]
    public string username = "Player";

    private void Awake()
    {
        SetDefaults();
    }

    private void SetDefaults()
    {
        currentHealth = maxHealth;
    }
    [ClientRpc]
    public void RPcTakeDamage(float amount)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
    }


}
