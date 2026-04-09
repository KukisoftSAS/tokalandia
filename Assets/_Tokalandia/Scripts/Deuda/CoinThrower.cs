using UnityEngine;

public class CoinThrower : MonoBehaviour
{
    [Header("Cost")]
    public int coinCost = 5;

    [Header("Ghost Effect")]
    public GhostController ghostController;
    public GhostBlink ghostBlink;
    public float slowDuration = 1.5f;
    public float pushBackAmount = 2f;

    public void ThrowCoins()
    {
        if (GameManager.Instance == null || ghostController == null)
            return;

        bool couldSpend = GameManager.Instance.SpendCoins(coinCost);

        if (!couldSpend)
        {
            Debug.Log("Not enough coins!");
            return;
        }

        ghostController.SlowAndPushBack(slowDuration, pushBackAmount);
        GameManager.Instance.ReduceDebt(3);
        if (ghostBlink != null)
            ghostBlink.Blink();

        Debug.Log("Coins thrown! Ghost slowed.");
    }
}
