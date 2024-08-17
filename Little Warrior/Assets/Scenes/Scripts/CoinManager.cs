using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public int CoinCount = 0;
    public TMP_Text coinText;

    public static CoinManager Instance; // Singleton instance

    private const string CoinCountKey = "CoinCount"; // Key for PlayerPrefs

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    public void AddCoins(int amount)
    {
        CoinCount += amount;
        SaveCoinCount();
    }

    public int GetCoinCount()
    {
        return CoinCount;
    }

    private void SaveCoinCount()
    {
        PlayerPrefs.SetInt(CoinCountKey, CoinCount);
        PlayerPrefs.Save();
    }

    private void LoadCoinCount()
    {
        if (PlayerPrefs.HasKey(CoinCountKey))
        {
            CoinCount = PlayerPrefs.GetInt(CoinCountKey);
        }
        else
        {
            CoinCount = 0; // Default to 0 if no saved value exists
        }
    }

    public void ResetCoinCount()
    {
        CoinCount = 0;
        SaveCoinCount();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinText.text = CoinCount.ToString();
    }
}
