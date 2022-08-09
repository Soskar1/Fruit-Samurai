using UnityEngine;

namespace Core.Entities.AI
{
    [RequireComponent(typeof(Chasing))]
    public class Enemy : Entity
    {
        [SerializeField] private Chasing _chasing;
        [SerializeField] private float _attackDistance;

        private bool _canChase = false;

        private void OnEnable() => _combat.Attacked += ContinueChasing;
        private void OnDisable() => _combat.Attacked -= ContinueChasing;

        private void Update()
        {
            if (!_canChase)
                return;

            if (_chasing.GetDistanceToTarget() <= _attackDistance)
            {
                Attack();
                _canChase = false;
            }
        }

        private void FixedUpdate()
        {
            if (!_canChase)
                return;

            Move(_chasing.GetMovementDirection());
        }

        public override void Move(float direction) => _movable.Move(direction);

        private void Attack() => _combat.TryAttack();

        private void ContinueChasing() => _canChase = true;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - _attackDistance, transform.position.y));
        }
    }
}
