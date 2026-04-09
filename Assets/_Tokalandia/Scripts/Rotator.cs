using UnityEngine;

public class Rotator : MonoBehaviour
{
   [Header("Rotation")]
    public Vector3 rotationAxis = new Vector3(0f, 0f, 1f);
    public float rotationSpeed = 180f;
    void Update()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }
}
