using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public float walkSpeed = 5f;
    public bool isWalking = false;
    public float runSpeed = 10f;
    public bool isRunning;
    public float moveHorizontal;
    public float moveVertical;

    public float maxStamina = 5f;          
    public float staminaDrainRate = 1f;     
    public float staminaRecoveryRate = 1.5f;
    private Vector3 velocidadY;
    public float gravity = -9.81f;
    public bool isGrounded;
   public Transform groundCheck;
    public float radius;
    public LayerMask groundMask;



    public float currentStamina;
    public bool canRun = true;
    private CharacterController controller;
    public float jumpHeight = 2f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, radius, groundMask);
        if (isGrounded && velocidadY.y <= 0)
        {
            velocidadY.y = 0f;
        }

        velocidadY.y += gravity * Time.deltaTime;
        controller.Move(velocidadY * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocidadY.y = Mathf.Sqrt(2f * -gravity);
        }



         isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f&& canRun;
        float speed = isRunning ? runSpeed : walkSpeed;

        if (currentStamina <= 0f)
        {
            canRun = false; 
        }   
        else if (currentStamina == maxStamina)
        {
            canRun = true; 
        }

         moveHorizontal = Input.GetAxis("Horizontal");
         moveVertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
        if (move.magnitude > 0.1f)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        controller.Move(move * speed * Time.deltaTime);

        // Manejo de stamina
        if (isRunning && move.magnitude > 0f)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0f);
        }
        else
        {
            currentStamina += staminaRecoveryRate * Time.deltaTime;
            currentStamina = Mathf.Min(currentStamina, maxStamina);
        }

    }

    // Opcional: método para obtener el porcentaje de stamina (útil para UI)
    public float GetStaminaPercent()
    {
        return currentStamina / maxStamina;
    }
}
