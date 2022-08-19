using UnityEngine;
using Core.Entities.Movement;

namespace Core.Entities
{
    [RequireComponent(typeof(Health))]
    public abstract class Entity : MonoBehaviour, IHittable
    {
        [SerializeField] protected Health _health;
        protected IMovable _movable;

        public virtual void Awake() => _movable = GetComponent<IMovable>();

        public void Hit() => _health.TryTakeDamage();
    }
}