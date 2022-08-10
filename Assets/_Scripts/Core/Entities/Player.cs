using Core.Entities.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Entities
{
    [RequireComponent(typeof(Jumping))]
    [RequireComponent(typeof(GroundCheck))]
    [RequireComponent(typeof(Flipping))]
    [RequireComponent(typeof(Dashing))]
    [RequireComponent(typeof(Friction))]
    public class Player : Entity
    {
        [SerializeField] private Input _input;
        [SerializeField] private Jumping _jumping;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Flipping _flipping;
        [SerializeField] private Dashing _dashing;
        [SerializeField] private Friction _friction;

        private void OnEnable()
        {
            _input.Controls.Player.Jump.performed += TryJump;
            _input.Controls.Player.Attack.performed += TryDash;
            _input.Controls.Player.Attack.performed += Attack;
        }

        private void OnDisable()
        {
            _input.Controls.Player.Jump.performed -= TryJump;
            _input.Controls.Player.Attack.performed -= TryDash;
            _input.Controls.Player.Attack.performed -= Attack;
        }

        private void Update()
        {
            if (_flipping.FacingRight && _input.MovementInput < 0 ||
                !_flipping.FacingRight && _input.MovementInput > 0)
                _flipping.Flip();
        }

        private void FixedUpdate()
        {
            if (_combat.IsAttacking)
                return;

            Move(_input.MovementInput);

            if (_groundCheck.CheckForGround() && _input.MovementInput == 0)
                _friction.Apply();
        }

        public override void Move(float direction) => _movable.Move(direction);

        private void TryJump(InputAction.CallbackContext ctx)
        {
            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }

        private void Attack(InputAction.CallbackContext ctx) => _combat.TryStartAttack();

        private void TryDash(InputAction.CallbackContext ctx)
        {
            if (_combat.IsAttacking)
                return;

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_input.MousePosition);
            Vector2 direction = mousePosition - (Vector2)transform.position;
            _dashing.Dash(direction.normalized);
        }
    }
}