using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private Transform padre;
    private float moX;
    private float moY;
    [SerializeField] private float sensX = 100f;
    [SerializeField] private float sensY = 100f;

    private MainCharacter pM;

    private float rotX = 0;

    [Header("Blob Movement")]
    [SerializeField] private float walkingSpeed = 1f;

    [SerializeField, Range(0, 0.1f)] private float walkingAmplitude = 0.015f; // Que tanto se mueve hacia los lados al caminar
    [SerializeField, Range(0, 0.1f)] private float runningAmplitude = 0.03f;  // Que tanto se mueve hacia los lados al correr
    [SerializeField, Range(0, 15)] private float walkingFrequency = 10.0f; // La frecuencia con la que se mueve al caminar
    [SerializeField, Range(10, 30)] private float runningFrequency = 18f; // La frecuencia con la que se mueve al correr
    [SerializeField] private float resetPosSpeed = 3.0f; // Cuando dejas de moverte que regrese al centro
    [SerializeField] private float blobLerpSpeed = 10f; // Velocidad para interpolar la cámara hacia target (suaviza movimiento)

    private Vector3 startPos;

    private Animator animator;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Cache parent y componentes
        padre = transform.parent;
        if (padre == null)
        {
            Debug.LogWarning("MainCamera: El transform no tiene padre asignado. El script espera que la cámara sea hija del jugador.");
            return;
        }

        pM = padre.GetComponent<MainCharacter>();
        if (pM == null)
        {
            Debug.LogWarning("MainCamera: No se encontró MainCharacter en el padre. Asegurate que exista ese script.");
        }

        animator = padre.GetComponent<Animator>();
        // No destruir el animator automáticamente; si necesitas quitarlo hazlo explícitamente.
        startPos = transform.localPosition;
    }

    private void Update()
    {
        RotateCamera();
        BlobMove();
        ResetPosition();
    }

    private void RotateCamera()
    {
        moX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        padre.Rotate(0f, moX, 0f);

        moY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        rotX -= moY;
        rotX = Mathf.Clamp(rotX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
    }

    private void BlobMove()
    {
        if (pM == null) return;

        Vector2 input = new Vector2(pM.moveHorizontal, pM.moveVertical);
        float inputMag = input.magnitude;

        // Decide tipo de movimiento
        bool isMoving = inputMag > 0.01f;
        bool isMovingBackwardOrSideways = (pM.moveHorizontal != 0f && Mathf.Abs(pM.moveHorizontal) > Mathf.Abs(pM.moveVertical)) || (pM.moveVertical < 0f && Mathf.Abs(pM.moveVertical) > Mathf.Abs(pM.moveHorizontal));
        bool isMovingForward = (pM.moveVertical > 0f) && !isMovingBackwardOrSideways;

        // Si no se mueve, no generamos offset (ResetPosition lo regresará al centro)
        Vector3 bobOffset = Vector3.zero;
        if (isMoving)
        {
            // escalar amplitud por la magnitud de entrada para suavizar si se camina diagonal/leve
            float speedFactor = Mathf.Clamp01(inputMag);

            if (isMovingForward && pM.isRunning)
            {
                bobOffset = RunningFootStepMotion(speedFactor);
            }
            else
            {
                bobOffset = FootStepMotion(speedFactor);
            }
        }

        Vector3 targetLocalPos = startPos + bobOffset;
        // Interpolar para evitar saltos bruscos y permitir retorno suave
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetLocalPos, blobLerpSpeed * Time.deltaTime);
    }

    private void ResetPosition()
    {
        if (pM == null) return;


        // Si la cámara ya está prácticamente en la posición inicial, no hacemos nada
        if (Vector3.Distance(transform.localPosition, startPos) < 0.001f) return;

        // La interpolación ya está siendo aplicada en BlobMove hacia startPos cuando no hay movimiento,
        // pero aseguramos un retorno más rápido si es necesario:
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, resetPosSpeed * Time.deltaTime);
    }

    private Vector3 FootStepMotion(float speedFactor)
    {
        Vector3 pos = Vector3.zero;
        float t = Time.time * walkingFrequency * Mathf.Lerp(0.8f, 1.0f, speedFactor);
        float amp = walkingAmplitude * Mathf.Lerp(0.5f, 1.0f, speedFactor) * walkingSpeed;

        pos.y = Mathf.Sin(t) * amp; // bob vertical
        pos.x = Mathf.Cos(t * 0.5f) * amp * 0.6f; // leve movimiento lateral
        return pos;
    }

    private Vector3 RunningFootStepMotion(float speedFactor)
    {
        Vector3 pos = Vector3.zero;
        float t = Time.time * runningFrequency * Mathf.Lerp(1.0f, 1.2f, speedFactor);
        float amp = runningAmplitude * Mathf.Lerp(0.8f, 1.0f, speedFactor) * walkingSpeed;

        pos.y = Mathf.Sin(t) * amp * 1.25f; // mayor bob vertical al correr
        pos.x = Mathf.Cos(t * 0.5f) * amp * 1.0f; // mayor movimiento lateral al correr
        return pos;
    }

    // Método accesible para UI/debug
    public float GetStartOffsetDistance()
    {
        return Vector3.Distance(transform.localPosition, startPos);
    }
}
