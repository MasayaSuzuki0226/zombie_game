using UnityEngine;
using UnityEngine.InputSystem; // 新Input System

public class Player_Move : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private float rotateInput;

    public float moveSpeed = 3f;
    public float rotationSpeed = 120f;
    private string text; // 表示するテキスト（UIなどに使える）
    private GameObject nearbyItem = null; // 今ぶつかってるアイテム

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Rotate.performed += ctx => rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Rotate.canceled += ctx => rotateInput = 0f;

        // Interactアクションの登録（Fキー or Aボタン）
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Rotate.performed -= ctx => rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Interact.performed -= OnInteract;

        inputActions.Player.Disable();
    }

    private void Update()
    {
        // 回転
        transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime);
        // 前進・後退
        transform.Translate(Vector3.forward * moveInput.y * moveSpeed * Time.deltaTime);
    }

    // アイテムにぶつかっている間
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            nearbyItem = collision.gameObject;
            text = "F 拾う"; // このtextをUIに使えば表示できるよ
        }
    }

    // 離れたらリセット
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            nearbyItem = null;
            text = "";
        }
    }

    // Interact（F or Aボタン）を押したときの処理
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (nearbyItem != null)
        {
            text = nearbyItem.name + " を拾った！";
            Destroy(nearbyItem);
            nearbyItem = null;

            // 2秒後にtextを空にしたい場合
            // Invoke(nameof(ClearText), 2f);
        }
    }

    // textを消す関数（使いたければ↑のInvokeと合わせて）
    // private void ClearText()
    // {
    //     text = "";
    // }
}