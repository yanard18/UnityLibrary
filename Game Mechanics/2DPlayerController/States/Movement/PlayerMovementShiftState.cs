using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;



namespace DenizYanar.Player
{
    public class PlayerMovementShiftState : State
    {
        private readonly Rigidbody2D _rb;
        private readonly SenseEnginePlayer _enterShiftSense;
        private readonly SenseEnginePlayer _leaveShiftSense;
        private readonly float _originalGravity;
        private readonly float _speed;
        private readonly float _turnSpeed;


        #region Constructor

        public PlayerMovementShiftState(Rigidbody2D rb, PlayerSettings settings, SenseEnginePlayer enterShiftSense, SenseEnginePlayer leaveShiftSense, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateNameInformerEventChannel = nameInformerEvent;
            _stateName = stateName;
            
            _rb = rb;
            _originalGravity = rb.gravityScale;
            _speed = settings.ShiftModeSpeed;
            _enterShiftSense = enterShiftSense;
            _leaveShiftSense = leaveShiftSense;
            _turnSpeed = settings.ShiftModeTurnSpeed;
        }

        #endregion

        #region State Callbacks

        public override void PhysicsTick()
        {
            base.PhysicsTick(); 
            MoveForward();
            SetAngle();
        }
        
        public override void OnEnter()
        {
            base.OnEnter();

            SetAngleInstant();
            _enterShiftSense.Play();

            _rb.gravityScale = 0;
            _rb.freezeRotation = false;
        }
        
        public override void OnExit()
        {
            base.OnExit();
            _rb.gravityScale = _originalGravity;
            _rb.velocity *= 1.5f;
            _rb.rotation = 0;
            _rb.freezeRotation = true;
            _leaveShiftSense.Play();
        }

        #endregion

        #region Local Methods

        private void MoveForward()
        {
            _rb.velocity = _rb.transform.right * (_speed * Time.fixedDeltaTime);
        }
        
        private void SetAngle()
        {
            if (Camera.main is null) return;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_rb.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rb.rotation = Mathf.MoveTowardsAngle(_rb.rotation, angle, Time.fixedDeltaTime * _turnSpeed);
        }
        
        private void SetAngleInstant()
        {
            if (Camera.main is null) return;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_rb.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
        }

        #endregion
        
        
        
        
    }
    
}