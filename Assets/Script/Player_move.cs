using UnityEngine;
using UnityEngine.InputSystem; // ← 新Input Systemに必要

public class BioMovement : MonoBehaviour
{
    private PlayerInputActions inputActions; // 自動生成されたInputActionsクラス
    private Vector2 moveInput;     // Move（前後移動）用
    private float rotateInput;     // Rotate（左右回転）用

    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        // Move (Vector2：上下だけ使う)
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Rotate (float：-1～1)
        inputActions.Player.Rotate.performed += ctx => rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Rotate.canceled += ctx => rotateInput = 0f;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        // 左右回転（X軸ではなくY軸）
        transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);

        // 前後移動（キャラの前向き方向）
        transform.Translate(Vector3.forward * moveInput.y * moveSpeed * Time.deltaTime);
    }
}
