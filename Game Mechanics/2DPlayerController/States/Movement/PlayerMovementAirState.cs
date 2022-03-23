using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;

namespace DenizYanar.Player
{
    public class PlayerMovementAirState : State
    {

        private readonly Rigidbody2D _rb;
        private readonly PlayerInputs _inputs;
        
        private readonly float _xAcceleration;
        private readonly float _maxXVelocity;
        private readonly float _yAcceleration;
        private readonly float _maxYVelocity;

        private bool _dive;

        #region Constructor
        
        
        public PlayerMovementAirState(Rigidbody2D rb, PlayerSettings settings, PlayerInputs inputs, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
            _rb = rb;
            _inputs = inputs;

            _xAcceleration = settings.AirStrafeXAcceleration;
            _maxXVelocity = settings.AirStrafeMaxXVelocity;
            _yAcceleration = settings.AirStrafeYAcceleration;
            _maxYVelocity = settings.AirStrafeMaxYVelocity;
        }

        #endregion
        
        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            _inputs.OnDiveStarted += OnDiveStarted;
            _inputs.OnDiveCancelled += OnDiveCancelled;
        }

        public override void OnExit()
        {
            _inputs.OnDiveStarted -= OnDiveStarted;
            _inputs.OnDiveCancelled -= OnDiveCancelled;
            _dive = false;
        }

        public override void PhysicsTick()
        {
            base.PhysicsTick();
            
            //var sKeyInput = Mathf.Sign(Input.GetAxisRaw("Vertical")) < 0 ? 1 : 0;
            var horizontalKeyInput = _inputs.HorizontalMovement;
            
            // X


            switch (horizontalKeyInput)
            {
                //+x
                case 1:
                {
                    if (_rb.velocity.x < _maxXVelocity)
                        _rb.AddForce(new Vector2(_xAcceleration, 0), ForceMode2D.Force);
                    break;
                }
                //-x
                case -1:
                {
                    if (_rb.velocity.x > -_maxXVelocity)
                        _rb.AddForce(new Vector2(-_xAcceleration, 0), ForceMode2D.Force);
                    break;
                }
            }



            // Y
            if(Mathf.Abs(_rb.velocity.y) < _maxYVelocity && _dive)
                _rb.AddForce(new Vector2(0, _yAcceleration), ForceMode2D.Force);
        }

        #endregion


        private void OnDiveStarted() => _dive = true;
        private void OnDiveCancelled() => _dive = false;

    }
}
