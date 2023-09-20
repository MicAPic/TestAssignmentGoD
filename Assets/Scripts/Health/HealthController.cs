using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public abstract class HealthController : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField]
        private float healthPoints;

        private float _maxHealth;

        [Header("Visuals")]
        [SerializeField]
        private GameObject healthBar;
        [SerializeField]
        private Image healthBarFill;

        void Awake()
        {
            _maxHealth = healthPoints;
        }

        // public float GetCurrentHealth()
        // {
        //     // can't just use a getter/setter, otherwise the field wouldnt be serializable
        //     return healthPoints;
        // }

        public virtual void TakeDamage(float damagePoints)
        {
            healthPoints -= damagePoints;
            if (healthPoints <= 0)
            {
                Die();
            }

            if (!healthBar.activeInHierarchy)
            {
                // like in VS: health bar isn't active until we take damage
                healthBar.SetActive(true);
            }
            healthBarFill.fillAmount = healthPoints / _maxHealth;
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
