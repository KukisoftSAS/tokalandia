using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public bool gameOver = false;

    [Header("Stats")]
    public int coins = 0;
    public int debt = 0;
    public float timeSurvived = 0f;

    [Header("Debt Settings")]
    public int debtIncreaseAmount = 1;
    public float debtIncreaseInterval = 1f;

    [Header("UI")]
    public TMP_Text debtTxt;
    public TMP_Text coinsTxt;
    public TMP_Text timeTxt;
    public GameObject gameOverPanel;

    public AudioClip coinSound;
    public AudioSource audioSource;

    private float debtTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameOver) return;

        // Survival timer
        timeSurvived += Time.deltaTime;

        // Debt rises over time
        debtTimer += Time.deltaTime;
        if (debtTimer >= debtIncreaseInterval)
        {
            debt += debtIncreaseAmount;
            debtTimer = 0f;
        }

        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        if (coins < 0) coins = 0;

        audioSource.PlayOneShot(coinSound);
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            return true;
        }

        return false;
    }

    public void AddDebt(int amount)
    {
        debt += amount;
        if (debt < 0) debt = 0;
        UpdateUI();
    }

    public void ReduceDebt(int amount)
    {
        debt -= amount;
        if (debt < 0) debt = 0;
        UpdateUI();
    }

    public void SetDebt(int amount)
    {
        debt = Mathf.Max(0, amount);
        UpdateUI();
    }

    public float GetTimeSurvived()
    {
        return timeSurvived;
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetDebt()
    {
        return debt;
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("Game Over");
    }

    private void UpdateUI()
    {
        if (coinsTxt != null)
            coinsTxt.text = "" + coins;

        if (debtTxt != null)
            debtTxt.text = "" + debt;

        if (timeTxt != null)
            timeTxt.text = "" + Mathf.FloorToInt(timeSurvived);
    }
}