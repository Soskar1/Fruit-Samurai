using Core.Entities.Movement;
using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class EnemyChaseState : EnemyBaseState
    {
        private Flipping _flipping;
        private IMovable _movable;
        private Transform _target;
        private Transform _transform;

        private float _attackDistance;

        public EnemyChaseState(Enemy enemy, Flipping flipping, IMovable movable, Transform target, float attackDistance) : base(enemy)
        {
            _flipping = flipping;
            _movable = movable;

            _target = target;
            _transform = enemy.transform;

            _attackDistance = attackDistance;
        }

        public override void EnterState()
        {
        }

        public override void UpdateState()
        {
            if (_target.position.x > _transform.position.x && !_flipping.FacingRight ||
                _target.position.x < _transform.position.x && _flipping.FacingRight)
                _flipping.Flip();

            if (Vector2.Distance(_target.position, _transform.position) <= _attackDistance)
                _enemy.SetState(_enemy.AttackState);
        }

        public override void FixedUpdateState() => _movable.Move(GetMovementDirection());

        public override void ExitState()
        {
        }

        private float GetMovementDirection()
        {
            if (_target.position.x > _transform.position.x)
                return 1;
            else
                return -1;
        }
    }
}