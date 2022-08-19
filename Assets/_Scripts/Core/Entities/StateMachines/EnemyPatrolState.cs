using Core.Entities.Movement;
using UnityEngine;

namespace Core.Entities.StateMachines
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private Flipping _flipping;
        private IMovable _movable;
        private float _movementDirection;

        private Vector2 _leftWaypoint;
        private Vector2 _rightWaypoint;
        private Vector2 _targetWaypoint;
        private Transform _transform;

        public EnemyPatrolState(Enemy enemy, Flipping flipping, IMovable movable, Vector2 leftWaypoint, Vector2 rightWaypoint) : base(enemy)
        {
            _flipping = flipping;
            _movable = movable;

            _leftWaypoint = leftWaypoint;
            _rightWaypoint = rightWaypoint;
            _transform = enemy.transform;
        }

        public override void EnterState()
        {
            _flipping.Flip();
            _movementDirection = _flipping.FacingRight ? 1 : -1;

            _targetWaypoint = _flipping.FacingRight ? _rightWaypoint : _leftWaypoint;
            Debug.Log("Patrol");
        }

        public override void UpdateState()
        {
            if (_targetWaypoint == _rightWaypoint)
            {
                if (_transform.position.x >= _targetWaypoint.x)
                {
                    _enemy.SetState(_enemy.IdleState);
                }
            }
            else
            {
                if (_transform.position.x <= _targetWaypoint.x)
                {
                    _enemy.SetState(_enemy.IdleState);
                }
            }
        }

        public override void FixedUpdateState()
        {
            _movable.Move(_movementDirection);
        }

        public override void ExitState()
        {

        }
    }
}