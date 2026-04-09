using UnityEngine;

public class CoinThrower : MonoBehaviour
{
    public void ThrowCoins()
    {if(GameManager.Instance.coins <= 0)
        {
            return; // No coins to throw
        }
        GameManager.Instance.SpendCoins(1);
        GameManager.Instance.ReduceDebt(1);
    }
}
