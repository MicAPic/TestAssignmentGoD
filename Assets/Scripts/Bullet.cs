using Health;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float damage;
    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponentInChildren<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthController healthController) && healthController.enabled)
        {
            healthController.TakeDamage(damage);
        }
        Disable();
    }
    
    private void OnBecameInvisible()
    {
        Disable();
    }

    public void Enable(Vector3 direction, float firePower, float damageToDeal)
    {
        var normalizedDirection = (Vector2) direction;
        normalizedDirection.Normalize();

        damage = damageToDeal;
        gameObject.SetActive(true);
        _rb.AddForce(normalizedDirection * firePower, ForceMode2D.Impulse);
        
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Disable()
    {
        _rb.constraints = RigidbodyConstraints2D.None;
        gameObject.SetActive(false);
    }
}