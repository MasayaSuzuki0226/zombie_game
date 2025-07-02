using UnityEngine;
using UnityEngine.InputSystem; // �� �VInput System�ɕK�v

public class BioMovement : MonoBehaviour
{
    private PlayerInputActions inputActions; // �����������ꂽInputActions�N���X
    private Vector2 moveInput;     // Move�i�O��ړ��j�p
    private float rotateInput;     // Rotate�i���E��]�j�p

    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        // Move (Vector2�F�㉺�����g��)
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Rotate (float�F-1�`1)
        inputActions.Player.Rotate.performed += ctx => rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Rotate.canceled += ctx => rotateInput = 0f;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        // ���E��]�iX���ł͂Ȃ�Y���j
        transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);

        // �O��ړ��i�L�����̑O���������j
        transform.Translate(Vector3.forward * moveInput.y * moveSpeed * Time.deltaTime);
    }
}
