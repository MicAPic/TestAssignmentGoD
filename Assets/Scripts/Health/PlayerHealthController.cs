using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Health
{
    public class PlayerHealthController : HealthController
    {
        [SerializeField]
        private float hitInvincibilityTime = 0.15f;
        private bool _canTakeDamage = true;
        
        public override void TakeDamage(float damagePoints)
        {
            if (!_canTakeDamage) return;
            base.TakeDamage(damagePoints);
            StartCoroutine(ToggleInvincibility());
        }

        protected override void Die()
        {
            EditorApplication.ExitPlaymode();
        }

        private IEnumerator ToggleInvincibility()
        {
            _canTakeDamage = false;
            yield return new WaitForSeconds(hitInvincibilityTime);
            _canTakeDamage = true;
        }
    }
}
