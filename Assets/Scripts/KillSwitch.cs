using UnityEngine;

public class KillSwitch : MonoBehaviour
{
    public string playerTag = "Player";
    public ProjectileEnemy enemy; 
    [SerializeField] private AudioSource deathSource;

  
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            enemy.DefeatEnemy();
            deathSource.volume = 4.0f;
            deathSource.Play();
            Destroy(gameObject, 1f);
        }
    }
}