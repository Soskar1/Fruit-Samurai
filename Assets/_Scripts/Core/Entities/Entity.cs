using UnityEngine;
using Core.Entities.Movement;

namespace Core.Entities
{
    [RequireComponent(typeof(Combat))]
    [RequireComponent(typeof(Health))]
    public abstract class Entity : MonoBehaviour, IHittable
    {
        [SerializeField] protected Health _health;
        [SerializeField] protected Combat _combat;
        protected IMovable _movable;

        private void Awake() => _movable = GetComponent<IMovable>();

        public abstract void Move(float direction);

        public void Hit() => _health.TryTakeDamage();
    }
}