using UnityEngine;

namespace Core.Entities.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _speed;

        public float Speed => _speed;

        public void Move(float direction)
        {
            _rb2d.velocity = new Vector2(direction * _speed * Time.fixedDeltaTime, _rb2d.velocity.y);
        }
    }
}