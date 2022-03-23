using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;


namespace DenizYanar.Player
{
    public class PlayerMovementIdleState : State
    {
        private const float FRICTION_ACCELERATION = 40.0f;
        
        private readonly Rigidbody2D _rb;


        #region Constructor

        public PlayerMovementIdleState(Rigidbody2D rb, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _rb = rb;
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
        }

        #endregion
        
        #region State Callbacks
        
        public override void PhysicsTick()
        {
            var currentXVelocity = _rb.velocity.x;
            currentXVelocity = Mathf.MoveTowards(currentXVelocity, 0, Time.fixedDeltaTime * FRICTION_ACCELERATION);
            _rb.velocity = new Vector2(currentXVelocity, _rb.velocity.y);
        }
        
        #endregion
        
        
    }
}
