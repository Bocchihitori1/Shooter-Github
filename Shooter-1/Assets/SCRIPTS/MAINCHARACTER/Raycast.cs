using UnityEngine;

public class Raycast : MonoBehaviour

{
  public Transform rayOrigin;
    public float rayDistance = 10f;
    public LayerMask interactiveLayers;
    public RaycastHit hit;
    void Update()
    {
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayDistance, interactiveLayers) && Input.GetKeyDown(KeyCode.E))
        {
        hit.collider.GetComponent<IInteractuables>().Interact();

        }
    }
}
