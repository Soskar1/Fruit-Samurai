using UnityEngine;
using System;

namespace Core.Entities
{
    public class Combat : MonoBehaviour
    {
        [SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private float _attackTime;
        [SerializeField] private float _delay;
        private float _timer;

        [SerializeField] private Transform _firstAttackAreaPoint;
        [SerializeField] private Transform _secondAttackAreaPoint;

        public Action Attacked;

        private bool _isAttacking = false;
        private bool _cooldown = false;
        public bool IsAttacking => _isAttacking;
        public bool Cooldown => _cooldown;

        private void Awake() => _timer = _attackTime;

        private void Update()
        {
            #region Cooldown
            if (_cooldown)
            {
                if (_timer <= 0)
                {
                    _cooldown = false;
                    _timer = _attackTime;
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }

            #endregion

            #region Attack
            if (!_isAttacking)
                return;

            if (_timer <= 0)
            {
                Attacked?.Invoke();

                _isAttacking = false;
                _cooldown = true;
                _timer = _delay;
            }
            else
            {
                if (SearchForEnemyInArea(out IHittable enemy))
                    enemy.Hit();

                _timer -= Time.deltaTime;
            }
            #endregion
        }

        public void StartAttack()
        {
            _timer = _attackTime;
            _isAttacking = true;
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