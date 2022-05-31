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

    public Camera camera;

    [SerializeField]
    private float maxHealth = 100f;
    
    [SyncVar]
    private float currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;
    private bool[] wasEnabledOnStart;

    [SerializeField]
    private Behaviour SpectatorMode;

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
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(true);
        }
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled=true;
        }
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
        }

    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                RPcTakeDamage(40f);
            }
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
        Debug.Log(transform.name + "a maintenant : " + currentHealth + "points de vies.");
    }

    private void Die()
    {
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.simulated = false;
            rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        Debug.Log(transform.name + "a été éliminé");
        //changement de caméra
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
        }
        
        
       

        //SpectatorMode.enabled = true;
    }
   
        


}
