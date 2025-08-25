using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; // 新Input System

public class Player_Move : MonoBehaviour
{
    private PlayerInputActions inputActions;
    private Vector2 _moveInput;
    private float _rotateInput;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotationSpeed = 120f;
    private string text; // 表示するテキスト（UIなどに使える）
    private GameObject nearbyItem = null; // 今ぶつかってるアイテム
    private TextMeshProUGUI _item_text;// アイテムテキスト
    public List<string> Item_list = new List<string>();
    public int HP=100;
    public TextMeshProUGUI HP_text;
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;

        inputActions.Player.Rotate.performed += ctx => _rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Rotate.canceled += ctx => _rotateInput = 0f;

        // Interactアクションの登録（Fキー or Aボタン）
        inputActions.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Rotate.performed -= ctx => _rotateInput = ctx.ReadValue<float>();
        inputActions.Player.Interact.performed -= OnInteract;

        inputActions.Player.Disable();
    }
    private void Update()
    {
        // 回転
        transform.Rotate(Vector3.up, _rotateInput * _rotationSpeed * Time.deltaTime);
        // 前進・後退
        transform.Translate(Vector3.forward * _moveInput.y * _moveSpeed * Time.deltaTime);
    }
    // アイテムにぶつかっている間
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Item_list.Add(collision.gameObject.name);
            Destroy(collision.gameObject);
        }

        //Debug.Log(collision.gameObject.name);
    }

    // 離れたらリセット
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            
        }
    }
    // Interact（F or Aボタン）を押したときの処理
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (nearbyItem != null)
        {
            nearbyItem = null;
            _item_text.text = "I got" +nearbyItem+ "!";
        }
    }
    public void Damage(int damage)
    {
        HP -= damage;
        HP_text.text = "HP " + HP;
        //navmeshを使って追跡AIを作成
    }

}