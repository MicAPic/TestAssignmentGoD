using UnityEngine;

namespace Health
{
    public class EnemyHealthController : HealthController
    {
        protected override void Die()
        {
            Instantiate(
                GameManager.Instance.pickUpPrefabs[Random.Range(0, GameManager.Instance.pickUpPrefabs.Length)],
                transform.position,
                Quaternion.identity
                );
            base.Die();
        }
    }
}
