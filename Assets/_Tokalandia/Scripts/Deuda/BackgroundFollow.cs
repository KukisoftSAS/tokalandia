using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform player;

    [Header("Follow Settings")]
    public float followSpeed = 2f;
    public float zOffset = 50f; // distance behind player

    private float targetZ;

    void Start()
    {
        if (player != null)
        {
            targetZ = player.position.z + zOffset;
            transform.position = new Vector3(transform.position.x, transform.position.y, targetZ);
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        targetZ = player.position.z + zOffset;

        Vector3 newPos = transform.position;
        newPos.z = Mathf.Lerp(transform.position.z, targetZ, followSpeed * Time.deltaTime);

        transform.position = newPos;
    }
}