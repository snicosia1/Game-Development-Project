using UnityEngine;

public class CollectibleCoin : MonoBehaviour
{
    [SerializeField] private int coinID;
    [SerializeField] private AudioSource coinSource;
    [SerializeField] private ParticleSystem collectEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectCoin(coinID);

            coinSource.volume = 4.0f;
            coinSource.Play();
            //gameObject.SetActive(false);
            Destroy(gameObject, .4f);
        }
    }
}