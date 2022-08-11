using UnityEngine;

namespace Core.Entities.AI
{
    [RequireComponent(typeof(Chasing))]
    public class Enemy : Entity
    {
        [SerializeField] private Chasing _chasing;
        [SerializeField] private float _attackDistance;

        private void OnEnable() => _combat.Attacked += _chasing.StartChasing;
        private void OnDisable() => _combat.Attacked -= _chasing.StartChasing;

        private void Update()
        {
            if (!_chasing.CanChase)
                return;

            if (_chasing.GetDistanceToTarget() <= _attackDistance)
            {
                Attack();
                _chasing.StopChasing();
            }
        }

        private void FixedUpdate()
        {
            if (!_chasing.CanChase)
                return;

            Move(_chasing.GetMovementDirection());
        }

        public override void Move(float direction) => _movable.Move(direction);

        private void Attack() => _combat.TryStartAttack();

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - _attackDistance, transform.position.y));
        }
    }
}
