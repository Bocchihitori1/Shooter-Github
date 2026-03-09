using UnityEngine;
using System.Collections;
using TMPro;

public class HealingBox : MonoBehaviour, IInteractuables
{
    public float creationTime = 5f;
    public BoxState boxState;
    public TextMeshProUGUI infoText;
    public Transform player;
    private Coroutine expirationCoroutine;
    private MainCharHealth mainCharHealth;
    public int healAmount;

    private void Start()
    {
        boxState = BoxState.Idle;
        infoText.text = "Press E to create bandages.";
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        mainCharHealth = player.GetComponent<MainCharHealth>();
    }
    private void Update()
    {
        Quaternion lookRotation = (player.rotation);
        infoText.transform.rotation = lookRotation;
    }
    public void Interact()

    {
        Debug.Log("Healing...");
        // Add healing logic here

        switch (boxState)
        {
            case BoxState.Idle:
                boxState = BoxState.Creating;
                StartCoroutine(CreationTime());
                Debug.Log("Creating bandages...");
                infoText.text = "Creating bandages... Time left: " + creationTime + "s";
                break;
            case BoxState.Creating:
                Debug.Log("Bandages are being created. Please wait.");
                infoText.text = "Bandages are being created. Please wait.";
                break;
            case BoxState.Ready:
                boxState = BoxState.Idle;
                Debug.Log("Bandages collected!");
                infoText.text = "Bandages collected!";
                mainCharHealth.Heal(healAmount); // Heal the player by 30
                creationTime = 5f; // Reset timer
                StopCoroutine(expirationCoroutine);
                expirationCoroutine = null;
                break;
            case BoxState.Expired:
                Debug.Log("Bandages expired. Please start over.");
                infoText.text = "Bandages expired. Please start over.";
                boxState = BoxState.Idle;
                creationTime = 5f; // Reset timer
                break;
        }


    }

    // Al acercase a la caja te sale un mensaje de ( presiona E para fabricar balas) le picas y el mensaje se cambia
    // por un timer de 2:00 y al terminar se cambia por otro mensaje de Balas terminadas Presiona E para recogerlas
    // Las balas estaras 15 segundos listas, si no se presiona E cuando esten listas se perderan y aparece un mensaje de 
    // balas perdidas

    public IEnumerator CreationTime()
    {
        while (creationTime > 0)
        {
            yield return new WaitForSeconds(1f);
            creationTime--;
        }
        infoText.text = "Bandages ready! Press E to collect.";
        boxState = BoxState.Ready;

        expirationCoroutine = StartCoroutine(ExpireTime());
    }
    public IEnumerator ExpireTime()
    {
        yield return new WaitForSeconds(15f);
        boxState = BoxState.Expired;
        infoText.text = "Bandages expired. Please start over.";
    }
    public enum BoxState
    {
        Idle,
        Creating,
        Ready,
        Expired
    }
}
