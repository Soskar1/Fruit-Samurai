using System.Collections;
using UnityEngine;

namespace Core.Entities
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Dashing : MonoBehaviour
    {
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private float _force;
        [SerializeField] private float _zeroGravityTime;
        private float _defaultGravity;

        private bool _dashed;
        private bool _searchForGround;

        private void Awake() => _defaultGravity = _rb2d.gravityScale;

        private void Update()
        {
            if (!_searchForGround)
                return;

            if (!_groundCheck.CheckForGround())
                return;

            _dashed = false;
            _searchForGround = false;
        }

        private void LateUpdate()
        {
            if (!_groundCheck.CheckForGround())
                _searchForGround = true;
        }

        public void Dash(Vector2 direction)
        {
            _rb2d.velocity = _dashed ? Vector2.zero : direction * _force;
            StartCoroutine(TurnOffGravityForACertainTime());

            _dashed = true;
        }

        private IEnumerator TurnOffGravityForACertainTime()
        {
            _rb2d.gravityScale = 0;

            yield return new WaitForSeconds(_zeroGravityTime);

            _rb2d.gravityScale = _defaultGravity;
        }
    }
}