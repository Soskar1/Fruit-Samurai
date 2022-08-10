using System.Collections;
using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Dashing : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _force;
        [SerializeField] private float _zeroGravityTime;
        private float _defaultGravity;

        private void Awake() => _defaultGravity = _rb2d.gravityScale;

        public void Dash(Vector2 direction)
        {
            _rb2d.velocity = direction * _force;
            StartCoroutine(ChangeGravity());
        }

        private IEnumerator ChangeGravity()
        {
            _rb2d.gravityScale = 0;

            yield return new WaitForSeconds(_zeroGravityTime);

            _rb2d.gravityScale = _defaultGravity;
        }
    }
}