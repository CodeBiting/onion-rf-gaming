using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class movimeintoJugador : MonoBehaviour
{
    private enum MachineStates { IDLE, MOVIMENT, RECEIVE, PICKING, PUTAWAY, EXPEDITION };
    private MachineStates m_CurrentState;

    [SerializeField]
    private InputActionAsset m_InputAsset;
    private InputActionAsset m_Input;
    private InputAction m_MovementAction;
    private Rigidbody m_Rigidbody;
    private Animator m_Animator;

    [SerializeField]
    private LayerMask m_ShootMask;

    //Camera
    [SerializeField]
    private GameObject m_Camera;

    [SerializeField] private GameEventTransform m_PasarTransform;

    [Header("Character Values")]
    [SerializeField]
    private float m_Speed = 2;
    [SerializeField]
    private float m_JumpForce = 5f;
    [SerializeField] private float interactDistance = 5f;
    [SerializeField] private GameEvent activarInventario;

    private void ChangeState(MachineStates newState)
    {
        if (newState == m_CurrentState)
            return;

        ExitState();
        InitState(newState);
    }

    private void InitState(MachineStates currentState)
    {
        m_CurrentState = currentState;
        switch (m_CurrentState)
        {
            case MachineStates.IDLE:
                m_Animator.SetFloat("movimiento", 0);
                m_Rigidbody.velocity = Vector3.zero;
                break;

            case MachineStates.MOVIMENT:
                m_Animator.SetFloat("movimiento", 1);
                //m_Animator.Play("PruebaWalk");
                break;
            case MachineStates.RECEIVE:
                break;
            case MachineStates.PICKING:
                break;
            case MachineStates.PUTAWAY:
                break;
            case MachineStates.EXPEDITION:
                break;

            default:
                break;
        }
    }

    private void ExitState()
    {
        switch (m_CurrentState)
        {
            case MachineStates.IDLE:

                break;

            case MachineStates.MOVIMENT:

                break;
            case MachineStates.RECEIVE:
                break;
            case MachineStates.PICKING:
                break;
            case MachineStates.PUTAWAY:
                break;
            case MachineStates.EXPEDITION:
                break;
            default:
                break;
        }
    }

    private void UpdateState()
    {
        switch (m_CurrentState)
        {
            case MachineStates.IDLE:
                if (m_MovementAction.ReadValue<Vector2>().sqrMagnitude > 0)
                {
                    ChangeState(MachineStates.MOVIMENT);
                }
                break;
            case MachineStates.MOVIMENT:
                Vector2 movementInput = m_MovementAction.ReadValue<Vector2>();

                Vector3 movementDirection = new Vector3(movementInput.x, 0f, movementInput.y);
                movementDirection = m_Camera.transform.TransformDirection(movementDirection.normalized) * m_Speed;

                m_Rigidbody.velocity = new Vector3(movementDirection.x, m_Rigidbody.velocity.y, movementDirection.z);

                if (movementDirection == Vector3.zero)
                    ChangeState(MachineStates.IDLE);

                break;
            case MachineStates.RECEIVE:
                break;
            case MachineStates.PICKING:
                break;
            case MachineStates.PUTAWAY:
                break;
            case MachineStates.EXPEDITION:
                break;
            default:
                break;
        }
    }

    void Awake()
    {
        Assert.IsNotNull(m_InputAsset);

        m_Input = Instantiate(m_InputAsset);
        m_MovementAction = m_Input.FindActionMap("Character").FindAction("Movement");
        m_Input.FindActionMap("Character").FindAction("Jump").performed += Jump;
        m_Input.FindActionMap("Character").FindAction("Shoot").performed += Shoot;
        m_Input.FindActionMap("Character").FindAction("Interact").performed += CheckForInteractableObject;
        m_Input.FindActionMap("Character").FindAction("InventarioCajas").performed += OnInventory;
        m_Input.FindActionMap("Character").Enable();

        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InitState(MachineStates.IDLE);
    }

    void Update()
    {
        UpdateState();
    }

    private void OnDestroy()
    {
        m_Input.FindActionMap("Character").Disable();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //make my character jump only when is on the ground
        if (Physics.Raycast(transform.position, Vector3.down, 1.1f))
        {
            m_Rigidbody.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);
        }

    }

    private void Shoot(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 20f, m_ShootMask))
        {
            Debug.Log($"He tocat {hit.collider.gameObject} a la posici {hit.point} amb normal {hit.normal}");
            Debug.DrawLine(m_Camera.transform.position, hit.point, Color.green, 2f);
        }
    }

    void CheckForInteractableObject(InputAction.CallbackContext context)
    {
        // Raycast o colisión para detectar objetos cercanos
        RaycastHit hit;
        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, interactDistance))
        {
            // Verifica si el objeto es interactuable
            if ( hit.collider.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
            {
                // Lógica para recoger o soltar el objeto
                m_PasarTransform.Raise(transform);
                interactableObject.ToggleInteraction();
            }
        }
    }

    // si presiono tecla I, activo el inventario
    private void OnInventory(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, interactDistance))
        {
            // Verifica si el objeto es interactuable
            if (hit.collider.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
            {
                Debug.Log("Activar inventario");
                activarInventario.Raise();
            }
        }

    }
}
