using UnityEngine;
using System.Collections;

public class GhostBlink : MonoBehaviour
{
    [SerializeField] private Renderer ghostRenderer;

    [Header("Blink Settings")]
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float blinkDuration = 0.15f;
    [SerializeField] private AudioSource hitSound;

    private Material ghostMaterial;
    private Color originalColor;

    void Start()
    {
        if (ghostRenderer == null)
            ghostRenderer = GetComponentInChildren<Renderer>();

        ghostMaterial = ghostRenderer.material;

        // Try both shader properties
        if (ghostMaterial.HasProperty("_BaseColor"))
            originalColor = ghostMaterial.GetColor("_BaseColor");
        else
            originalColor = ghostMaterial.GetColor("_Color");
    }

    public void Blink()
    {
        StopAllCoroutines();
        StartCoroutine(BlinkRoutine());
    }

    IEnumerator BlinkRoutine()
    {
        SetColor(hitColor);
        hitSound.PlayOneShot(hitSound.clip);
        yield return new WaitForSeconds(blinkDuration);

        SetColor(originalColor);
    }

    void SetColor(Color color)
    {
        if (ghostMaterial.HasProperty("_BaseColor"))
            ghostMaterial.SetColor("_BaseColor", color);
        else
            ghostMaterial.SetColor("_Color", color);
    }
}