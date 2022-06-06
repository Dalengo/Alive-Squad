using UnityEngine;
using Mirror;
using System.Collections;
using TMPro;

public class Player : NetworkBehaviour
{
    [SyncVar]
    public bool isDead = false;

    

    public Camera camera;
    [SyncVar]
    public bool isRespawned = false;

    public TextMeshProUGUI interactUI;
    

    
    public bool canRespawn = false;
    public bool isInvincible = false;
    private float InvincibilityFlashDelay = 0.15f;
    public float InvincibilityTimeAfterHit = 3f;
    public SpriteRenderer graphics;
    public HealthBar healthBarMulti;
    public HealthBar healthBarsolo;
    public AudioClip[] playlist;
    public AudioSource audioSource;

    [SerializeField]
    private float maxHealth = 100f;

    [SyncVar]
    private float currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    [SerializeField]
    private GameObject[] disableGameObjectsOnDeath;
    [SerializeField]
    private GameObject Name;
    private bool[] wasEnabledOnStart;

    [SerializeField]
    private Behaviour SpectatorMode;

    [SyncVar]
    public string username = "Player";


    
    public void Setup()
    {

        wasEnabledOnStart = new bool[disableOnDeath.Length];
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            wasEnabledOnStart[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();

    }

    

    [Command]
    public void PutisRespawned()
    {
        RpcRespawn();
    }



    [ClientRpc]
    private void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            SpectatorMode.enabled = false;


            foreach (Behaviour behaviour in disableOnDeath)
            {
                behaviour.enabled = true;
            }

            foreach (GameObject gameObject in disableGameObjectsOnDeath)
            {
                gameObject.SetActive(true);
            }
        }
        isDead = false;
        Debug.Log("Activation health");
        currentHealth = maxHealth;
        Debug.Log(currentHealth);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        healthBarMulti.SetHealth((int)currentHealth); //actutalisation de la barre de vie
        healthBarsolo.SetHealth((int)currentHealth);
        Collider2D collider = GetComponent<Collider2D>();
        Name.SetActive(true);

        if (collider != null)
        {
            collider.enabled = true;
        }
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            rigidbody.simulated = true;
            rigidbody.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        }
        
        
    }


    private void SetDefaults()
    {

        isDead = false;
        currentHealth = maxHealth;
        for (int i = 0; i < disableOnDeath.Length; i++)
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
            collider.enabled = true;
        }
        if (isLocalPlayer)
        {
            //GameManager.instance.SetSceneCameraActive(false);
        }

    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                if (isDead == true)
                {

                }
                
                RPcTakeDamage(40f);
            }
            if (canRespawn == true)
            {
                GameManager.PlayerActivated(this);
                SetCanRespawn(false);
                PutisRespawned();
            }
        }

    }

    [ClientRpc]
    public void RPcTakeDamage(float amount)
    {
        if (!isDead && !isInvincible)
        {
            audioSource.clip = playlist[0];
            audioSource.Play();
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                Die();
            }
            healthBarMulti.SetHealth((int)currentHealth);
            healthBarsolo.SetHealth((int)currentHealth);
            isInvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(HandleInvincibleDelay());
        }
        Debug.Log(transform.name + "a maintenant : " + currentHealth + "points de vies.");
    }

    private IEnumerator InvincibilityFlash()
    {
        while (isInvincible)
        {
            graphics.color = new Color(1f, 1f, 1f, 0f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
            graphics.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(InvincibilityFlashDelay);
        }
    }
    private IEnumerator HandleInvincibleDelay()
    {
        yield return new WaitForSeconds(InvincibilityTimeAfterHit);
        isInvincible = false;
    }

    private void Die()
    {
        isDead = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        
        //d�sactivation des Behaviour y compris la cam�ra du joueur
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }
        
        for (int i = 0; i < disableGameObjectsOnDeath.Length; i++)
        {
            disableGameObjectsOnDeath[i].SetActive(false);
        }
        Name.SetActive(false);
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
        Debug.Log(transform.name + "a �t� �limin�");
        //changement de cam�ra
        
        GameManager.PlayerDesactivated(this);
        if (GameManager.AllPlayersAlive.Count > 0)
        {
            Debug.Log("Spectator");
            SpectatorMode.enabled = true;
            
        }


    }
    // invoked by clients but executed on the server only
    [Command(requiresAuthority = false)]
    void CmdProvideCanRespawnStateToServer(bool state, Vector3 position)
    {
        // make the change local on the server
        canRespawn = state;
        transform.position = position;
        // forward the change also to all clients
        RpcSendCanRespawnState(state,position);
    }

    // invoked by the server only but executed on ALL clients
    [ClientRpc]
    void RpcSendCanRespawnState(bool state, Vector3 position)
    {
        // skip this function on the LocalPlayer 
        // because he is the one who originally invoked this


        //make the change local on all clients
        canRespawn = state;
        transform.position = position;
    }

    // Client makes sure this function is only executed on clients
    // If called on the server it will throw an warning
    // https://docs.unity3d.com/ScriptReference/Networking.ClientAttribute.html
    
    public void SetCanRespawn(bool state,Vector3 position)
    {
        //Only go on for the LocalPlayer
        

        // make the change local on this client
        canRespawn = state;
        transform.position = position;
        
        // invoke the change on the Server as you already named the function
        CmdProvideCanRespawnStateToServer(canRespawn,position);
    }

    public void SetCanRespawn(bool state)
    {
        //Only go on for the LocalPlayer


        // make the change local on this client
        canRespawn = state;
        

        // invoke the change on the Server as you already named the function
        CmdProvideCanRespawnStateToServer(canRespawn);
    }

    [Command(requiresAuthority = false)]
    void CmdProvideCanRespawnStateToServer(bool state)
    {
        // make the change local on the server
        canRespawn = state;
        
        // forward the change also to all clients
        RpcSendCanRespawnState(state);
    }

    [ClientRpc]
    void RpcSendCanRespawnState(bool state)
    {
        // skip this function on the LocalPlayer 
        // because he is the one who originally invoked this


        //make the change local on all clients
        canRespawn = state;
    }

    public static Player localPlayer;
    public void HostGame()
    {

    }

    
    
}