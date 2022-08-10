using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Friction : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _amount;

        public void Apply()
        {
            float amount = Mathf.Min(Mathf.Abs(_rb2d.velocity.x), Mathf.Abs(_amount));
            amount *= Mathf.Sign(_rb2d.velocity.x);
            _rb2d.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }
}