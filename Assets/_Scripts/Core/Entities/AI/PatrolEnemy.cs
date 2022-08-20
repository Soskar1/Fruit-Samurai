using UnityEngine;

namespace Core.Entities.AI
{
    [RequireComponent(typeof(PatrolPoints))]
    public class PatrolEnemy : Enemy
    {
        [SerializeField] private PatrolPoints _patrolPoints;
        [SerializeField] private float _stopDistance;
        private float _remainingDistance = 0;

        public float StopDistance => _stopDistance;
        public float RemainingDistance => _remainingDistance;

        public override void Awake()
        {
            base.Awake();
            SetDestination(_patrolPoints.CurrentPoint);
        }

        private void FixedUpdate()
        {
            if (_target == null) return;

            Move(GetMovementDirection());
        }

        public override void Move(float direction) => _movable.Move(direction);

        private float GetMovementDirection() => _flipping.FacingRight ? 1 : -1;
    }
}