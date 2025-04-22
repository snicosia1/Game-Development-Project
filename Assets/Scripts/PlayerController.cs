using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 5f; // Speed of horizontal movement
    [SerializeField] public float jumpForce = 10f; // Force applied when jumping

    [Header("Ground Check")]

    private Rigidbody2D rb;
    private bool isGrounded;
    public Animator animator;
     private int jumpsRemaining;
    [SerializeField] public int maxJumps = 2;
    [Header("Health System")]
     [SerializeField] private float invincibilityDuration = 1f;
    private bool isInvincible = false;
    [SerializeField] private Image[] healthHearts; // Assign in inspector
    [SerializeField] private Sprite fullHeart, emptyHeart;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Check if the player is grounded
        
        // Handle movement
        float moveInput = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow keys
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveInput * moveSpeed));

        if (moveInput > 0)
        {
            transform.localScale = new Vector3(20, 20, 20); // Facing right
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-20, 20, 20); // Facing left
        }

        // Handle jumping
       if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                Jump();
                jumpsRemaining = maxJumps - 1; // Subtract one for the initial jump
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
        // Check if player is touching ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps; // Reset jumps when grounded
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
    
    void Die()
    {
        Debug.Log("Player has died!");
        // Add death animation, game over screen, etc.
        Destroy(gameObject);
    }

    IEnumerator InvincibilityFrame()
    {
        isInvincible = true;
        
        // Optional: Visual feedback (blinking)
        float elapsed = 0f;
        while (elapsed < invincibilityDuration)
        {
            // Toggle sprite renderer visibility for blink effect
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            elapsed += Time.deltaTime * 5f; // Adjust blink speed
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
}


    
