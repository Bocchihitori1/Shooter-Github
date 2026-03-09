using UnityEngine;

public class BoundaryFogController : MonoBehaviour
{
    [Header("Fog Settings")]
    public string shaderProperty = "_FogDensity";
    public string triggerTag = "Player";    // Tag of the object that activates the boundary (usually "Player")
    public float maxFogAmount = 1f;
    public float fadeSpeed = 2f;
    public Transform mapCenter;
    public float boundaryStart = 80f;
    public float boundaryEnd = 100f;
    // units per second

    float currentAmount = 0f;
    float targetAmount = 0f;

    void Start()
    {
        // Ensure shader global exists and starts at 0
        currentAmount = 0f;
        targetAmount = 0f;
        if (!string.IsNullOrEmpty(shaderProperty))
            Shader.SetGlobalFloat(shaderProperty, 0f);
    }

    void Update()
    {
       
   
        float dist = Vector3.Distance(transform.position, mapCenter.position);

        float t = Mathf.InverseLerp(boundaryStart, boundaryEnd, dist);

        Shader.SetGlobalFloat("_FogDensity", t);
    }

}
