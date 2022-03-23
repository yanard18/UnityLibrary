using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar.Player
{
    public class PlayerAttackSliceState : State
    {
        private readonly Rigidbody2D _rb;
        private const float DASH_SPEED = 100.0f;
        private Vector2 _movementDirection;

        #region Constructor

        public PlayerAttackSliceState(Rigidbody2D rb)
        {
            _rb = rb;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            _movementDirection = _rb.velocity.normalized;
            _rb.velocity = _movementDirection * DASH_SPEED;
            Debug.Log("SLICE");
        }

        #endregion
        

    }
}