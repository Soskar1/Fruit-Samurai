using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class EnemyAttackState : EnemyBaseState
    {
        private float _timer;
        private float _attackTime;

        private Transform _firstAreaPoint;
        private Transform _secondAreaPoint;
        private LayerMask _enemyLayer;

        public EnemyAttackState(Enemy enemy, float attackTime, Transform firstAreaPoint, Transform secondAreaPoint, LayerMask enemyLayer) : base(enemy)
        {
            _attackTime = attackTime;
            _firstAreaPoint = firstAreaPoint;
            _secondAreaPoint = secondAreaPoint;
            _enemyLayer = enemyLayer;
        }

        public override void EnterState() => _timer = _attackTime;

        public override void UpdateState()
        {
            if (_timer <= 0)
            {
                _enemy.SetState(_enemy.ChaseState);
                _timer = _attackTime;
            }
            else
            {
                if (SearchForEnemyInArea(out IHittable enemy))
                    enemy.Hit();

                _timer -= Time.deltaTime;
            }
        }

        public override void FixedUpdateState()
        {
        }

        public override void ExitState()
        {
        }

        private bool SearchForEnemyInArea(out IHittable enemy)
        {
            Collider2D overlapInfo = Physics2D.OverlapArea(_firstAreaPoint.position, _secondAreaPoint.position, _enemyLayer);

            if (overlapInfo != null)
            {
                enemy = overlapInfo.GetComponent<IHittable>();
                return true;
            }

            enemy = null;
            return false;
        }
    }
}