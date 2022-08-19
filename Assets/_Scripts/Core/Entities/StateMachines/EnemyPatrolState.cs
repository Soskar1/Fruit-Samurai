using Core.Entities.Movement;
using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private float _timer;
        private float _patrolTime;
        private IMovable _movable;
        private Patrol _patrol;

        public EnemyPatrolState(Enemy enemy, float patrolTime, IMovable movable, Patrol patrol) : base(enemy)
        {
            _patrolTime = patrolTime;
            _movable = movable;
            _patrol = patrol;
        }

        public override void EnterState()
        {
            Debug.Log("Патрулирую");
            _timer = _patrolTime;
        }

        public override void UpdateState()
        {
            if (_timer <= 0)
            {
                _enemy.SetState(_enemy.IdleState);
                _timer = _patrolTime;
            }
            else
            {
                _movable.Move(_patrol.GetMovementDirection());
                _timer -= Time.deltaTime;
            }
        }
    }
}