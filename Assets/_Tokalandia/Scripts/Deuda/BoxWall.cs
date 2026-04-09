using UnityEngine;

public class BoxWall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            // Aquí puedes agregar la lógica para manejar la colisión con el jugador
            Debug.Log("¡Colisión con la pared!");
            // Por ejemplo, podrías reducir la vida del jugador o reiniciar el nivel
            DestroyWall();
        }
    }

    void DestroyWall()
    {
        // Aquí puedes agregar la lógica para destruir la pared después de un tiempo o bajo ciertas condiciones
        Destroy(gameObject);
    }
}
