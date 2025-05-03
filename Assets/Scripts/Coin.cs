using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [SerializeField] private int coinID;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private ParticleSystem collectEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectCoin(coinID);
            
            if (collectSound != null)
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            
            if (collectEffect != null)
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            
            gameObject.SetActive(false);
        }
    }
}