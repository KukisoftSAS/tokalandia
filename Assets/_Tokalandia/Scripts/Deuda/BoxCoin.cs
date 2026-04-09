using Unity.VisualScripting;
using UnityEngine;

public class BoxCoin : MonoBehaviour
{
    [SerializeField] private int coinValue = 5;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //Debug.Log("¡Moneda recogida!");
            GameManager.Instance.AddCoins(coinValue);
            Destroy(gameObject); // Destroy the coin after collecting it
        }
    }
}
