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

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabledOnStart;

    [SyncVar]
    public string username = "Player";

    public void Setup()
    {
        wasEnabledOnStart = new bool[disableOnDeath.Length];
        for(int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnabledOnStart[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }

    private void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth;
        for(int i=0; i<disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabledOnStart[i];
        }
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled=true;
        }
    }

    [ClientRpc]
    public void RPcTakeDamage(float amount)
    {
        if (!isDead)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }


}
