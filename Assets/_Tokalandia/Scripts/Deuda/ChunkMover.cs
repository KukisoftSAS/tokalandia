using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float moveSpeed = 8f;

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
    }
}