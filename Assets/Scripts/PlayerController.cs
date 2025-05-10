using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 5f; 
    [SerializeField] public float jumpForce = 10f; 

    [Header("Ground Check")]

    private Rigidbody2D rb;
    private bool isGrounded;
    public Animator animator;
     private int jumpsRemaining;
    [SerializeField] public int maxJumps = 2;
    [Header("Health System")]
     [SerializeField] private float invincibilityDuration = 1f;
    private bool isInvincible = false;
    [SerializeField] private Image[] healthHearts; 
    [SerializeField] private Sprite fullHeart, emptyHeart;
    [SerializeField] private AudioSource playerSource;
    [SerializeField] private AudioClip deathSound;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;
    [SerializeField] private float delaySeconds = 3f;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        
        
        
        float moveInput = Input.GetAxis("Horizontal"); 
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput * moveSpeed));

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(20, 20, 20); 
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-20, 20, 20); 
        }

        // Handle jumping
       if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                jumpsRemaining = maxJumps - 1; 
            }
            else if (jumpsRemaining > 0)
            {
                Jump();
                jumpsRemaining--;
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps; 
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void TakeDamage()
    {
        if (isInvincible) return;
        
        currentHealth--;
        UpdateHealthUI();
        
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityFrame());
        }
    }
    
    public void Die()
    {
        Debug.Log("Player has died!");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetTrigger("IsDead");
        playerSource.volume = 4.0f;
        playerSource.Play();
        Destroy(gameObject, 1f);
        
        
        
        
    }

    IEnumerator InvincibilityFrame()
    {
        isInvincible = true;
        
        
        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            elapsed += Time.deltaTime * 5f; 
            yield return null;
        }
        GetComponent<SpriteRenderer>().enabled = true;
        
        isInvincible = false;
    }
    public void OnHit()
{
    TakeDamage();
}
void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        Debug.Log("RAAAAAAAA");
        OnHit();
    }
}
void UpdateHealthUI()
    {
        for (int i = 0; i < healthHearts.Length; i++)
        {
            healthHearts[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
            healthHearts[i].enabled = (i < maxHealth);
        }
    }

   public void DrainHealth()
{
    healthHearts[0].sprite = emptyHeart;
    healthHearts[1].sprite = emptyHeart;
    healthHearts[2].sprite = emptyHeart;
}
IEnumerator Delay()
    {
        Debug.Log($"Waiting {delaySeconds} seconds...");
        yield return new WaitForSeconds(delaySeconds);
        
        SceneManager.LoadScene("MainMenu");
    }
}


    
