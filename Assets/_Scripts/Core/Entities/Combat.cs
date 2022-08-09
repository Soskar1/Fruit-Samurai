using UnityEngine;
using System;

namespace Core.Entities
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private int _damage = 1;

        [SerializeField] private Transform _firstAttackAreaPoint;
        [SerializeField] private Transform _secondAttackAreaPoint;

        public Action Attacked;

        public void TryAttack()
        {
            if (SearchForEnemyInArea(out IHittable enemy))
                enemy.Hit(_damage);

            Attacked?.Invoke();
        }

        private bool SearchForEnemyInArea(out IHittable enemy)
        {
            Collider2D overlapInfo = Physics2D.OverlapArea(_firstAttackAreaPoint.position, _secondAttackAreaPoint.position, _enemyLayer);

            if (overlapInfo != null)
            {
                enemy = overlapInfo.GetComponent<IHittable>();
                return true;
            }

            enemy = null;
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawLine(_firstAttackAreaPoint.position, new Vector2(_secondAttackAreaPoint.position.x, _firstAttackAreaPoint.position.y));
            Gizmos.DrawLine(new Vector2(_secondAttackAreaPoint.position.x, _firstAttackAreaPoint.position.y), _secondAttackAreaPoint.position);
            Gizmos.DrawLine(_secondAttackAreaPoint.position, new Vector2(_firstAttackAreaPoint.position.x, _secondAttackAreaPoint.position.y));
            Gizmos.DrawLine(new Vector2(_firstAttackAreaPoint.position.x, _secondAttackAreaPoint.position.y), _firstAttackAreaPoint.position);
        }
    }
}