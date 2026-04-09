using UnityEngine;

public class GhostController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameManager gameManager;

    [Header("Z Chase Settings")]
    public float startZ = 20f;
    public float catchZ = 4f;
    public float maxZ = 24f;

    [Header("Movement")]
    public float baseMoveSpeed = 0.8f;
    public float debtSpeedMultiplier = 0.03f;
    public float lateralFollowSpeed = 5f;

    [Header("Slow Effect")]
    public float slowMultiplier = 0.35f;
    public float slowDuration = 1.5f;

    [Header("Pushback")]
    public float defaultPushBack = 2f;

    [Header("Visuals")]
    public float baseY = 0f;
    public float bobSpeed = 4f;
    public float bobHeight = 0.15f;
    public Vector3 farScale = Vector3.one * 0.9f;
    public Vector3 nearScale = Vector3.one * 1.2f;

    private float slowTimer = 0f;

    void Start()
    {
        Vector3 pos = transform.position;
        pos.z = startZ;
        pos.y = baseY;
        transform.position = pos;
    }

    void Update()
    {
        if (gameManager == null || player == null) return;
        if (gameManager.gameOver) return;

        UpdateGhostMovement();
        UpdateGhostVisuals();
        CheckCatchPlayer();
    }

    void UpdateGhostMovement()
    {
        float debtFactor = gameManager.debt * debtSpeedMultiplier;
        float moveSpeed = baseMoveSpeed + debtFactor;

        if (slowTimer > 0f)
        {
            slowTimer -= Time.deltaTime;
            moveSpeed *= slowMultiplier;
        }

        Vector3 pos = transform.position;

        // Follow the player a bit on X
        pos.x = Mathf.Lerp(pos.x, player.position.x, lateralFollowSpeed * Time.deltaTime);

        // Move ghost toward the player by decreasing Z
        pos.z -= moveSpeed * Time.deltaTime;

        // Optional bobbing on Y
        pos.y = baseY + Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Clamp so the ghost never goes too far back or too far through the player
        pos.z = Mathf.Clamp(pos.z, catchZ, maxZ);

        transform.position = pos;
    }

    void UpdateGhostVisuals()
    {
        // When ghost is far away at high Z, use farScale
        // When ghost is close to catchZ, use nearScale
        float t = Mathf.InverseLerp(startZ, catchZ, transform.position.z);
        transform.localScale = Vector3.Lerp(farScale, nearScale, t);
    }

    void CheckCatchPlayer()
    {
        if (transform.position.z <= catchZ)
        {
            gameManager.GameOver();
        }
    }

    public void SlowGhost()
    {
        slowTimer = slowDuration;
    }

    public void PushBackGhost()
    {
        Vector3 pos = transform.position;
        pos.z += defaultPushBack;
        pos.z = Mathf.Clamp(pos.z, catchZ, maxZ);
        transform.position = pos;
    }

    public void SlowAndPushBack(float duration, float pushBackAmount)
    {
        slowTimer = duration;

        Vector3 pos = transform.position;
        pos.z += pushBackAmount;
        pos.z = Mathf.Clamp(pos.z, catchZ, maxZ);
        transform.position = pos;
    }

    public void AdvanceGhost(float amount)
    {
        Vector3 pos = transform.position;
        pos.z -= amount;
        pos.z = Mathf.Clamp(pos.z, catchZ, maxZ);
        transform.position = pos;
    }

    public float GetDangerPercent()
    {
        return Mathf.InverseLerp(startZ, catchZ, transform.position.z);
    }

    public void ResetGhost()
    {
        Vector3 pos = transform.position;
        pos.z = startZ;
        pos.y = baseY;
        transform.position = pos;

        slowTimer = 0f;
    }
}