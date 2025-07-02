//using UnityEngine;
//using UnityEngine.InputSystem;

//public class Player_move : MonoBehaviour
//{
//    private PlayerInputActions inputActions;
//    private Vector2 moveInput;
//    private float rotationInput;
//    public float moveSpeed = 3f;
//    public float rotationSpeed = 100f;

//    private void Awake()
//    {
//        inputActions = new PlayerInputActions();
//    }

//    private void OnEnable()
//    {
//        inputActions.Player.Enable();
//        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
//        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

//        inputActions.Player.Rotate.performed += ctx => rotationInput = ctx.ReadValue<float>();
//        inputActions.Player.Rotate.canceled += ctx => rotationInput = 0f;
//    }

//    private void OnDisable()
//    {
//        inputActions.Player.Disable();
//    }

//    void Update()
//    {
//        // ��]�i���E���́j
//        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);

//        // �O�i�E��ށi�㉺���́j
//        transform.Translate(Vector3.forward * moveInput.y * moveSpeed * Time.deltaTime);
//    }
//}

