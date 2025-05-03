using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private GameObject levelExitBlock;
    [SerializeField] private GameObject allCoinsMessage;
    [SerializeField] private int totalCoinsInLevel;
    
    private int coinsCollected;
    private bool allCoinsCollected = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitializeLevel();
    }

    private void InitializeLevel()
    {
        coinsCollected = 0;
        allCoinsCollected = false;
        UpdateUI();
        
        if (totalCoinsInLevel == 0)
            totalCoinsInLevel = GameObject.FindGameObjectsWithTag("Coin").Length;
            
        if (levelExitBlock != null)
            levelExitBlock.SetActive(true);
    }

    public void CollectCoin(int coinID)
    {
        if (coinsCollected < totalCoinsInLevel)
        {
            coinsCollected++;
            UpdateUI();
            
            if (coinsCollected >= totalCoinsInLevel)
            {
                AllCoinsCollected();
            }
        }
    }

    private void AllCoinsCollected()
    {
        allCoinsCollected = true;
        
        if (levelExitBlock != null)
            levelExitBlock.SetActive(false);
            
        if (allCoinsMessage != null)
        {
            allCoinsMessage.SetActive(true);
            Invoke("HideMessage", 2f);
        }
    }

    private void HideMessage()
    {
        if (allCoinsMessage != null)
            allCoinsMessage.SetActive(false);
    }

    private void UpdateUI()
    {
        if (coinsText != null)
            coinsText.text = $"Coins: {coinsCollected}/{totalCoinsInLevel}";
    }

    public bool CanExitLevel() => allCoinsCollected;
}