using UnityEngine;

public class DeadlyPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().DrainHealth();
            collision.GetComponent<PlayerController>().Die();
            }
        }
    
}