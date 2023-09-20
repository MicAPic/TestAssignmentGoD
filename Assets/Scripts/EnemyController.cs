using System;
using System.Collections.Generic;
using Health;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Idle,
        Chase
    }
    private State _currentState;
    
    [Header("Attack")]
    [SerializeField]
    private float damageToDeal = 15f;
    [SerializeField]
    private float attackDistance = 6.0f;
    
    [Header("Movement")]
    [SerializeField]
    private float movementSpeed = 15f;
    private Vector2 _movementDirection;

    [Header("Visuals")]
    [SerializeField]
    private Transform body;
    private bool _isFlipped;
    private static Dictionary<bool, Vector3> _flipTable = new()
    {
        {false, Vector3.one},
        {true, new Vector3(-1, 1, 1)}
    };

    private float _playerDistance;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        _playerDistance = Vector2.Distance(PlayerController.Instance.transform.position, transform.position);
        switch (_currentState)
        {
            case State.Idle:
                if (_playerDistance < attackDistance)
                {
                    _currentState = State.Chase;
                    _rb.WakeUp();
                }
                return;
            case State.Chase:
                if (_playerDistance >= attackDistance)
                {
                    _currentState = State.Idle;
                    _rb.Sleep();
                }
                _movementDirection = (PlayerController.Instance.transform.position - transform.position).normalized;
                break;
        }
        
        var spriteFlipCheck = _movementDirection.x < 0;
        if (_isFlipped == spriteFlipCheck) return;
        _isFlipped = !_isFlipped;
        body.localScale = _flipTable[_isFlipped];
    }

    void FixedUpdate()
    {
        _rb.velocity = _movementDirection * movementSpeed;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out PlayerHealthController playerHealth))
        {
            playerHealth.TakeDamage(damageToDeal);
        }
    }
}
