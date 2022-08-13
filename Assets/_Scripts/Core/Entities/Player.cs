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
        #region fields
        [Header("Input")]
        [SerializeField] private Input _input;

        [Header("Movement")]
        [SerializeField] private Jumping _jumping;
        [SerializeField] private GroundCheck _groundCheck;
        [SerializeField] private Friction _friction;

        [Header("Abilities")]
        [SerializeField] private Rolling _rolling;
        [SerializeField] private Dashing _dashing;

        [Header("Visual")]
        [SerializeField] private Flipping _flipping;

        [Header("Animation")]
        [SerializeField] private Animator _animator;

        private readonly int _speedParam = Animator.StringToHash("Speed");
        private readonly int _rollTrigger = Animator.StringToHash("Roll");
        private readonly int _attackTrigger = Animator.StringToHash("Attack");

        private Transform _transform;
        #endregion

        private void OnEnable()
        {
            _input.Controls.Player.Jump.performed += TryJump;
            _input.Controls.Player.Attack.performed += TryDash;
            _input.Controls.Player.Attack.performed += TryAttack;
            _input.Controls.Player.Roll.performed += TryRoll;

            _rolling.OnRollEnded += TurnOffInvulnerability;
        }

        private void OnDisable()
        {
            _input.Controls.Player.Jump.performed -= TryJump;
            _input.Controls.Player.Attack.performed -= TryDash;
            _input.Controls.Player.Attack.performed -= TryAttack;
            _input.Controls.Player.Roll.performed -= TryRoll;

            _rolling.OnRollEnded -= TurnOffInvulnerability;
        }

        private void Start() => _transform = transform;

        private void Update()
        {
            if (_combat.IsAttacking)
                return;

            if (_flipping.FacingRight && _input.MovementInput < 0 ||
                !_flipping.FacingRight && _input.MovementInput > 0)
                _flipping.Flip();
        }

        private void FixedUpdate()
        {
            if (_combat.IsAttacking)
                return;

            if (_rolling.IsRolling)
                return;

            Move(_input.MovementInput);
            _animator.SetFloat(_speedParam, Mathf.Abs(_input.MovementInput));

            if (_groundCheck.CheckForGround() && _input.MovementInput == 0)
                _friction.Apply();
        }

        public override void Move(float direction) => _movable.Move(direction);

        private void TryJump(InputAction.CallbackContext ctx)
        {
            if (_combat.IsAttacking)
                return;

            if (_rolling.IsRolling)
                return;

            if (_groundCheck.CheckForGround())
                _jumping.Jump();
        }

        private void TryAttack(InputAction.CallbackContext ctx)
        {
            if (_combat.Cooldown)
                return;

            if (_combat.IsAttacking)
                return;

            if (_rolling.IsRolling)
                return;

            Vector2 mousePosition = _input.GetWorldMousePosition();

            if (mousePosition.x > _transform.position.x && !_flipping.FacingRight ||
                mousePosition.x < _transform.position.x && _flipping.FacingRight)
                _flipping.Flip();

            _combat.StartAttack();
            _animator.SetTrigger(_attackTrigger);
        }

        private void TryDash(InputAction.CallbackContext ctx)
        {
            if (_rolling.IsRolling)
                return;

            if (_combat.Cooldown)
                return;

            if (_combat.IsAttacking)
                return;

            Vector2 mousePosition = _input.GetWorldMousePosition();
            Vector2 direction = mousePosition - (Vector2)transform.position;
            _dashing.Dash(direction.normalized);
        }

        private void TryRoll(InputAction.CallbackContext ctx)
        {
            if (_rolling.IsRolling)
                return;

            if (_rolling.Cooldown)
                return;

            if (!_groundCheck.CheckForGround())
                return;

            int direction = _flipping.FacingRight ? 1 : -1;
            _rolling.Roll(direction);

            _health.Invulnerable = true;

            _animator.SetTrigger(_rollTrigger);
        }

        private void TurnOffInvulnerability() => _health.Invulnerable = false;
    }
}