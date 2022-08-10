using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ForceMovement : MonoBehaviour, IMovable
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;
        [SerializeField] private float _decceleration;
        [SerializeField] private float _velocityPower;

        public float Speed => _speed;

        public void Move(float direction)
        {
            float targetSpeed = direction * _speed;
            float speedDif = targetSpeed - _rb2d.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, _velocityPower) * Mathf.Sign(speedDif);

            _rb2d.AddForce(movement * Vector2.right);
        }
    }
}