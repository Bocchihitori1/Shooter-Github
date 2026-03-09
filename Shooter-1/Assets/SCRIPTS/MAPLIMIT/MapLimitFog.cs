using UnityEngine;

public class BoundaryFog : MonoBehaviour
{
    public float targetDensity = 0.08f;
    public float normalDensity = 0.01f;
    public float speed = 2f;

    float current;

    void Update()
    {
        RenderSettings.fogDensity = Mathf.Lerp(RenderSettings.fogDensity, current, Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            current = targetDensity;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            current = normalDensity;
    }
}
