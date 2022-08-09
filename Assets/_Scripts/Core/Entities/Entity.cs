using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Health))]
    public abstract class Entity : MonoBehaviour, IHittable
    {
        [SerializeField] private Health _health;
        protected IMovable _movable;

        private void Awake() => _movable = GetComponent<IMovable>();

        public abstract void Move();

        public void Hit(int damage) => _health.TakeDamage(damage);
    }
}