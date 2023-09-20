using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Shooting")]
    public float firePower = 5.0f;
    public float damageToDeal = 10.0f;
    public float aimRange = 5.0f;
    public Vector2 aimDirection;
    [SerializeField]
    private LayerMask shootableLayers;
    [SerializeField]
    private Item ammoReference;
    
    [Header("Physics & Movement")] 
    public float movementSpeed;
    private Vector2 _movementValue;

    [Header("UI")]
    [SerializeField]
    private Image shootButton;
    private readonly Color _shootButtonInactiveColour = new(1.0f, 1.0f, 1.0f, 0.5f);
    private bool _isAiming;

    [Header("Visuals")]
    [SerializeField]
    private Transform body;
    private bool _isFlipped;
    private static Dictionary<bool, Vector3> _flipTable = new()
    {
        {false, Vector3.one},
        {true, new Vector3(-1, 1, 1)}
    };
    
    private Rigidbody2D _rb;
    private PlayerInput _playerInput;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _rb = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
    }
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        // Input processing
        if (_playerInput.actions["ToggleInventory"].WasPressedThisFrame())
        {
            GameManager.Instance.TogglePause();
            InventoryManager.Instance.ToggleInventory();
        }
        if (_playerInput.actions["Shoot"].WasPressedThisFrame())
        {
            Shoot();
        }
        
        // Check if there are any enemies in the aiming range, aim at the 1st one detected
        var hit = Physics2D.OverlapCircle(transform.position, aimRange, shootableLayers);
        if (hit)
        {
            aimDirection = hit.transform.position - transform.position;
            aimDirection.Normalize();
            
            if (!_isAiming)
            {
                _isAiming = !_isAiming;
                shootButton.color = Color.white;
            }
        }
        else
        {
            aimDirection = Vector2.zero;
            
            if (_isAiming)
            {
                _isAiming = !_isAiming;
                shootButton.color = _shootButtonInactiveColour;
            }
        }
        
        var spriteFlipCheck = _movementValue.x < 0;
        if (_isFlipped == spriteFlipCheck) return;
        _isFlipped = !_isFlipped;
        body.localScale = _flipTable[_isFlipped];
    }
    
    void FixedUpdate()
    {
        // Movement
        _rb.velocity = _movementValue * movementSpeed;
    }
    
    void OnMove(InputValue value)
    {
        _movementValue = value.Get<Vector2>();
    }

    private void Shoot()
    {
        if (aimDirection == Vector2.zero || !InventoryManager.Instance.HasItem(ammoReference)) return;
        
        var bullet = BulletPool.Instance.GetBulletFromPool();
        bullet.transform.position = transform.position;
        bullet.transform.right = aimDirection;
        bullet.Enable(aimDirection, firePower, damageToDeal);
        InventoryManager.Instance.Remove(ammoReference);
    }
}
