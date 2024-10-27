using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Info")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    private float speed;
    [SerializeField] private float rotationSpeed = 5f;
    private bool isRunning;

    [Header("Aim Info")]
    [SerializeField] private LayerMask aimLayerMask;
    [SerializeField] private GameObject aimIndicator; // Drag your 3D aim object here in the inspector

    private Animator animator;
    private Vector2 moveInput;
    private Vector3 movementDirection;
    private RaycastHit hitInfo;
    private float verticalVelocity;
    private CharacterController characterController;
    private PlayerControl controls; // Assuming this is set up with Unity's new Input System

private void Awake()
{
    // Initialize the input controls
    controls = new PlayerControl();

    controls.Character.Fire.performed += context => Shoot();
    controls.Character.Movement.performed += context => {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log("Movement input detected: " + moveInput);
    };
    controls.Character.Movement.canceled += context => {
        moveInput = Vector2.zero;
        Debug.Log("Movement input canceled");
    };

    controls.Character.Aim.performed += context => UpdateAimIndicator();
    controls.Character.Aim.canceled += context => UpdateAimIndicator();
    
    // Run input handling: set isRunning based on input
    controls.Character.Run.performed += context => {
        isRunning = true;
        Debug.Log("Run input detected: isRunning set to true");
    };
    controls.Character.Run.canceled += context => {
        isRunning = false;
        Debug.Log("Run input released: isRunning set to false");
    };

    Debug.Log("Input system initialized.");
}


    private void Start()
    {
        // Initialize components
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Disable the aim indicator by default
        if (aimIndicator != null)
            aimIndicator.SetActive(false);

        // Set the default movement speed to walk speed
        speed = walkSpeed;
    }

    private void Update()
    {
        ApplyMovement();
        UpdateAimIndicator();
        AnimatorControllers();
    }

    private void ApplyMovement()
    {
        // Set movement direction based on input
        movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

        // Determine the speed based on movement and run state
        if (isRunning && movementDirection.sqrMagnitude > 0)
        {
            speed = runSpeed;
            Debug.Log("Running at run speed.");
        }
        else
        {
            speed = walkSpeed;
            Debug.Log("Walking at walk speed.");
        }

        ApplyGravity();

        if (movementDirection.sqrMagnitude > 0)
        {
            // Move character based on current speed
            characterController.Move(movementDirection * Time.deltaTime * speed);
        }

        // Rotate towards the aiming point smoothly if the aim indicator is active
        if (aimIndicator != null && aimIndicator.activeSelf)
        {
            Vector3 direction = aimIndicator.transform.position - transform.position;
            direction.y = 0; // Ensure the player only rotates around the y-axis
            if (direction != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Fire");
    }

    private void AnimatorControllers()
    {
        // Calculate movement components for the animator
        float xVelocity = Vector3.Dot(movementDirection.normalized, transform.right);
        float zVelocity = Vector3.Dot(movementDirection.normalized, transform.forward);

        animator.SetFloat("xVelocity", xVelocity, .1f, Time.deltaTime);
        animator.SetFloat("zVelocity", zVelocity, .1f, Time.deltaTime);

        // Set running state only if player is moving and shift is held
        bool playRunAnimation = isRunning && movementDirection.sqrMagnitude > 0;
        animator.SetBool("isRunning", playRunAnimation);

        // Debug to check if the run animation should play
        Debug.Log($"Animator isRunning: {playRunAnimation}, isRunning: {isRunning}, movementDirection.magnitude: {movementDirection.magnitude}");
    }

    private void UpdateAimIndicator()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool hit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, aimLayerMask);

        if (aimIndicator != null)
        {
            aimIndicator.SetActive(hit);
            
            if (hit)
            {
                aimIndicator.transform.position = hitInfo.point;
            }
        }
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
        }
        else
        {
            verticalVelocity = -0.5f;
        }
        movementDirection.y = verticalVelocity;
    }

    private void OnEnable()
    {
        controls.Enable();
        Debug.Log("Player controls enabled.");
    }

    private void OnDisable()
    {
        controls.Disable();
        Debug.Log("Player controls disabled.");
    }
}
