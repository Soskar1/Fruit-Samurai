using UnityEngine;
using Core.Entities.Movement;

namespace Core.Entities
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(Flipping))]
    public abstract class Entity : MonoBehaviour, IHittable
    {
        [SerializeField] protected Flipping _flipping;
        [SerializeField] protected Health _health;
        protected IMovable _movable;
        protected Transform _transform;

        public virtual void Awake()
        {
            _movable = GetComponent<IMovable>();
            _transform = transform;
        }

        public abstract void Move(float direction);

        public void Hit() => _health.TryTakeDamage();
    }
}